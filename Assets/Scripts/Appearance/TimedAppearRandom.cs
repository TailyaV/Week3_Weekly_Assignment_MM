using UnityEngine;

public class TimedAppearRandom : MonoBehaviour
{
    [Header("Life Prefab")]
    public GameObject playerPrefab;

    [Header("Spawn Settings")]
    public float margin = 0.5f;
    public float spawnInterval = 5f;

    void Start()
    {
        // עדיף async void Start ואז await
        _ = SpawnRoutine();
    }

    async System.Threading.Tasks.Task SpawnRoutine()
    {
        // המתנה ראשונית, אם את רוצה
        await Awaitable.WaitForSecondsAsync(3f);

        while (!GotoGameOver.isGameOver && !GotoNextLevel.isNextLevel)
        {
            // אם האובייקט נהרס (עברנו סצנה והוא לא עושה DontDestroy) – לצאת
            if (!this) break;

            Camera cam = Camera.main;
            if (!cam)
            {
                // אין מצלמה כרגע (במעבר בין סצנות?) – נחכה רגע וננסה שוב
                await Awaitable.WaitForSecondsAsync(0.1f);
                continue;
            }

            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.orthographicSize * cam.aspect;
            Vector3 center = cam.transform.position;

            if (LiveScreenCounter.Instance.livesOnScreen < 1)
            {
                float randomX = Random.Range(center.x - halfWidth + margin,
                                             center.x + halfWidth - margin);

                float randomY = Random.Range(center.y - halfHeight + margin,
                                             center.y + halfHeight - margin);

                Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

                Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                LiveScreenCounter.Instance.AddNewLife();
            }

            await Awaitable.WaitForSecondsAsync(spawnInterval);
        }

        Debug.Log("SpawnRoutine stopped – level ended or object destroyed");
    }
}

