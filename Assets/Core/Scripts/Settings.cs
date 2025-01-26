using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    public static string Language = "ru";

    public static bool PostProcessingEnabled = true;

    public static float SoundsVolume = 1;
    public static float MusicVolume = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
