using UnityEngine;

public class Saves : MonoBehaviour
{
    public static Saves Instance;

    public static int Level = 1;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetLevel(int newValue)
    {
        Level = newValue;
    }
}
