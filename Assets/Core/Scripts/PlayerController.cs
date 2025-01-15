using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Space(20f)]
    [SerializeField] private Engine[] _engines;

    [SerializeField] private Engine[] _staticEngines;
    [SerializeField] private Engine[] _rotatingEngines;

    [SerializeField] private Engine[] _leftEngines;
    [SerializeField] private Engine[] _rightEngines;

    [Header("Двигатели для джойстика")]
    [SerializeField] private Engine[] _WEngines;
    [SerializeField] private Engine[] _AEngines;
    [SerializeField] private Engine[] _SEngines;
    [SerializeField] private Engine[] _DEngines; 

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

    private void Update()
    {
        PlayerInput();
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
                OnStaticEngines();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                OffStaticEngines();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnRightEngines();
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                OffRightEngines();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnLeftEngines();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                OffLeftEngines();
            }
        }

        if (!_keyboardActive)
        {
            if (_joystick.Vertical > _joystick.DeadZone)
            {
                foreach (Engine engine in _WEngines)
                {
                    OnEngine(engine, _joystick.Vertical);
                }
            }

            if (_joystick.Horizontal < _joystick.DeadZone && _joystick.Vertical < _joystick.DeadZone)
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

    public void OnStaticEngines()
    {
        foreach (Engine engine in _staticEngines)
        {
            OnEngine(engine, 1);
            //engine.StartEngine();
        }
    }

    public void OffStaticEngines()
    {
        foreach (Engine engine in _staticEngines)
        {
            OffEngine(engine);
            //engine.StopEngine();
        }
    }

    public void OnLeftEngines()
    {
        foreach (Engine engine in _leftEngines)
        {
            OnEngine(engine, 1);
            //engine.StartEngine();
        }
    }

    public void OffLeftEngines()
    {
        foreach (Engine engine in _leftEngines)
        {
            OffEngine(engine);
            //engine.StopEngine();
        }
    }

    public void OnRightEngines()
    {
        foreach (Engine engine in _rightEngines)
        {
            OnEngine(engine, 1);
            //engine.StartEngine();
        }
    }

    public void OffRightEngines()
    {
        foreach (Engine engine in _rightEngines)
        {
            OffEngine(engine);
            //engine.StopEngine();
        }
    }

}
