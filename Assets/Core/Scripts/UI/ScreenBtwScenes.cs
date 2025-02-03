using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBtwScenes : MonoBehaviour
{
    [Space(20f)]
    [SerializeField] private Image _fadePanel;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _fadeTime = 0.5f;

    public void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        _fadePanel.gameObject.SetActive(true);

        DOTween.Sequence()
            .SetUpdate(true)
            .Append(_fadePanel.DOFade(1f, _fadeTime))
            .OnComplete(() => SceneLoader.Instance.LoadLevelId());
    }

    public void FadeOut()
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.DOFade(1f, 0f);

        DOTween.Sequence()
            .SetUpdate(true)
            .Append(_fadePanel.DOFade(0f, _fadeTime))
            .OnComplete(() => _fadePanel.gameObject.SetActive(false));
    }
}
