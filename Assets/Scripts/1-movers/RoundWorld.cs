using UnityEngine;

public class WorldBoundsByCamera : MonoBehaviour
{
    private Camera cam;

    float leftBound, rightBound, topBound, bottomBound;

    void Update()
    {
        // אם אין מצלמה (או שהיא נהרסה) – ננסה למצוא מחדש
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                return; // עדיין אין מצלמה? לא לעשות כלום בפריים הזה
        }

        UpdateBounds();
        HandleHorizontalWrap();
        HandleVerticalClamp();
    }

    void UpdateBounds()
    {
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        Vector3 c = cam.transform.position;

        leftBound = c.x - width / 2f;
        rightBound = c.x + width / 2f;
        bottomBound = c.y - height / 2f;
        topBound = c.y + height / 2f;
    }

    void HandleHorizontalWrap()
    {
        Vector3 pos = transform.position;

        if (pos.x < leftBound)
            pos.x = rightBound;
        else if (pos.x > rightBound)
            pos.x = leftBound;

        transform.position = pos;
    }

    void HandleVerticalClamp()
    {
        Vector3 pos = transform.position;

        pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);

        transform.position = pos;
    }
}
