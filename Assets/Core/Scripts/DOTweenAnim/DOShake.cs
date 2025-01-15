using UnityEngine;
using DG.Tweening;

public class DOShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float _duration = 1f; // 100
    [SerializeField] private float _strength = 1f; // 0.05
    [SerializeField] private int _vibrato = 10; //5
    [SerializeField] private float _randomness = 90f;

    [Header("Axis Control")]
    [SerializeField] private bool _shakeX = true;
    [SerializeField] private bool _shakeY = true;
    [SerializeField] private bool _shakeZ = true;

    [Header("Auto Start")]
    [SerializeField] private bool _autoStart = false;
    [SerializeField] private bool _loop = false;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.localPosition;

        if (_autoStart)
        {
            if (_loop)
            {
                Shake().SetLoops(-1);
            }
            else
            {
                Shake();
            }
        }
    }

    public Tween Shake()
    {
        Vector3 shakeStrength = new Vector3(
            _shakeX ? _strength : 0f,
            _shakeY ? _strength : 0f,
            _shakeZ ? _strength : 0f
        );

        return transform.DOShakePosition(_duration, shakeStrength, _vibrato, _randomness)
                        .OnComplete(() => transform.localPosition = _originalPosition);
    }
}
