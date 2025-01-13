using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIEngineEndurance : MonoBehaviour
{
    [SerializeField] private Engine[] _engines;

    [SerializeField] private TMP_Text[] _texts;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        for (int i = 0; i < _engines.Length; i++)
        {
            _texts[i].text = "ÄÂÈÃÀÒÅËÜ " + (i + 1) + " : " + "ÏÅÐÅÃÐÅÂ: " + Mathf.Floor((100 - _engines[i].GetEndurance()) * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2);
        }
    }
}
