using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private GameObject _popup;

    public void OpenPopup()
    {
        UIController.Instance.FadeIn(_backgroundImage);
        UIController.Instance.OpenPopup(_popup);
    }

    public void ClosePopup()
    {
        UIController.Instance.FadeOut(_backgroundImage);
        UIController.Instance.ClosePopup(_popup);
    }
}
