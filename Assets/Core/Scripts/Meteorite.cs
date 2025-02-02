using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private Vector3 _randomDirection;
    [SerializeField] private Vector3 _randomTorque;

    [SerializeField] private Rigidbody _rb;

    public void Init()
    {
        _rb.AddForce(_randomDirection, ForceMode.Impulse);

        _rb.AddTorque(_randomTorque, ForceMode.Impulse);
    }

    public void SetDirection(Vector3 newValue) => _randomDirection = newValue;

    public void SetTorque(Vector3 newValue) => _randomTorque = newValue;
}
