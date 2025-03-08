using UnityEngine;
using UnityEngine.VFX;

public class ArcadePlayerController : MonoBehaviour
{
    public static ArcadePlayerController Instance;

    [Header("Engines")]
    [SerializeField] private Engine[] _engines;

    [SerializeField] private Engine[] _rotatingEngines;

    [Header("Engine Settings")]
    [SerializeField] private float _power;
    [SerializeField] private float _powerPercentage;

    [Header("Forsage Settings")]
    [SerializeField] private bool _forsageMode;
    [SerializeField] private float _forsageMultiplier;

    [SerializeField] private float _maxForsageEndurance = 100;
    [SerializeField] private float _forsageEndurance = 100;
    [SerializeField] private float _forsageConsumptionRate = 5;
    [SerializeField] private float _forsageEnduranceRegeneration = 2;

    [Header("Rotating")]
    [SerializeField] private bool _leftRotating;
    [SerializeField] private bool _rightRotating;

    [SerializeField] private float _rotatingPower;

    [Space(20f)]
    /*[SerializeField] private ParticleSystem[] _onLeftRotateEngines;
    [SerializeField] private ParticleSystem[] _onRightRotateEngines;*/

    [SerializeField] private VisualEffect[] _onLeftRotateEngines;
    [SerializeField] private VisualEffect[] _onRightRotateEngines;

