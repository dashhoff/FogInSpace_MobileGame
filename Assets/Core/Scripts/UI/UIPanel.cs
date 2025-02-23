using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _background;

    [SerializeField] private float _alpha;
    [SerializeField] private float _duration;

    public void Close()
    {
        _panel.SetActive(false);

        DOTween.Sequence()
            .Append(_background.DOFade(0, _duration))
            .OnComplete(() =>
            {
                _background.gameObject.SetActive(false);
            });
    }

    public void Open()
    {
        _background.gameObject.SetActive(true);

        DOTween.Sequence()
            .Append(_background.DOFade(0,0))
            .Append(_background.DOFade(_alpha, _duration)).OnComplete(() =>
            {
                _panel.SetActive(true);
            });
    }
}
