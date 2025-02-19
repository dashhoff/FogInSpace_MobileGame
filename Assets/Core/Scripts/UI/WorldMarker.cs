using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class WorldMarker : MonoBehaviour
{
    [SerializeField] private Transform _target;   // Цель в мире
    [SerializeField] private RectTransform _marker;  // Метка (UI)
    [SerializeField] private TMP_Text _distanceText;  // Текст с расстоянием
    [SerializeField] private Camera _mainCamera;  // Камера игрока
    [SerializeField] private float _borderOffset = 50f; // Отступ от краев экрана

    private void Update()
    {
        if (!_target) return;

        // 1. Переводим координаты мира в экранные
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_target.position);

        // 2. Проверяем, не за границами ли экран (сзади)
        bool isBehind = screenPos.z < 0;
        if (isBehind) screenPos *= -1; // Инвертируем направление

        // 3. Ограничиваем метку краями экрана
        screenPos.x = Mathf.Clamp(screenPos.x, _borderOffset, Screen.width - _borderOffset);
        screenPos.y = Mathf.Clamp(screenPos.y, _borderOffset, Screen.height - _borderOffset);

        // 4. Перемещаем UI-метку
        _marker.position = screenPos;

        // 5. Поворачиваем стрелку на объект
        Vector3 dir = _target.position - _mainCamera.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _marker.rotation = Quaternion.Euler(0, 0, angle);

        // 6. Обновляем расстояние
        float distance = Vector3.Distance(_mainCamera.transform.position, _target.position);
       
        if (Settings.Instance.Language == "ru")
            _distanceText.text = $"Расстояние: {Mathf.RoundToInt(distance)} м";
        else
            _distanceText.text = $"Distance: {Mathf.RoundToInt(distance)} m";

    }
}
