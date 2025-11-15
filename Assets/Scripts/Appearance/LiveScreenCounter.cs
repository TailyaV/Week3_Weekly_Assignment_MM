using UnityEngine;

public class LiveScreenCounter : MonoBehaviour
{
    public static LiveScreenCounter Instance;

    public int livesOnScreen = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }

    public void AddNewLife()
    {
        livesOnScreen++;
        Debug.Log("Lives on screen: " + livesOnScreen);
    }

    public void RemoveLife()
    {
        livesOnScreen--;
        Debug.Log("Lives on screen: " + livesOnScreen);
    }
}
