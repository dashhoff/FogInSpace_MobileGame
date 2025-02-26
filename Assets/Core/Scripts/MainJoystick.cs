using UnityEngine;
using UnityEngine.EventSystems;

public class MainJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _handle;
    private Vector2 _input;

    public Vector2 Direction => _input;

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        _input = (eventData.position - (Vector2)transform.position) / (transform as RectTransform).sizeDelta;
        _input = _input.magnitude > 1 ? _input.normalized : _input;
        _handle.anchoredPosition = _input * ((transform as RectTransform).sizeDelta / 2);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }
}
