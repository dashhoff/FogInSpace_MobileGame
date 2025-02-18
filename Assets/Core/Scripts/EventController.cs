using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;

    public static event Action ShipDamaget;

    public static event Action Victory;

    public static event Action Defeat;

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
}
