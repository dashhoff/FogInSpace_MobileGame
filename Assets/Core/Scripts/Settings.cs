using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    public string Language = "ru";

    public int TargetFPS = 60;
    public int Quality = 1;

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

    public void Load()
    {
        Language = PlayerPrefs.GetString("Language", "ru");

        TargetFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        Quality = PlayerPrefs.GetInt("Quality", 2);

        if (PlayerPrefs.GetInt("PostProcessingEnabled", 1) == 1)
            PostProcessingEnabled = true;
        else
            PostProcessingEnabled = false;

        SoundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public void Save()
    {
        PlayerPrefs.SetString("Language", Language);

        PlayerPrefs.SetInt("TargetFPS", TargetFPS);
        PlayerPrefs.SetInt("Quality", Quality);

        if (PostProcessingEnabled)
           PlayerPrefs.SetInt("PostProcessingEnabled", 1);
        else
            PlayerPrefs.SetInt("PostProcessingEnabled", 0);

        PlayerPrefs.SetFloat("SoundsVolume", SoundsVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

    public void SetSoundsVolume(float newValue)
    {
        SoundsVolume = newValue;
    }

    public void SetMusicVolume(float newValue)
    {
        MusicVolume = newValue;
    }

    public void SetTargetFPS(int newValue)
    {
        TargetFPS = newValue;
    }

    public void SetQuality(int newValue)
    {
        switch (newValue)
        {
            case 0:
                Quality = 0;
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                Quality = 1;
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                Quality = 2;
                QualitySettings.SetQualityLevel(2);
                break;
        }
    }
}
