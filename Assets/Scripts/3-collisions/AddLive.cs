using TMPro;
using UnityEngine;

public class AddLive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField][Tooltip("Lives text")] TextMeshPro livesText;
    [SerializeField] string triggeringTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggeringTag)
        {
            livesText.GetComponent<NumberField>().AddNumber(1);
            Destroy(other.gameObject);
            LiveScreenCounter.Instance.RemoveLife();
        }
    }

}
