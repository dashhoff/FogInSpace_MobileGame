using UnityEngine;
using UnityEngine.VFX;

public class Engine : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private bool _isActive = false;

    [Space(10f)]
    [SerializeField] private bool _isStatic = true;

    [Space(10f)]
    [SerializeField] private bool _forsageMode = false;
    [SerializeField] private float _forcemultiplier = 2f;

    [Space(10f)]
    [SerializeField] private bool _overheated = false;

    [Space(10f)]
    [SerializeField] private bool _broken = false;

    [Space(20f)]
    [SerializeField] private float _power = 50f;
    [SerializeField] private float _powerPercentage = 100;

    [Space(20f)]
    [SerializeField] private float _startEndurance = 100f;
    [SerializeField][Range(0, 100)][Min(0)] private float _endurance = 100;

    [Space(10f)]
    [SerializeField] private float _overheatingSpeed = 0.05f;
    [SerializeField] private float _coolingSpeed = 0.1f;

    [Space(20f)]
    [SerializeField] private float _startHP = 100f;
    [SerializeField][Range(0, 100)][Min(0)] private float _hp = 100;

    [Space(10f)]
    [SerializeField] private float _damageSpeed = 0.01f;
    [SerializeField] private float _repairingSpeed = 0.005f;

    [Space(20f)]
    [SerializeField] private Rigidbody _shipRb;

    [Space(20f)]
    [SerializeField] private VisualEffect _engineEffect;

    private void FixedUpdate()
    {
        EngineCycle();
    }

    public void SetPowerPercentage(float value)
    {
        _powerPercentage = value;
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
        if (_hp <= 0)
        {
            _broken = true;

            DestroyEngine();
        }

        if (_endurance <= 0)
        {
            _overheated = true;
        }

        if (_broken)
        {
            /*RepaireEngine();

            if (_hp > _startHP / 5)
            {
                _broken = false;
            }*/
        }

        if (_overheated)
        {
            CoolingEngine();

            _engineEffect.Stop();

            if (_endurance > _startEndurance / 2)
            {
                _overheated = false;
            }
        }


        if (_isActive && !(_overheated || _broken))
        {
            OverheatidEngine();

            Vector3 forceDirection = transform.up;

            if (_forsageMode)
            {
                float finalPower = _power * _forcemultiplier * _powerPercentage;

                _shipRb.AddForceAtPosition(forceDirection * finalPower, transform.position, ForceMode.Force);
                SelfDamageEngine();
            }
            else
            {
                float finalPower = _power * _powerPercentage;

                _shipRb.AddForceAtPosition(forceDirection * finalPower, transform.position, ForceMode.Force);
                RepaireEngine();
            }

            _engineEffect.Play();
        }
        else
        {
            CoolingEngine();

            RepaireEngine();

            _engineEffect.Stop();
        }
    }

    private void OverheatidEngine()
    {
        if (_forsageMode)
            _endurance -= _overheatingSpeed * _forcemultiplier;
        else
            _endurance -= _overheatingSpeed;
    }

    private void CoolingEngine()
    {
        if (_endurance < _startEndurance)
            _endurance += _coolingSpeed;
        else
            _endurance = _startEndurance;
    }

    private void SelfDamageEngine()
    {
        _hp -= _damageSpeed;
    }

    public void GetDamage(float value)
    {
        _hp += value;
    }

    private void RepaireEngine()
    {
        if (_hp < _startHP)
            _hp += _repairingSpeed;
        else
            _hp = _startHP;
    }

    public void DestroyEngine()
    {
        Destroy(gameObject);
    } 

    public float GetEndurance()
    {
        return _endurance;
    }

    public float GetHP()
    {
        return _hp;
    }
}
