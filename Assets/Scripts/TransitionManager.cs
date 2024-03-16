using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator canvasAnimator;

    public bool settingsActive;
    public bool calendarEntryActive;
    public GameObject calendarPage;


    public void HomeButton()
    {
        if (settingsActive)
        {
            canvasAnimator.SetTrigger("settingsOut");
        }

        if(calendarPage.activeSelf)
        {
            calendarPage.SetActive(false);
        }

        if (calendarEntryActive)
        {
            canvasAnimator.SetTrigger("calendarOut");
        }
    }

    public void SettingsActive(bool isActive)
    {
        settingsActive = isActive;
    }

    public void CalendarActive(bool isActive)
    {
        calendarEntryActive = isActive;
    }
}
