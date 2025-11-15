using UnityEngine;

/**
 * This component moves its object either:
 * 1) By a fixed velocity vector (legacy mode), or
 * 2) In the local "up" direction with a given speed (for rotated bullets).
 */
public class Mover : MonoBehaviour
{
    [Header("Movement mode")]
    [Tooltip("If true: move by 'velocity' vector.\nIf false: move by transform.up * speed.")]
    [SerializeField] bool useVelocity = true;

    [Header("Velocity mode")]
    [Tooltip("Movement vector in units per second (legacy mode)")]
    [SerializeField] Vector3 velocity = Vector3.up;

    [Header("Transform.up mode")]
    [Tooltip("Movement speed in units per second (used when useVelocity = false)")]
    [SerializeField] float speed = 10f;

    private void Update()
    {
        if (useVelocity)
        {
            // Old behavior: move by velocity vector, no rotation changes
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            // New behavior: move in the local 'up' direction (supports rotated bullets)
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    /// In velocity mode: just store the vector.
    /// In transform.up mode: you can decide if you even need this.
    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
