using TMPro;
using UnityEngine;

public class TextTranslator : MonoBehaviour
{
    [SerializeField] private TMP_Text _targetText;

    [SerializeField] private string _ruText;
    [SerializeField] private string _enText;

    private void Start()
    {
        if (_targetText == null)
            _targetText = GetComponent<TMP_Text>();

        if (Settings.Instance.Language == "ru")
            _targetText.text = _ruText;
        else
            _targetText.text = _enText;
    } 
}