    [Header("Other")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private MainJoystick _mainJoystick;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Engine engine in _engines)
        {
            engine.SetPower(_power);
            engine.SetForsageMultiplier(_forsageMultiplier);
        }
    }

    private void FixedUpdate()
    {
        RotateShip();

        MainCycle();
    }

    private void Update()
    {
        PlayerInput();

        EditSoundsVolume();
    }

    private void OnEnable()
    {
        EventController.Victory += OffAllEngine;
        EventController.Defeat += OffAllEngine;
    }

    private void OnDisable()
    {
        EventController.Victory -= OffAllEngine;
        EventController.Defeat -= OffAllEngine;
    }

    /*private void PlayerInput()
    {
        if (Player_Level1.Instance.Defeated || Player_Level1.Instance.Victoried) return;

        Vector2 joystickInput = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);

        //float powerPercentage = 1;

        if (_moveJoystick.Vertical > 0 && _moveJoystick.Horizontal > 0)
            _powerPercentage = Mathf.Max(_moveJoystick.Vertical, _moveJoystick.Horizontal);

        if (_moveJoystick.Vertical > 0 && _moveJoystick.Horizontal < 0)
            _powerPercentage = Mathf.Max(_moveJoystick.Vertical, -_moveJoystick.Horizontal);

        if (_moveJoystick.Vertical < 0 && _moveJoystick.Horizontal > 0)
            _powerPercentage = Mathf.Max(-_moveJoystick.Vertical, _moveJoystick.Horizontal);

        if (_moveJoystick.Vertical < 0 && _moveJoystick.Horizontal < 0)
            _powerPercentage = Mathf.Max(-_moveJoystick.Vertical, -_moveJoystick.Horizontal);

        if (_moveJoystick.Vertical > _moveJoystick.DeadZone 
            || _moveJoystick.Vertical < -_moveJoystick.DeadZone
            || _moveJoystick.Horizontal > _moveJoystick.DeadZone
            || _moveJoystick.Horizontal < -_moveJoystick.DeadZone)
        {
            foreach (var engine in _rotatingEngines) 
            {
                engine.RotateEngine(joystickInput);
                OnEngine(engine, _powerPercentage);
            }
        }

        if (_moveJoystick.Horizontal == 0 && _moveJoystick.Vertical == 0)
        {
            _powerPercentage = 0;

            foreach (Engine engine in _engines)
            {
                OffEngine(engine);
            }
        }
    }*/

    private void MainCycle()
    {
        if (_forsageMode)
        {
            _forsageEndurance -= _forsageConsumptionRate;

            if (_forsageEndurance > 0)
                EnableForsageInEngines();
            else
                _forsageMode = false;
        }
        else
        {
            DisableForsageInEngines();

            if (_forsageEndurance < _maxForsageEndurance)
                _forsageEndurance += _forsageEnduranceRegeneration;

            if (_forsageEndurance > _maxForsageEndurance)
                _forsageEndurance = _maxForsageEndurance;
        }
    }

    private void PlayerInput()
    {
        if (Player_Level1.Instance.Defeated || Player_Level1.Instance.Victoried) return;

        Vector2 joystickInput = new Vector2(_mainJoystick.Direction.x, _mainJoystick.Direction.y);

        //float powerPercentage = 1;

        if (_mainJoystick.Direction.y > 0 && _mainJoystick.Direction.x > 0)
            _powerPercentage = Mathf.Max(_mainJoystick.Direction.y, _mainJoystick.Direction.x);

        if (_mainJoystick.Direction.y > 0 && _mainJoystick.Direction.x < 0)
            _powerPercentage = Mathf.Max(_mainJoystick.Direction.y, -_mainJoystick.Direction.x);

        if (_mainJoystick.Direction.y < 0 && _mainJoystick.Direction.x > 0)
            _powerPercentage = Mathf.Max(-_mainJoystick.Direction.y, _mainJoystick.Direction.x);

        if (_mainJoystick.Direction.y < 0 && _mainJoystick.Direction.x < 0)
            _powerPercentage = Mathf.Max(-_mainJoystick.Direction.y, -_mainJoystick.Direction.x);

        if (_mainJoystick.Direction.y > _moveJoystick.DeadZone
            || _mainJoystick.Direction.y < -_moveJoystick.DeadZone
            || _mainJoystick.Direction.x > _moveJoystick.DeadZone
            || _mainJoystick.Direction.x < -_moveJoystick.DeadZone)
        {
            foreach (var engine in _rotatingEngines)
            {
                engine.RotateEngine(joystickInput);
                OnEngine(engine, _powerPercentage);
            }
        }

        if (_mainJoystick.Direction.x == 0 && _moveJoystick.Vertical == 0)
        {
            _powerPercentage = 0;

            foreach (Engine engine in _engines)
            {
                OffEngine(engine);
            }
        }
    }

    public void OnLeftRotate()
    {
        _rightRotating = false;
        _leftRotating = true;
    }

    public void OffLeftRotate()
    {
        _leftRotating = false;
    }

    public void OnRightRotate()
    {
        _leftRotating = false;
        _rightRotating = true;
    }

    public void OffRightRotate()
    {
        _rightRotating = false;
    }

    private void RotateShip()
    {
        if (_leftRotating && _rightRotating) return;

        if (_leftRotating)
        {
            //_rb.transform.Rotate(Vector3.forward * _rotatingPower * Time.deltaTime);

            _rb.AddTorque(transform.forward * _rotatingPower, ForceMode.Acceleration);

            foreach (var engine in _onRightRotateEngines) engine.Stop();
            foreach (var engine in _onLeftRotateEngines) engine.Play();
        }

        if (_rightRotating)
        {
            //_rb.transform.Rotate(Vector3.forward * -_rotatingPower * Time.deltaTime);

            _rb.AddTorque(transform.forward * -_rotatingPower, ForceMode.Acceleration);

            foreach (var engine in _onLeftRotateEngines) engine.Stop();
            foreach (var engine in _onRightRotateEngines) engine.Play();
        }
    }

    public void OnEngine(Engine engine, float powerPercentage)
    {
        engine.SetPowerPercentage(powerPercentage);

        if (_forsageMode)
            engine.StartForsage();
        else
            engine.StopForsage();

        engine.StartEngine();
    }

    public void OffEngine(Engine engine)
    {
        engine.SetPowerPercentage(0);

        engine.StopEngine();
    }

    public void OffAllEngine()
    {
        _powerPercentage = 0;

        foreach (Engine engine in _engines)
        {
            OffEngine(engine);
        }
    }

    public void EditSoundsVolume()
    {
        AudioController.Instance.SetVolume("EngineSound", _powerPercentage / 10);
        AudioController.Instance.SetVolume("TurbineSound", _powerPercentage / 10);
    }

    public void EnableForsageInEngines()
    {
        foreach (Engine engine in _engines)
        {
            engine.StartForsage();
        }
    }

    public void DisableForsageInEngines()
    {
        foreach (Engine engine in _engines)
        {
            engine.StopForsage();
        }
    }

    public void EnableForsage()
    {
        _forsageMode = true;

        Debug.Log("ForsageON");
    }

    public void DisableForsage()
    {
        _forsageMode = false;

        Debug.Log("ForsageOFF");
    }

    public float GetForsage()
    {
        return _forsageEndurance;
    }
}
