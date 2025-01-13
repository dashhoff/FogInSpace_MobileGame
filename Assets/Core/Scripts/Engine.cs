using UnityEngine;
using UnityEngine.VFX;

public class Engine : MonoBehaviour
{
    [Space(20f)]
    [SerializeField] private bool _isActive = false;

    [Space(20f)]
    [SerializeField] private bool _isStatic = true;

    [Space(20f)]
    [SerializeField] private bool _forsageMode = false;
    [SerializeField] private float _forcemultiplier = 2f;

    [Space(20f)]
    [SerializeField] private bool _overheated = false;

    [Space(20f)]
    [SerializeField] private float _power = 10f;

    [Space(20f)]
    [SerializeField] private float _startEndurance = 100f;
    [SerializeField][Range(0, 100)][Min(0)] private float _endurance = 100;

    [Space(20f)]
    [SerializeField] private float _overheatingSpeed = 0.25f;
    [SerializeField] private float _coolingSpeed = 0.25f;

    [Space(20f)]
    [SerializeField] private Rigidbody _shipRb;

    [Space(20f)]
    [SerializeField] private VisualEffect _engineEffect;

    private void FixedUpdate()
    {
        EngineCycle();
    }

    public void StartEngine()
    {
        _isActive = true;
    }

    public void StopEngine()
    {
        _isActive = false;
    }

    public void RotateEngine()
    {

    }

    public void StartForsage()
    {
        _forsageMode = true;
    }

    public void StopForsage()
    {
        _forsageMode = false;
    }

    private void EngineCycle()
    {
        if (_endurance <= 0)
        {
            _overheated = true;
        }

        if (_overheated)
        {
            _endurance += _coolingSpeed;

            if (_endurance > _startEndurance / 3)
            {
                _overheated = false;
            }
            return;
        }

        if (_isActive)
        {
            if (_forsageMode)
                _endurance -= _overheatingSpeed * _forcemultiplier;
            else
                _endurance -= _overheatingSpeed;

            Vector3 forceDirection = transform.up;

            if (_forsageMode)
                _shipRb.AddForceAtPosition(forceDirection * _power * _forcemultiplier, transform.position, ForceMode.Force);
            else
                _shipRb.AddForceAtPosition(forceDirection * _power, transform.position, ForceMode.Force);

            _engineEffect.Play();
        }
        else
        {
            if (_endurance < _startEndurance)
                _endurance +=_coolingSpeed;
            else
                _endurance = _startEndurance;

            _engineEffect.Stop();
        }
    }
}
