using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator canvasAnimator;

    public bool settingsActive;
    public bool calendarEntryActive;
    public GameObject calendarPage;

    public GameObject profileArea;
    public GameObject settings;
    public GameObject gameObj;
    public GameObject backButton;
    public GameObject attribution;


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

    public void MoodTrackerButton()
    {
        if (settingsActive)
        {
            canvasAnimator.SetTrigger("settingsOut");
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

    public void RestoreSettings()
    {
        profileArea.SetActive(true);
        settings.SetActive(true);
        gameObj.SetActive(false);
        backButton.SetActive(false);
        attribution.SetActive(false);
    }
}
