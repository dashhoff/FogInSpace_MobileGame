using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 5, -10);
    [SerializeField] private float _followSpeed = 10f;
    [SerializeField] private bool _smoothFollow = true;

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        Vector3 targetPosition = _target.position + _offset;

        if (_smoothFollow)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
