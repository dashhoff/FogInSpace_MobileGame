using UnityEngine;

public class VibrationController : MonoBehaviour
{
    public static VibrationController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void OnEnable()
    {
        EventController.ShipDamaget += Vibrate;
    }

    private void OnDisable()
    {
        EventController.ShipDamaget += Vibrate;
    }

    public static void Vibrate()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
                {
                    if (vibrator.Call<bool>("hasVibrator"))
                        vibrator.Call("vibrate", 500);
                }
            }
        }
    }

    public static void Vibrate(float milliseconds)
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
                {
                    if (vibrator.Call<bool>("hasVibrator"))
                        vibrator.Call("vibrate", milliseconds);
                }
            }
        }
    }
}
