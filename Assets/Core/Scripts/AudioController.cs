using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private GameObject _soundPrefab;

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

    [SerializeField] private AudioMixer _soundMixer;
    [SerializeField] private AudioMixer _musicMixer;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventController.ShipDamaget += MetalSound;
    }

    private void OnDisable()
    {
        EventController.ShipDamaget -= MetalSound;

    }

    public void Init()
    {
        for (int i = 0; i < _allSounds.Count; i++)
        {
            _allSounds.Add(_allSoundsArray[i].name, _allSoundsArray[i]);
        }
    }

    public void MetalSound()
    {
        GameObject sound = Instantiate(_soundPrefab);

        AudioSource soundAudioSource = _soundPrefab.GetComponent<AudioSource>();

        soundAudioSource.clip = _hitSound.clip;

        soundAudioSource.Play();
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
        float volume = Mathf.Log10(Settings.Instance.SoundsVolume) * 20;
        _soundMixer.SetFloat("SoundVolume", volume);

        /*foreach (AudioSource sound in _allSoundsArray)
        {
            sound.volume *= Settings.Instance.SoundsVolume;
        }*/
    }
    public void SetAllMusicVolume()
    {
        float volume = Mathf.Log10(Settings.Instance.SoundsVolume) * 20;
        _musicMixer.SetFloat("MusicVolume", volume);

        /*foreach (AudioSource music in _allMusicArray)
        {
            music.volume *= Settings.Instance.MusicVolume;
        }*/
    }

    public void SetPitch(string name, float newPitch)
    {
        AudioSource sound = _allSounds[name];

        sound.pitch = newPitch;
    }
}
