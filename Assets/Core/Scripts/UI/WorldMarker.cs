using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class WorldMarker : MonoBehaviour
{
    [SerializeField] private Transform _target;   // ���� � ����
    [SerializeField] private RectTransform _marker;  // ����� (UI)
    [SerializeField] private TMP_Text _distanceText;  // ����� � �����������
    [SerializeField] private Camera _mainCamera;  // ������ ������
    [SerializeField] private float _borderOffset = 50f; // ������ �� ����� ������

    private void Update()
    {
        if (!_target) return;

        // 1. ��������� ���������� ���� � ��������
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_target.position);

        // 2. ���������, �� �� ��������� �� ����� (�����)
        bool isBehind = screenPos.z < 0;
        if (isBehind) screenPos *= -1; // ����������� �����������

        // 3. ������������ ����� ������ ������
        screenPos.x = Mathf.Clamp(screenPos.x, _borderOffset, Screen.width - _borderOffset);
        screenPos.y = Mathf.Clamp(screenPos.y, _borderOffset, Screen.height - _borderOffset);

        // 4. ���������� UI-�����
        _marker.position = screenPos;

        // 5. ������������ ������� �� ������
        Vector3 dir = _target.position - _mainCamera.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _marker.rotation = Quaternion.Euler(0, 0, angle);

        // 6. ��������� ����������
        float distance = Vector3.Distance(_mainCamera.transform.position, _target.position);
       
        if (Settings.Instance.Language == "ru")
            _distanceText.text = $"����������: {Mathf.RoundToInt(distance)} �";
        else
            _distanceText.text = $"Distance: {Mathf.RoundToInt(distance)} m";

    }
}
