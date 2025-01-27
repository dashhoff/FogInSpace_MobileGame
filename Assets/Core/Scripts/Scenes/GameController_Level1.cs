using UnityEngine;

public class GameController_Level1 : MonoBehaviour
{
    public static GameController_Level1 Instance;

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
        Saves.Level = 2;
    }
}
