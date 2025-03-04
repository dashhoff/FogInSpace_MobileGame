using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController_Level_1 : MonoBehaviour
{
    public static UIController_Level_1 Instance;

    [SerializeField] private UIPanel _mainPanel;

    [SerializeField] private UIPanel _victoryPanel;

    [SerializeField] private UIPanel _defeaPanel;

    [SerializeField] private DOFade _attentionPanel;

    [Space(20f)]
    [SerializeField] private ArcadePlayerController _playerController;
    [SerializeField] private Player_Level1 _player;

    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _forsageBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventController.ShipDamaget += PulseAttention;
        EventController.ShipDamaget += UpdateHPBar;

        EventController.Victory += Victory;
        EventController.Defeat += Defeat;

    }

    private void OnDisable()
    {
        EventController.ShipDamaget -= PulseAttention;
        EventController.ShipDamaget -= UpdateHPBar;

        EventController.Victory -= Victory;
        EventController.Defeat -= Defeat;
    }

    public void UpdateHPBar()
    {
        _hpBar.fillAmount = _player.GetHP() / 100;
    }

    public void UpdateForsageBar()
    {
        _forsageBar.fillAmount = _playerController.GetForsage() / 100;
    }

    private void PulseAttention()
    {
        _attentionPanel.PulseFade();
    }

    private void Victory()
    {
        _mainPanel.Close();

        _victoryPanel.Open();
    }

    private void Defeat()
    {
        _mainPanel.Close();

        _defeaPanel.Open();
    }
}
