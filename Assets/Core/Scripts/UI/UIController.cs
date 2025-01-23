using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Space(20f)]
    [SerializeField] private Ease _ease;
    [SerializeField] private float _fadeAlpha = 45;
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _popupTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void FadeIn(Image image)
    {
        image.gameObject.SetActive(true);

        DOTween.Sequence()
            .SetUpdate(true)
            .Append(image.DOFade(_fadeAlpha, _fadeTime));
    }

    public void FadeIn(Image image, float time)
	{
        image.gameObject.SetActive(true);

        DOTween.Sequence()
            .SetUpdate(true)
            .Append(image.DOFade(_fadeAlpha, time));
    }

    public void FadeOut(Image image)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .Append(image.DOFade(0f, _fadeTime))
            .OnComplete(() => image.gameObject.SetActive(false));
    }

    public void FadeOut(Image image, float time)
	{
        DOTween.Sequence()
            .SetUpdate(true)
            .Append(image.DOFade(0f, time))
            .OnComplete(() => image.gameObject.SetActive(false));
    }

    public void OpenPopup(GameObject popup)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .SetEase(_ease)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), _popupTime))
            .Append(popup.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    public void OpenPopup(GameObject popup, float time)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .SetEase(_ease)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), time))
            .Append(popup.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    public void ClosePopup(GameObject popup)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .SetEase(_ease)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f))
            .Append(popup.transform.DOScale(new Vector3(0, 0, 0), _popupTime));
    }

    public void ClosePopup(GameObject popup, float time)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .SetEase(_ease)
            .Append(popup.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f))
            .Append(popup.transform.DOScale(new Vector3(0, 0, 0), time));
    }
}
