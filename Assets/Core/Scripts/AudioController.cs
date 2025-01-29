using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource[] _allSoundsArray;
    [SerializeField] private AudioSource[] _allMusicArray;

    [SerializeField] private Dictionary<string, AudioSource> _allSounds;

    [SerializeField] private AudioSource _engineSound;
    [SerializeField] private AudioSource _turbineSource;

    [SerializeField] private AudioSource _radiationSound;
    [SerializeField] private AudioSource _noiseSound;

    [SerializeField] private AudioSource _hitSound;

    [SerializeField] private AudioSource _uiSound;

    [SerializeField] private AudioSource _backMusic;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Init()
    {
        
    }

    public void PLay(string name, float newVolume)
    {
        AudioSource sound = _allSounds[name];

        sound.Play();
    }

    public void Stop(string name, float newPitch)
    {
        AudioSource sound = _allSounds[name];

        sound.Stop();
    }

    public void SetVolume(string name, float newVolume)
    {
        AudioSource sound = _allSounds[name];

        sound.volume = newVolume;
    }

    public void SetAllSoundsVolume()
    {
        foreach (AudioSource sound in _allSoundsArray)
        {
            sound.volume *= Settings.Instance.SoundsVolume;
        }
    }
    public void SetAllMusicVolume()
    {
        foreach (AudioSource music in _allMusicArray)
        {
            music.volume *= Settings.Instance.MusicVolume;
        }
    }

    public void SetPitch(string name, float newPitch)
    {
        AudioSource sound = _allSounds[name];

        sound.pitch = newPitch;
    }
}
