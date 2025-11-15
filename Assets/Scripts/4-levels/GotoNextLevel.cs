using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextLevel : MonoBehaviour
{
    [SerializeField] string triggeringTag;
    [SerializeField] string sceneName;

    public static bool isNextLevel = false;   // global flag
    bool isLoading = false;                   // Local anti-duplication lock

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (!other.CompareTag(triggeringTag)) return;

        isLoading = true;
        isNextLevel = true;   // Informing the entire game that the stage is over

        SceneManager.LoadScene(sceneName);
    }
}
