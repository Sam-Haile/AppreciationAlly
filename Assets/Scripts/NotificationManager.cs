using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public Animator toggle;

    public void CheckToggle()
    {
        toggle.SetBool("enabled", !toggle.GetBool("enabled"));
        bool isEnabled = toggle.GetBool("enabled");
        this.GetComponent<NativeNotificationsController>().enabled = isEnabled;

        // Set PlayerPrefs according to the new status
        // Save 1 if true (enabled), 0 if false (disabled)
        PlayerPrefs.SetInt("notifications", isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool DetermineToggleStatus()
    {
        // Get the saved notifications status
        int notifsOn = PlayerPrefs.GetInt("notifications");

        // Return true if notifsOn is 1 (meaning notifications are enabled)
        return notifsOn == 1;
    }
}
