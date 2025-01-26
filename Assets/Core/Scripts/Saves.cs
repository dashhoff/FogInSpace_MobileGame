using UnityEngine;

public class Saves : MonoBehaviour
{
    public static Saves Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
