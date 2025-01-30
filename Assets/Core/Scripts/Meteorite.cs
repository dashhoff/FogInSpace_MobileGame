using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private Vector3 _randomDirection;
    [SerializeField] private Vector3 _randomTorque;

    [SerializeField] private Rigidbody _rb;

    public void Init()
    {
        _rb.AddForce(_randomDirection);
        _rb.AddTorque(_randomTorque);
    }
}
