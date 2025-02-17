using UnityEngine;
using UnityEngine.VFX;

public class ArcadePlayerController : MonoBehaviour
{
    public static ArcadePlayerController Instance;

    [Space(20f)]
    [SerializeField] private Engine[] _engines;

    [SerializeField] private Engine[] _staticEngines;
    [SerializeField] private Engine[] _rotatingEngines;

    [Space(20f)]
    [SerializeField] private float _power;
    [SerializeField] private float _powerPercentage;

    [SerializeField] private bool _forsageMode;
    [SerializeField] private float _forsageMultiplier;

    [Space(20f)]
    [SerializeField] private Rigidbody _rb;

    [Space(20f)]
    [SerializeField] private Joystick _moveJoystick;

    [Space(20f)]
    [Header("Rotating")]
    [SerializeField] private bool _leftRotating;
    [SerializeField] private bool _rightRotating;

    [SerializeField] private float _rotatingPower;

    [Space(20f)]
    [SerializeField] private VisualEffect[] _onLeftRotateEngines;
    [SerializeField] private VisualEffect[] _onRightRotateEngines;

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
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        Vector2 joystickInput = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);

        float powerPercentage = 1;

        if (_moveJoystick.Vertical > 0 && _moveJoystick.Horizontal > 0)
            powerPercentage = Mathf.Max(_moveJoystick.Vertical, _moveJoystick.Horizontal);

        if (_moveJoystick.Vertical > 0 && _moveJoystick.Horizontal < 0)
            powerPercentage = Mathf.Max(_moveJoystick.Vertical, -_moveJoystick.Horizontal);

        if (_moveJoystick.Vertical < 0 && _moveJoystick.Horizontal > 0)
            powerPercentage = Mathf.Max(-_moveJoystick.Vertical, _moveJoystick.Horizontal);

        if (_moveJoystick.Vertical < 0 && _moveJoystick.Horizontal < 0)
            powerPercentage = Mathf.Max(-_moveJoystick.Vertical, -_moveJoystick.Horizontal);

        if (_moveJoystick.Vertical > _moveJoystick.DeadZone 
            || _moveJoystick.Vertical < -_moveJoystick.DeadZone
            || _moveJoystick.Horizontal > _moveJoystick.DeadZone
            || _moveJoystick.Horizontal < -_moveJoystick.DeadZone)
        {
            foreach (var engine in _rotatingEngines) 
            {
                engine.RotateEngine(joystickInput);
                OnEngine(engine, powerPercentage);
            }
        }

        if (_moveJoystick.Horizontal == 0 && _moveJoystick.Vertical == 0)
        {
            foreach (Engine engine in _engines)
            {
                OffEngine(engine);
            }
        }

        EditSound();
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
            _rb.transform.Rotate(Vector3.right * -_rotatingPower * Time.deltaTime);
            //_rb.AddTorque(transform.up * -_rotatingPower, ForceMode.Acceleration);

            foreach (var engine in _onRightRotateEngines) engine.Stop();
            foreach (var engine in _onLeftRotateEngines) engine.Play();
        }

        if (_rightRotating)
        {
            _rb.transform.Rotate(Vector3.right * _rotatingPower * Time.deltaTime);
            //_rb.AddTorque(transform.forward * _rotatingPower, ForceMode.Acceleration);

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

    public void EditSound()
    {
        //AudioController.Instance.SetVolume("engine", _powerPercentage);
        //AudioController.Instance.SetVolume("turbine", _powerPercentage);
    }

    public void EnableForsage()
    {
        _forsageMode = true;
    }

    public void DisableForsage()
    {
        _forsageMode = false;
    }
}
