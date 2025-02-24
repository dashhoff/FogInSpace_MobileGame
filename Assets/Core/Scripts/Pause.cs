using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseOn()
    {
        Time.timeScale = 0f;

        EventController.Instance.Paused();
    }

    public void PauseOff()
    {
        Time.timeScale = 1.0f;

        EventController.Instance.UnPaused();
    }
}
