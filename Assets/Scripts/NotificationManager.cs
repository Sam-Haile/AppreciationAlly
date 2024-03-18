#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        CreateNotificationChannel();
#endif
    }

#if UNITY_ANDROID
    private void CreateNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "game_notifications",
            Name = "Game Notifications",
            Importance = Importance.Default,
            Description = "Notification channel for game events",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
#endif

#if UNITY_ANDROID
private void ScheduleSimpleNotification()
{
    var notification = new AndroidNotification
    {
        Title = "Time to play!",
        Text = "Don't forget to play your daily challenge.",
        SmallIcon = "icon_small", // Ensure you have this icon in your Android project
        LargeIcon = "icon_large", // Ensure you have this icon in your Android project
        FireTime = System.DateTime.Now.AddSeconds(5),
    };

    AndroidNotificationCenter.SendNotification(notification, "game_notifications");
}
#endif


}




