using UnityEngine;
using UnityEngine.Audio;

public class SoundPrefab : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [Header("Settings")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    [Header("Random")]
    [SerializeField] private Vector2 _volumeRange = new Vector2(0.8f, 1f);
    [SerializeField] private Vector2 _pitchRange = new Vector2(0.95f, 1.05f);

    public void Init()
    {
        _audioSource.volume = Random.Range(_volumeRange.x, _volumeRange.y);
        _audioSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);

        _audioSource.Play();
        Destroy(gameObject, _audioSource.clip.length / _audioSource.pitch); // Удаление после проигрывания
    }

    public void SetPitch(Vector2 newPitch)
    {
        _pitchRange = newPitch;
    }

    public void SetVolume(Vector2 newVolume)
    {
        _volumeRange = newVolume;
    }
}
