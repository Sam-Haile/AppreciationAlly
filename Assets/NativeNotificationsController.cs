using System.Collections;
using System.Collections.Generic;
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
        androidNotificationsController.SendNotification("Hello World!", 
            "This is anotification sent from my unity app", 3);
        #elif UNITY_IOS
        StartCoroutine(iOSNotificationsController.RequestAuthorization());
        iOSNotificationsController.SendNotification("Hello World!",
            "This is body of notification", "This is subtitle", 3);
        #endif
    }
}

