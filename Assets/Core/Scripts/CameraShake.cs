using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float _duration;

    [SerializeField] private float _strength;

    [SerializeField] private int _vibrato;

    [SerializeField] private float _randomness;

    [SerializeField] private bool _fadeOut;


    private void OnEnable()
    {
        EventController.ShipDamaget += Shake;
    }

    private void OnDisable()
    {
        EventController.ShipDamaget -= Shake;
    }

    public void Shake()
    {
        _camera.DOShakePosition(_duration, _strength, _vibrato, _randomness, _fadeOut)
            .SetEase(Ease.InOutBounce);
    }
}
