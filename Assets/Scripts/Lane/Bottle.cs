using UnityEngine;

public class Bottle : MonoBehaviour
{
    private const float FALL_ANGLE = 45f;
    private const float FALL_HEIGHT = 1.35f;

    public bool IsFallen()
    {
        Debug.Log($"[Bottle.IsFallen] {gameObject.name}: y={transform.position.y}");
        
        bool tiltedOver = Vector3.Angle(transform.up, Vector3.up) > FALL_ANGLE;
        bool fellDown = transform.position.y < FALL_HEIGHT;

        return fellDown || tiltedOver;
    }
}
