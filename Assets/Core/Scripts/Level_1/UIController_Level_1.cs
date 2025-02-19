using UnityEngine;
using DG.Tweening;

public class UIController_Level_1 : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private GameObject _victoryPanel;

    [SerializeField] private GameObject _defeaPanel;

    [SerializeField] private DOFade _attentionPanel;

    private void OnEnable()
    {
        EventController.ShipDamaget += PulseAttention;

        EventController.Victory += Victory;
        EventController.Defeat += Defeat;

    }

    private void OnDisable()
    {
        EventController.ShipDamaget -= PulseAttention;

        EventController.Victory -= Victory;
        EventController.Defeat -= Defeat;
    }

    private void PulseAttention()
    {
        _attentionPanel.PulseFade();
    }

    private void Victory()
    {
        _mainPanel.SetActive(false);


    }

    private void Defeat()
    {
        _mainPanel.SetActive(false);


    }
}
