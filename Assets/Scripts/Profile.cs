using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Calendar calendar;

    public Image[] sevenDayTracker;
    public TextMeshProUGUI[] sevenDaystrings;
    public TextMeshProUGUI streak;
    private int streakCounter;
    private Color customGreen = new Color(122f / 255f, 236f / 255f, 93f / 255f);

    private void Start()
    {
        UpdateStreak();
        DetermineStreak();
    }

    public void UpdateStreak()
    {
        // Use DateTime.Now to get the current system date and time
        DateTime currentDate = DateTime.Now;

        for (int i = 0; i < sevenDaystrings.Length; i++)
        {
            // Calculate the date to display, starting from the current day and going back
            DateTime dateToShow = currentDate.AddDays(-i);
            // Update the text of the UI element to show the day of the week
            sevenDaystrings[sevenDaystrings.Length - 1 - i].text = dateToShow.ToString("ddd").Substring(0, 1);

            CheckForData(currentDate, dateToShow.Day, i);
        }

    }

    private bool CheckForData(DateTime monthYear, int selectedDate, int index)
    {
        string filePath = calendar.GetFilePath(monthYear, selectedDate);

        if (File.Exists(filePath))
        {
            sevenDayTracker[sevenDaystrings.Length - 1 - index].color = customGreen;
            return true;
        }
        else
            return false;
    }

    private void DetermineStreak()
    {
        streakCounter = 0;

        for (int i = sevenDayTracker.Length - 1; i >= 0; i--)
        {
            Debug.Log(i + " Color = " + sevenDayTracker[i].color.ToString());
            if (sevenDayTracker[i].color == customGreen)
            {
                streakCounter++;
            }
            else
            {
                break;
            }
        }

        streak.text = streakCounter.ToString();
    }

}
