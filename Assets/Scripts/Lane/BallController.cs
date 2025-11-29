using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rollForce = 20f;
    [SerializeField] private InputAction rollAction;
    [SerializeField] private float velocityThreshold = 0.15f;         
    [SerializeField] private float angularVelocityThreshold = 0.1f;    
    [SerializeField] private float minTimeBeforeStop = 0.5f;
    [SerializeField] private float stationaryDistanceThreshold = 0.02f; 
    [SerializeField] private float minStationaryTime = 0.25f;  

    public event Action OnBallStopped;

    private bool hasRolled = false;
    private float rollTime;
    private Vector3 lastPosition;
    private float stationaryTime;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    private void OnEnable()
    {
        rollAction.Enable();
    }

    private void OnDisable()
    {
        rollAction.Disable();
    }

    private void Update()
    {
        if (!hasRolled && rollAction.WasPressedThisFrame())
            RollBall();

        if (hasRolled)
        {
            float timeSinceRoll = Time.time - rollTime;
            float velocity = rb != null ? rb.linearVelocity.magnitude : 0f;
            float angular = rb != null ? rb.angularVelocity.magnitude : 0f;

            float moved = Vector3.Distance(transform.position, lastPosition);
            if (moved <= stationaryDistanceThreshold)
                stationaryTime += Time.deltaTime;
            else
                stationaryTime = 0f;
            lastPosition = transform.position;

            Debug.Log($"[BallController.Update] time={timeSinceRoll:F2}s, vel={velocity:F3}, ang={angular:F3}, moved={moved:F4}, stationaryTime={stationaryTime:F2}");

            bool lowVelocity = velocity < velocityThreshold;
            bool lowAngular = angular < angularVelocityThreshold;
            bool stationaryLongEnough = stationaryTime >= minStationaryTime;
            bool sleeping = rb != null && rb.IsSleeping();

            if (timeSinceRoll > minTimeBeforeStop || sleeping || (lowVelocity && stationaryLongEnough))
            {
                Debug.Log($"[BallController.Update] Ball stopped!");
                hasRolled = false;
                OnBallStopped?.Invoke();
            }
        }
    }

    private void RollBall()
    {
        Debug.Log("[BallController.RollBall] Rolling ball");
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);
        }
        hasRolled = true;
        rollTime = Time.time;
        stationaryTime = 0f;
        lastPosition = transform.position;
    }

    public void OnReset()
    {
        Debug.Log("[BallController.OnReset] Resetting controller");
        hasRolled = false;
        stationaryTime = 0f;
        lastPosition = transform.position;
    }
}
