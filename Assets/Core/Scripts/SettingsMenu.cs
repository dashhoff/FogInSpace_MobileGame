using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance;

    [SerializeField] private TMP_Dropdown _fpsDropdown;

    [SerializeField] private TMP_Dropdown _qualityDropdown;

    [SerializeField] private Toggle _postProcessingToggle;
    [SerializeField] private Volume _postProcessVolume;

    [SerializeField] UniversalRendererData[] _universalRendererDatas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Init()
    {
        SetFPSDropdownValue();

        SetQualityDropdownValue();

        SetPostProcessigDropdownValue();
    }

    public void SetTargetFPS()
    {
        switch (_fpsDropdown.value)
        {
            case 0:
                Settings.Instance.TargetFPS = 30;
                break;
            case 1:
                Settings.Instance.TargetFPS = 60;
                break;
            case 2:
                Settings.Instance.TargetFPS = 75;
                break;
            case 3:
                Settings.Instance.TargetFPS = 90;
                break;
            case 4:
                Settings.Instance.TargetFPS = 120;
                break;
        }

        Application.targetFrameRate = Settings.Instance.TargetFPS;

        Settings.Instance.Save();
    }

    public void SetFPSDropdownValue()
    {
        switch (Settings.Instance.TargetFPS)
        {
            case 30:
                _fpsDropdown.value = 0;
                break;
            case 60:
                _fpsDropdown.value = 1;
                break;
            case 75:
                _fpsDropdown.value = 2;
                break;
            case 90:
                _fpsDropdown.value = 3;
                break;
            case 120:
                _fpsDropdown.value = 4;
                break;
        }
    }

    public void SetQualitySettings()
    {
        QualitySettings.SetQualityLevel(_qualityDropdown.value);

        Settings.Instance.Quality = _qualityDropdown.value;

        Settings.Instance.Save();
    }

    public void SetQualityDropdownValue()
    {
        _qualityDropdown.value = Settings.Instance.Quality;
    }

    public void SetPostProcessig()
    {
        _postProcessVolume.enabled = _postProcessingToggle.isOn;

        Settings.Instance.PostProcessingEnabled = _postProcessingToggle.isOn;

        Settings.Instance.Save();
    }
    public void SetPostProcessigDropdownValue()
    {
        _postProcessingToggle.isOn = Settings.Instance.PostProcessingEnabled;

        _postProcessVolume.enabled = _postProcessingToggle.isOn;

        for (int i = 0; i < _universalRendererDatas.Length; i++)
        {
            //_universalRendererDatas[i]
        }

        //Bloom bloom = _postProcessVolume.profile.TryGet<Bloom>(out var Bloom);

        /*if (_postProcessVolume.profile.TryGet<Bloom>(out var bloom))
            bloom.active = _postProcessingToggle.isOn;

        if (_postProcessVolume.profile.TryGet<Vignette>(out var vignette))
            vignette.active = _postProcessingToggle.isOn;*/
    }
}
