using System.Collections;
using UnityEngine;

public class Player_Level1 : MonoBehaviour
{
    public static Player_Level1 Instance;

    [Header("Main Settings")]
    public bool Victoried;
    public bool Defeated;

    [Header("HP Settings")]
    [SerializeField] private bool _canRepairing = true;

    [SerializeField] private float _maxHp = 100;
    [Min(0)] [SerializeField] private float _hp;
    [SerializeField] private float _repairTime = 1; //1 hp regenerates in 1 sec

    [Header("Other")]
    [SerializeField] private Rigidbody _shipRb;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _hp = _maxHp;

        StartCoroutine(RepairingCoroutine());
    }

    public void Damage(float damage)
    {
        _hp -= damage;

        if (_hp < 0)
            _hp = 0;
    }

    public float GetHP()
    {
        return _hp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteorite"))
        {
            if (Defeated || Victoried) return;

            Damage(1f);

            EventController.Instance.GetDamage();
        }

        if (collision.gameObject.CompareTag("SpaceBomb"))
        {
            if (Defeated || Victoried) return;

            Damage(10f);

            EventController.Instance.GetDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishZone"))
        {
            if (Victoried) return;

            Victoried = true;

            EventController.Instance.Victoried();
        }
    }

    private IEnumerator RepairingCoroutine()
    {
        while (true)
        {
            if (_hp <= 0)
            {
                if (Defeated || Victoried) yield return null;

                Defeated = true;

                EventController.Instance.Defeated();
            }

            if (!_canRepairing)
            {
                yield return new WaitForSecondsRealtime(_repairTime);
            }

            if (_hp < _maxHp)
            {
                _hp++;
                UIController_Level_1.Instance.UpdateHPBar();
            }

            if (_hp > _maxHp)
                _hp = _maxHp;

            yield return new WaitForSecondsRealtime(_repairTime);
        }
    }
}
