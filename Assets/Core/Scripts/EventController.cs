using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;

    public static event Action ShipDamaget;

    public static event Action Victory;

    public static event Action Defeat;

    public static event Action PauseOn;

    public static event Action PauseOff;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GetDamage()
    {
        Debug.Log("Ship Damaget!");

        ShipDamaget?.Invoke();
    }

    public void Victoried()
    {
        Debug.Log("Victory!");

        Victory?.Invoke();
    }

    public void Defeated()
    {
        Debug.Log("Defeat!");

        Defeat?.Invoke();
    }

    public void Paused()
    {
        Debug.Log("Paused!");

        PauseOn?.Invoke();
    }

    public void UnPaused()
    {
        Debug.Log("Unpaused!");

        PauseOff?.Invoke();
    }
}
