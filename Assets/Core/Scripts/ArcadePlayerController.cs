using UnityEngine;

public class ArcadePlayerController : MonoBehaviour
{
    public static ArcadePlayerController Instance;

    [Space(20f)]
    [SerializeField] private Engine[] _engines;

    [SerializeField] private Engine[] _rotatingEngines;

    [SerializeField] private Engine[] _mainEngines;
    [SerializeField] private Engine[] _leftEngines;
    [SerializeField] private Engine[] _rightEngines;
    [SerializeField] private Engine[] _backEngines;

    [Space(20f)]
    [SerializeField] private Rigidbody _rb;

    [Space(20f)]
    [SerializeField] private float _power;
    [SerializeField] private float _powerPercentage;

    [Space(20f)]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private bool _keyboardActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        MainCycle();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void MainCycle()
    {

    }

    private void PlayerInput()
    {
        if (_keyboardActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                foreach (Engine engine in _engines)
                {
                    engine.StartForsage();
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                foreach (Engine engine in _engines)
                {
                    engine.StopForsage();
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                foreach (Engine engine in _mainEngines)
                {
                    OnEngine(engine, 1);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                foreach (Engine engine in _mainEngines)
                {
                    OnEngine(engine, 1);
                }
            }
        }

        if (!_keyboardActive)
        {
            if (_joystick.Vertical > _joystick.DeadZone)
            {
                GoUp();
            }
            if (_joystick.Vertical < -_joystick.DeadZone)
            {
                GoDown();
            }

            if (_joystick.Horizontal > _joystick.DeadZone)
            {
                GoRight();
            }
            if (_joystick.Horizontal < -_joystick.DeadZone)
            {
                GoLeft();
            }

            if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            {
                foreach (Engine engine in _engines)
                {
                    OffEngine(engine);
                }
            }
        }
    }

    public void OnEngine(Engine engine, float powerPercentage)
    {
        engine.SetPowerPercentage(powerPercentage);
        engine.StartEngine();
    }

    public void OffEngine(Engine engine)
    {
        engine.SetPowerPercentage(0);
        engine.StopEngine();
    }

    public void GoLeft()
    {
        foreach (Engine engine in _leftEngines)
        {
            OffEngine(engine);
        }

        foreach (Engine engine in _rightEngines)
        {
            OnEngine(engine, -_joystick.Horizontal);
        }
    }

    public void GoRight()
    {
        foreach (Engine engine in _rightEngines)
        {
            OffEngine(engine);
        }

        foreach (Engine engine in _leftEngines)
        {
            OnEngine(engine, _joystick.Horizontal);
        }
    }

    public void GoUp()
    {
        foreach (Engine engine in _backEngines)
        {
            OffEngine(engine);
        }

        foreach (Engine engine in _mainEngines)
        {
            OnEngine(engine, _joystick.Vertical);
        }
    }

    public void GoDown()
    {
        foreach (Engine engine in _mainEngines)
        {
            OffEngine(engine);
        }

        foreach (Engine engine in _backEngines)
        {
            OnEngine(engine, -_joystick.Vertical);
        }
    }
}
