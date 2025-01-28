using UnityEngine;

public class GameController_Level2 : MonoBehaviour
{
    public static GameController_Level2 Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {

    }

    public void LoseGame()
    {
        
    }

    public void FinishGame()
    {
        Saves.Level = 1;
    }
}
