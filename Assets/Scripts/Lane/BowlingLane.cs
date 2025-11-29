using UnityEngine;

public class BowlingLane : MonoBehaviour
{
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private GameObject ballPrefab;

    private GameObject currentBall;
    public GameObject CurrentBall => currentBall;

    public void SpawnBall()
    {
        if (currentBall != null)
        {
            Debug.Log($"[BowlingLane.SpawnBall] ({gameObject.name} id={GetInstanceID()}) Existing currentBall = {currentBall.name}, sceneValid={currentBall.scene.IsValid()} (hiding+destroying...)");

            currentBall.SetActive(false);

            if (Application.isPlaying)
            {
                Destroy(currentBall); 
            }
            else
            {
                DestroyImmediate(currentBall, true);
            }
            currentBall = null;
        }

        if (ballPrefab == null)
        {
            Debug.LogError("[BowlingLane.SpawnBall] ballPrefab is not assigned!");
            return;
        }

        currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
        Debug.Log($"[BowlingLane.SpawnBall] ({gameObject.name} id={GetInstanceID()}) Spawned new ball = {currentBall.name}, sceneValid={currentBall.scene.IsValid()}");
    }

    public void ResetLane()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("[BowlingLane.ResetLane] ballPrefab is not assigned!");
            return;
        }

        if (currentBall != null && currentBall.scene.IsValid())
        {
            Debug.Log($"[BowlingLane.ResetLane] Reusing existing ball = {currentBall.name}");

            currentBall.transform.SetPositionAndRotation(ballSpawnPoint.position, ballSpawnPoint.rotation);
            currentBall.SetActive(true);

            var rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            var bc = currentBall.GetComponent<BallController>();
            if (bc != null)
            {
                bc.OnReset();
            }

            return;
        }

        SpawnBall();
    }
}
