using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
using Unity.Notifications.Android;
#endif

public class AndroidNotificationsController : MonoBehaviour
{
#if UNITY_ANDROID
    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Daily Check In"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text)
    {
        // Calculate the time until noon today or the next day
        var now = DateTime.Now;
        var noonToday = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0); // noon
        DateTime fireTime;

        if (now <= noonToday)
        {
            fireTime = noonToday;
        }
        else
        {
            fireTime = noonToday.AddDays(1); // Schedule for the next day
        }

        var notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            FireTime = fireTime,
            RepeatInterval = new TimeSpan(24, 0, 0) // Repeat daily
        };

        AndroidNotificationCenter.SendNotification(notification, "default_channel");

    }

#endif
}
