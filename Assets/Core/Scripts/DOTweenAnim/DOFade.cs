using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class DOFade : MonoBehaviour
{
    [SerializeField] private bool _autoStart;

    [SerializeField] private Image _target;

    [SerializeField] private float _startDelay;
    [SerializeField] private float _duration;

    [SerializeField] private float _minFade;
    [SerializeField] private float _maxFade;

    [SerializeField] private float _delay;

    public void Start()
    {
        if (_autoStart)
            Init();
    }

    public void Init()
    {
        DOTween.Sequence()
            .AppendInterval(_startDelay)
            .Append(_target.DOFade(0, _duration));
    }

    public void FadeIn()
    {
        DOTween.Sequence()
            .Append(_target.DOFade(_maxFade, _duration));
    }

    public void FadeOut()
    {
        DOTween.Sequence()
            .Append(_target.DOFade(_minFade, _duration));
    }

    public void PulseFade()
    {
        StartCoroutine(PulseFadeCoroutine());
    }

    public IEnumerator PulseFadeCoroutine()
    {
        FadeIn();

        yield return new WaitForSecondsRealtime(_delay);

        FadeOut();

        yield break;
    }
}
