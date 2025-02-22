using UnityEngine;
using DG.Tweening;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private Vector2 _randomScale;
    [SerializeField] private AnimationCurve _randomScaleCurve;

    [SerializeField] private Vector2 _randomDirection;


    [SerializeField] private Vector2 _randomTorque;




    [SerializeField] private Rigidbody _rb;

    public void Init()
    {
        _rb.angularVelocity = new Vector3(0,0,0);

        RandomScale();

        RandomDirection();

        RandomTorque();
    }

    private void RandomScale()
    {
        float t = Random.value;
        float curveValue = _randomScaleCurve.Evaluate(t);
        float scale = Mathf.Lerp(_randomScale.x, _randomScale.y, curveValue);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void RandomDirection()
    {
        float random = Random.Range(_randomDirection.x, _randomDirection.y);
        Vector3 direction = new Vector3(random, random, 0);
        _rb.AddForce(direction, ForceMode.Impulse);
    }

    private void RandomTorque()
    {
        float random_2 = Random.Range(_randomTorque.x, _randomTorque.y);
        Vector2 torque = new Vector2(random_2, random_2);
        _rb.AddTorque(torque, ForceMode.Impulse);
    }

    public void SetDirection(Vector2 newValue) => _randomDirection = newValue;

    public void SetTorque(Vector2 newValue) => _randomTorque = newValue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        DOTween.Sequence()
            .AppendInterval(2f)
            .Append(transform.DOScale(0, 1f))
            .OnComplete(() => 
            {
                gameObject.SetActive(false);
            });
    }
}
