using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
