using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        CreateNotificationChannel();
        ScheduleNotification();
    }

    void CreateNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void ScheduleNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Energy Recharged!";
        notification.Text = "Your energy is fully recharged. Come back and play!";
        notification.FireTime = System.DateTime.Now.AddMinutes(60);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
