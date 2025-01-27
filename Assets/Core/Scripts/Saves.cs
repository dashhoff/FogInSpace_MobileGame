using UnityEngine;

public class Saves : MonoBehaviour
{
    public static Saves Instance;

    public static int Level;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
