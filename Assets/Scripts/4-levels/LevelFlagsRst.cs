using UnityEngine;

public class LevelFlagsRst : MonoBehaviour
{
    void Awake()
    {
        GotoNextLevel.isNextLevel = false;
        GotoGameOver.isGameOver = false;
    }
}
