using DG.Tweening;
using UnityEngine;

public class DOScale : MonoBehaviour
{
    [SerializeField] private bool _autoStart;

    [SerializeField] private GameObject _target;

    [SerializeField] private float _autoStartDelay;
    [SerializeField] private float _duration;

    [SerializeField] private Ease _ease;

    public void Start()
    {
        if (_autoStart)
            Init();
    }

    public void Init()
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .AppendInterval(_autoStartDelay)
            .SetEase(_ease)
            .Append(_target.transform.DOScale(0f, _duration));
    }
}
