using UnityEngine;

/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also updates the "scoreText" field of the new laser.
 */
public class LaserShooter : ClickSpawner
{
    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]
    int pointsToAdd = 1;

    [SerializeField]
    [Tooltip("Angle (in degrees) for left/right lasers when triple shot is active")]
    float tripleShotAngle = 30f;

    private NumberField scoreField;

    // Power-up state
    private bool tripleShotActive = false;

    private void Start()
    {
        scoreField = GetComponentInChildren<NumberField>();
        if (!scoreField)
            Debug.LogError($"No child of {gameObject.name} has a NumberField component!");
    }

    private void AddScore()
    {
        scoreField.AddNumber(pointsToAdd);
    }

    // Called by AddPowerShoot when the player picks up the power-up
    public void ActivateTripleShot(float duration)
    {
        // If already active, we can just restart the timer
        StopAllCoroutines();
        StartCoroutine(TripleShotRoutine(duration));
    }

    private System.Collections.IEnumerator TripleShotRoutine(float duration)
    {
        tripleShotActive = true;
        yield return new WaitForSeconds(duration);
        tripleShotActive = false;
    }

    protected override GameObject spawnObject()
    {
        // Main (center) laser
        GameObject centerLaser = SpawnSingleLaser(0f);

        // If triple shot is active – spawn two additional lasers
        if (tripleShotActive)
        {
            SpawnSingleLaser(+tripleShotAngle);  // right
            SpawnSingleLaser(-tripleShotAngle);  // left
        }

        return centerLaser;
    }

    // Helper to spawn one laser with rotation offset (in degrees)
    private GameObject SpawnSingleLaser(float angleDegrees)
    {
        GameObject newObject = base.spawnObject();  // base = super

        // Rotate the laser by given angle around Z (for 2D)
        newObject.transform.Rotate(0f, 0f, angleDegrees);

        // Hook score callback
        DestroyOnTrigger2D newObjectDestroyer = newObject.GetComponent<DestroyOnTrigger2D>();
        if (newObjectDestroyer)
            newObjectDestroyer.onHit += AddScore;

        return newObject;
    }
}
