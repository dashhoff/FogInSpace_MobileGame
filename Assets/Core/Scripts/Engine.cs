using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VFX;

public class Engine : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private bool _arcade = false;

    [Space(10f)]
    [SerializeField] private bool _isActive = false;

    [Space(10f)]
    [SerializeField] private bool _isStatic;

    [SerializeField] private bool _isRotating;
    [SerializeField] private float _rotatingSpeed;

    [Header("Settings")]
    [Space(10f)]
    [SerializeField] private bool _forsageMode = false;
    [SerializeField] private float _forceMultiplier = 2f;
    [Space(20f)]
    [SerializeField] private float _power = 50f;
    [SerializeField] private float _powerPercentage = 100;

    [Space(10f)]
    [SerializeField] private bool _overheated = false;
    [SerializeField] private float _startEndurance = 100f;
    [SerializeField][Range(0, 100)][Min(0)] private float _endurance = 100;
    [SerializeField] private float _overheatingSpeed = 0.05f;
    [SerializeField] private float _coolingSpeed = 0.1f;

    [Space(10f)]
    [SerializeField] private bool _broken = false;
    [SerializeField] private float _startHP = 100f;
    [SerializeField][Range(0, 100)][Min(0)] private float _hp = 100;
    [SerializeField] private float _damageSpeed = 0.01f;
    [SerializeField] private float _repairingSpeed = 0.005f;

    [Space(20f)]
    [SerializeField] private GameObject _model;
    [SerializeField] private Rigidbody _shipRb;

    [Space(20f)]
    [Header("VFX")]
    [SerializeField] private VisualEffect _engineEffect;

    [Space(5f)]
    [SerializeField] private float _minLifeTimeVFX = 0.01f;
    [SerializeField] private float _maxLifeTimeVFX = 0.1f;

    [Space(5f)]
    [SerializeField] private float _minSpeedVFX = 0.01f;
    [SerializeField] private float _maxSpeedVFX = 0.1f;

    private void FixedUpdate()
    {
        EngineCycle();
    }

    private void Update()
    {
        Debug.DrawRay(_model.transform.position, -_model.transform.right * 10f, Color.green);
    }

    public void SetPower(float value)
    {
        _power = value;
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

    public void RotateEngine(Vector2 targetDirection)
    {
        // ¬ычисление угла между текущим направлением и целевым
        float targetAngle = Mathf.Atan2(-targetDirection.y, -targetDirection.x) * Mathf.Rad2Deg;

        // ѕлавное вращение двигател€
        float newAngle = Mathf.MoveTowardsAngle(
            _model.transform.eulerAngles.z, targetAngle, _rotatingSpeed * Time.deltaTime);

        // ”становка нового угла вращени€
        _model.transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void StartForsage()
    {
        _forsageMode = true;
    }

    public void StopForsage()
    {
        _forsageMode = false;
    }

    public void SetForsageMultiplier(float value)
    {
        _forceMultiplier = value;
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

            Vector2 forceDirection = -_model.transform.right;

            if (_forsageMode)
            {
                float finalPower = _power * _forceMultiplier * _powerPercentage;

                if (_arcade)
                    _shipRb.AddForce(forceDirection * finalPower, ForceMode.Force);
                else
                    _shipRb.AddForceAtPosition(forceDirection * finalPower, _model.transform.position, ForceMode.Force);
                
                SelfDamageEngine();
            }
            else
            {
                float finalPower = _power * _powerPercentage;

                if (_arcade)
                    _shipRb.AddForce(forceDirection * finalPower, ForceMode.Force);
                else
                    _shipRb.AddForceAtPosition(forceDirection * finalPower, _model.transform.position, ForceMode.Force);
                
                RepaireEngine();
            }

            SetLifeTimeVFX();
            SetSpeedVFX();
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
            _endurance -= _overheatingSpeed * _forceMultiplier;
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

    public void DamageEngine(float value)
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

    public void SetLifeTimeVFX()
    {
        Vector2 newLifeTime = new Vector2(_minLifeTimeVFX, _maxLifeTimeVFX * _powerPercentage);

        if (_forsageMode)
            newLifeTime = new Vector2(_minLifeTimeVFX, newLifeTime.y * _forceMultiplier);

        _engineEffect.SetVector2("LifeTime", newLifeTime);
    }

    public void SetSpeedVFX()
    {
        Vector2 newSpeed = new Vector2(_minSpeedVFX * _powerPercentage, _maxSpeedVFX * _powerPercentage);

        if (_forsageMode)
            newSpeed = new Vector2(newSpeed.x * _forceMultiplier, newSpeed.y * _forceMultiplier);

        _engineEffect.SetVector2("Speed", newSpeed);
    }
}
