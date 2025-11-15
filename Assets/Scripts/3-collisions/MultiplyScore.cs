using TMPro;
using UnityEngine;

public class MultiplyScore : MonoBehaviour
{
    [SerializeField] string scoreTextTag = "ScoreNumber";     // Tag of the score UI
    [SerializeField] string triggeringTag = "MultiplyScore";     // Tag of the pickup item

    private NumberField numberField;                          // Cached reference

    private void Update()
    {
        // If numberField is missing (new scene loaded), try to find it again
        if (numberField == null)
        {
            GameObject scoreObj = GameObject.FindGameObjectWithTag(scoreTextTag);
            if (scoreObj != null)
            {
                TMP_Text tmp = scoreObj.GetComponent<TMP_Text>();
                if (tmp != null)
                    numberField = tmp.GetComponent<NumberField>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(triggeringTag))
            return;

        if (numberField == null)
            return;  // still not found? do nothing

        // Double the score
        int current = numberField.GetNumber();
        numberField.AddNumber(current);

        // Remove the item
        Destroy(other.gameObject);
    }
}
