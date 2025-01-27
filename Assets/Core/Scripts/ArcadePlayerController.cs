using UnityEngine;

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

    [Space(20f)]
    [SerializeField] private Rigidbody _rb;

    [Space(20f)]
    [SerializeField] private Joystick _joystick;

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
        Vector2 joystickInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        if (_joystick.Vertical > _joystick.DeadZone 
            || _joystick.Vertical < -_joystick.DeadZone
            || _joystick.Horizontal > _joystick.DeadZone
            || _joystick.Horizontal < -_joystick.DeadZone)
        {
            foreach (var engine in _rotatingEngines) 
            {
                engine.RotateEngine(joystickInput);
                engine.StartEngine();
            }
        }

        if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            foreach (Engine engine in _engines)
            {
                OffEngine(engine);
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
}
