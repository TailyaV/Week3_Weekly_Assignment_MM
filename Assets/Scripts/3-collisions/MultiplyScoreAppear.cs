using TMPro;
using UnityEngine;

public class MultiplyScoreAppear : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag of the score text UI object")]
    string scoreTextTag = "ScoreNumber";

    [SerializeField]
    [Tooltip("Prefab that appears when score reaches the target score")]
    GameObject multiplyPrefab;

    [SerializeField]
    [Tooltip("Margin from the camera edges")]
    float margin = 0.5f;

    [SerializeField]
    [Tooltip("How long the spawned object stays on screen")]
    float lifeTime = 10f;

    [SerializeField]
    [Tooltip("Score at which the object appears")]
    int targetScore = 10;

    private bool hasSpawned = false;   // Ensures the object is spawned only once

    private NumberField numberField;   // Cached reference to NumberField

    private void Start()
    {
        // Find the score text object by tag
        GameObject scoreObj = GameObject.FindGameObjectWithTag(scoreTextTag);
        if (scoreObj == null)
        {
            Debug.LogError($"MultiplyScoreAppear: No GameObject with tag '{scoreTextTag}' was found.");
            return;
        }

        // Get the TMP_Text (TextMeshProUGUI / TextMeshPro) component
        TMP_Text scoreText = scoreObj.GetComponent<TMP_Text>();
        if (scoreText == null)
        {
            Debug.LogError("MultiplyScoreAppear: GameObject with scoreTextTag does not have a TMP_Text component.");
            return;
        }

        // Get the NumberField component from the same object
        numberField = scoreText.GetComponent<NumberField>();
        if (numberField == null)
        {
            Debug.LogError("MultiplyScoreAppear: TMP_Text does not have a NumberField component.");
        }
    }

    private void Update()
    {
        // If already spawned or we don't have a valid NumberField, do nothing
        if (hasSpawned || numberField == null)
            return;

        int score = numberField.GetNumber();

        // When score reaches targetScore (or more), spawn the object once
        if (score == targetScore)
        {
            SpawnObjectOnce();
        }
    }

    private void SpawnObjectOnce()
    {
        Camera cam = Camera.main;
        if (!cam)
        {
            Debug.LogWarning("MultiplyScoreAppear: No main camera found!");
            return;
        }

        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.orthographicSize * cam.aspect;
        Vector3 center = cam.transform.position;

        // Choose random position inside camera bounds
        float randomX = Random.Range(center.x - halfWidth + margin,
                                     center.x + halfWidth - margin);

        float randomY = Random.Range(center.y - halfHeight + margin,
                                     center.y + halfHeight - margin);

        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        // Instantiate the object
        GameObject obj = Instantiate(multiplyPrefab, spawnPos, Quaternion.identity);

        // Destroy the object after the given lifetime (default: 10 seconds)
        Destroy(obj, lifeTime);

        // Mark as spawned to prevent multiple spawns
        hasSpawned = true;
    }
}
