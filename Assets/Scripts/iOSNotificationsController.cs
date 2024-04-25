using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class iOSNotificationsController : MonoBehaviour
{
#if UNITY_IOS

    public IEnumerator RequestAuthorization()
    {
        using var req = new AuthorizationRequest(
            AuthorizationOption.Alert | AuthorizationOption.Badge, 
            true);

        while(!req.IsFinished) 
        {
            yield return null;
        }

    }

    public void SendNotification(string title, string body, string subtitle)
    {
        var now = DateTime.Now;
        var noonToday = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0); // noon
        TimeSpan timeUntilNoon;

        if (now <= noonToday)
        {
            timeUntilNoon = noonToday - now;
        }
        else
        {
            timeUntilNoon = noonToday.AddDays(1) - now; // Schedule for the next day
        }


        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = TimeSpan.FromHours(24),
            Repeats = true
        };

        var notification = new iOSNotification
        {
            Identifier = "daily_check_in",
            Title = title,
            Body = body,
            Subtitle = subtitle,
            ShowInForeground = true,
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
            CategoryIdentifier = "default_category",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
