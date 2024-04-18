using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public Animator toggle;

    private void Start()
    {
        int notifsOn = PlayerPrefs.GetInt("notifications");

        if (notifsOn == 1)
            toggle.SetTrigger("on");
        else
            toggle.SetTrigger("off");
    }

    public void CheckToggle()
    {
        toggle.SetBool("enabled", !toggle.GetBool("enabled"));
        bool isEnabled = toggle.GetBool("enabled");
        this.GetComponent<NativeNotificationsController>().enabled = isEnabled;

        // Convert boolean to integer (1 if true, 0 if false)
        PlayerPrefs.SetInt("notifications", isEnabled ? 0 : 1);

        PlayerPrefs.Save();
    }
}
