using UnityEngine;

public class Player_Level1 : MonoBehaviour
{
    public static Player_Level1 Instance;

    [SerializeField] private bool _victoried;
    [SerializeField] private bool _defeated;

    [SerializeField] private Rigidbody _shipRb;

    [SerializeField] private float _hp = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GetDamage(float damage)
    {
        _hp -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteorite"))
        {
            EventController.Instance.GetDamage();

            GetDamage(0.02f);
        }
    }
}
