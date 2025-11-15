using UnityEngine;

public class AddPowerShoot : MonoBehaviour
{
    [SerializeField] string triggeringTag = "PowerShoot";  // Tag of the power-up object
    [SerializeField] float powerDuration = 7f;            // How long the triple shot lasts

    private LaserShooter laserShooter;

    private void Awake()
    {
        // Get the LaserShooter on the same player object
        laserShooter = GetComponent<LaserShooter>();
        if (!laserShooter)
        {
            Debug.LogError("AddPowerShoot: No LaserShooter found on this GameObject!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If we hit the power-up object
        if (other.CompareTag(triggeringTag))
        {
            if (laserShooter != null)
            {
                // Activate triple shot for powerDuration seconds
                laserShooter.ActivateTripleShot(powerDuration);
            }

            // Remove the power-up object
            Destroy(other.gameObject);
        }
    }
}
