using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    public string Language = "ru";

    public bool PostProcessingEnabled = true;

    public float SoundsVolume = 1;
    public float MusicVolume = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetSoundsVolume(float newValue)
    {
        SoundsVolume = newValue;
    }

    public void SetMusicVolume(float newValue)
    {
        MusicVolume = newValue;
    }
}
