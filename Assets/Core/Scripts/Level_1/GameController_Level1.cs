using UnityEngine;

public class GameController_Level_1 : MonoBehaviour
{
    public static GameController_Level_1 Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventController.Victory += Victory;
    }

    private void OnDisable()
    {
        EventController.Victory -= Victory;
    }

    public void StartGame()
    {

    }

    public void Defeat()
    {
        
    }

    public void Victory()
    {
        Saves.Level = 2;
    }
}
