using UnityEngine;

public class NativeNotificationsController : MonoBehaviour
{
    [SerializeField]
    private AndroidNotificationsController androidNotificationsController;
    [SerializeField]
    private iOSNotificationsController iOSNotificationsController;

    private void Start()
    {
#if UNITY_ANDROID
        androidNotificationsController.RequestAuthorization();
        androidNotificationsController.RegisterNotificationChannel();
        CheckAndSendAndroidNotification();
#elif UNITY_IOS
        StartCoroutine(iOSNotificationsController.RequestAuthorization());
        CheckAndSendiOSNotification();
#endif
    }

#if UNITY_ANDROID
    private void CheckAndSendAndroidNotification()
    {
        if (PlayerPrefs.GetInt("notifications", 1) == 1) // Default to 1 (on) if not set
        {
            androidNotificationsController.SendNotification("Brighten Your Day!",
                "Tap here to complete your daily tasks and keep your positivity streak going!");
        }
    }
#elif UNITY_IOS

    private void CheckAndSendiOSNotification()
    {
        if (PlayerPrefs.GetInt("notifications", 1) == 1) // Default to 1 (on) if not set
        {
            iOSNotificationsController.SendNotification("Brighten Your Day!",
                "Your daily tasks await!", "Tap here to complete your daily tasks and keep your positivity streak going");
        }
    }
#endif

}
