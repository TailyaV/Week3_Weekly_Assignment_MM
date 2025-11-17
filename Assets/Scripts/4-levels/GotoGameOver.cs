using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoGameOver : MonoBehaviour
{
    [SerializeField] [Tooltip("Lives text")] TextMeshPro livesText;
    [SerializeField] string triggeringTag;
    [SerializeField] [Tooltip("Name of scene to move to when triggering the given tag")] string sceneName;
    public static bool isGameOver = false;
    //[SerializeField] NumberField scoreField;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isGameOver = false;
        if (other.tag == triggeringTag && livesText.GetComponent<NumberField>().GetNumber() == 0)
        {
            Debug.Log("Moving " + this + " to zero");
            this.transform.position = Vector3.zero;
            if (!isGameOver)
            {
                isGameOver = true;
            }
            SceneManager.LoadScene(sceneName);    // Input can either be a serial number or a name; here we use name.
        }
        if (other.tag == triggeringTag && livesText.GetComponent<NumberField>().GetNumber() != 0)
        {
            livesText.GetComponent<NumberField>().AddNumber(-1);
            livesText.GetComponent<NumberField>().SetNumber(livesText.GetComponent<NumberField>().GetNumber());
            Destroy(other.gameObject);
        }
    }

}
