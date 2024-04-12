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

    public void SendNotification(string title, string body, string subtitle, int fireTimeInSeconds)
    {
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new System.TimeSpan(0, 0, fireTimeInSeconds),
            Repeats = false
        };

        var notification = new iOSNotification
        {
            Identifier = "hello_world_notification",
            Title = title,
            Body = body,
            Subtitle = subtitle,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "default_category",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger
        };
    }
#endif
}
