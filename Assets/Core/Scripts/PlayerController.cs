using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Engine[] _engines;

    [SerializeField] private Engine[] _staticEngines;
    [SerializeField] private Engine[] _rotatingEngines;

    [SerializeField] private Engine[] _leftEngines;
    [SerializeField] private Engine[] _rightEngines;

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
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

    public void OnEngine(Engine engine)
    {
        engine.StartEngine();
    }

    public void OffEngine(Engine engine)
    {
        engine.StopEngine();
    }

    public void OnStaticEngines()
    {
        foreach (Engine engine in _staticEngines)
        {
            engine.StartEngine();
        }
    }

    public void OffStaticEngines()
    {
        foreach (Engine engine in _staticEngines)
        {
            engine.StopEngine();
        }
    }

    public void OnLeftEngines()
    {
        foreach (Engine engine in _leftEngines)
        {
            engine.StartEngine();
        }
    }

    public void OffLeftEngines()
    {
        foreach (Engine engine in _leftEngines)
        {
            engine.StopEngine();
        }
    }

    public void OnRightEngines()
    {
        foreach (Engine engine in _rightEngines)
        {
            engine.StartEngine();
        }
    }

    public void OffRightEngines()
    {
        foreach (Engine engine in _rightEngines)
        {
            engine.StopEngine();
        }
    }

}
