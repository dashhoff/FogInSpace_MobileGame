using Unity.VisualScripting;
using UnityEngine;

public class SleepOnInvisible : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    private void OnBecameInvisible()
    {
        _rb.Sleep();
        _rb.isKinematic = true;
    }

    private void OnBecameVisible()
    {
        _rb.WakeUp();
        _rb.isKinematic = false;
    }
}
