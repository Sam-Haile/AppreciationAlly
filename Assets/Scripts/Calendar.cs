using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    // Private fields for storing current date and month details
    [HideInInspector] public DateTime currentDate;
    public Button nextMonthButton;
    public Button prevMonthButton;
    public TextMeshProUGUI monthDisplayText;
    public TransitionManager transitionManager;
    public GameObject currentDayMarker;

    // Array of TextMeshProUGUI to display dates in the calendar
    public TextMeshProUGUI[] dates;

    // Fields related to the calendar's journal entry feature
    public GameObject calendarEntryUI;
    public TextMeshProUGUI date;
    public Image gratitudeLevel;
    public List<GratefulButton> grateful_buttons;
    public TextMeshProUGUI[] grateful_strings;
    public GameObject gratefulFor_str_txt;

    public GameObject calendar;
    public GameObject xButton;

    private void Awake()
    {
        ShowCurrentMonth();
    }

    void Start()
    {
        StartCoroutine(PositionMarkerAfterLayoutUpdate());

        gratefulFor_str_txt.SetActive(false); // Initially hide elements related to journal entry display
        // Add listeners for the next and previous month buttons

    }

    public void ShowCurrentMonth()
    {
        currentDate = DateTime.Now; // Initialize to the current date
        UpdateMonthDisplay();
        GenerateCalendar(currentDate.Year, currentDate.Month);
    }

    IEnumerator PositionMarkerAfterLayoutUpdate()
    {
        // Wait for the end of the frame, after all UI layout calculations have been completed
        yield return new WaitForEndOfFrame();

        // Now position the marker
        if (currentDate.Year == DateTime.Now.Year && currentDate.Month == DateTime.Now.Month)
        {
            PositionCurrentDayMarker();
        }
        else
        {
            currentDayMarker.SetActive(false);
        }
    }

    // Updates the month and year display at the top of the calendar
    void UpdateMonthDisplay()
    {
        monthDisplayText.text = currentDate.ToString("MMMM yyyy");
    }

    // Advances the calendar to the next month
    public void GoToNextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        UpdateMonthDisplay();
        GenerateCalendar(currentDate.Year, currentDate.Month);

        // After updating the calendar display:
        if (currentDate.Year == DateTime.Now.Year && currentDate.Month == DateTime.Now.Month)
        {
            StartCoroutine(PositionMarkerAfterLayoutUpdate());
        }
        else
        {
            currentDayMarker.SetActive(false);
        }

    }

    // Moves the calendar to the previous month
    public  void GoToPreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        UpdateMonthDisplay();
        GenerateCalendar(currentDate.Year, currentDate.Month);

        // After updating the calendar display:
        if (currentDate.Year == DateTime.Now.Year && currentDate.Month == DateTime.Now.Month)
        {
            StartCoroutine(PositionMarkerAfterLayoutUpdate());
        }
        else
        {
            currentDayMarker.SetActive(false);
        }
    }

    // Generates the calendar for a given year and month
    public void GenerateCalendar(int year, int month)
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        int startDayIndex = (int)firstDayOfMonth.DayOfWeek;

        // Clear existing dates and adjust their style to default
        foreach (var text in dates)
        {
            text.text = "";
            ReduceOpacity(text, false); // Reset opacity for new month generation
        }

        // Populate calendar with current month's dates
        for (int day = 1; day <= daysInMonth; day++)
        {
            int i = startDayIndex + day - 1;
            if (i < dates.Length)
            {
                dates[i].text = day.ToString();
                ReduceOpacity(dates[i], false); // Ensure current month's dates are fully visible
                dates[i].GetComponent<Date>().previousMonth = false;
                dates[i].GetComponent<Date>().nextMonth = false;
            }
        }

        // Fill in dates from the previous month for empty cells at the start of the calendar
        DateTime lastDayOfPreviousMonth = firstDayOfMonth.AddDays(-1);
        int daysInPreviousMonth = lastDayOfPreviousMonth.Day;
        for (int i = startDayIndex - 1; i >= 0 && i < dates.Length; i--)
        {
            dates[i].text = (daysInPreviousMonth - (startDayIndex - i - 1)).ToString();
            ReduceOpacity(dates[i], true); // Dim dates not in the current month for visual differentiation
            dates[i].GetComponent<Date>().previousMonth = true;
        }

        // Optionally, fill in dates from the next month for empty cells at the end of the calendar
        int totalDaysFilled = daysInMonth + startDayIndex;
        for (int i = totalDaysFilled; i < dates.Length; i++)
        {
            dates[i].text = (i - totalDaysFilled + 1).ToString();
            ReduceOpacity(dates[i], true); // Dim these dates as well
            dates[i].GetComponent<Date>().nextMonth = true;
        }

        foreach (var day in dates)
        {
            Date dateObj = day.GetComponent<Date>();

            if (dateObj.nextMonth)
            {
                dateObj.currentMonth = currentDate.AddMonths(1).Month.ToString();
                DisplayEmotionsCalendar(GetFilePath(currentDate.AddMonths(1), int.Parse(day.text)), day.gameObject);
            }
            else if (dateObj.previousMonth)
            {
                dateObj.currentMonth = currentDate.AddMonths(-1).Month.ToString();
                DisplayEmotionsCalendar(GetFilePath(currentDate.AddMonths(-1), int.Parse(day.text)), day.gameObject);
            }
            else
            {
                dateObj.currentMonth = currentDate.Month.ToString();
                DisplayEmotionsCalendar(GetFilePath(currentDate, int.Parse(day.text)), day.gameObject);
            }
        }
    }

    public void PositionCurrentDayMarker()
    {
        int today = DateTime.Now.Day;
        string month = DateTime.Now.Month.ToString();

        foreach (var day in dates)
        {
            Date dateObj = day.GetComponent<Date>();
            if (day.text == today.ToString() && dateObj.currentMonth == month)
            {
                // Assuming each 'day' TextMeshProUGUI object is a child of the day button or has a known relative position
                currentDayMarker.transform.position = day.transform.position;
                currentDayMarker.SetActive(true); // Make sure the marker is visible
                break;
            }
        }
    }

    // Adjusts the opacity of a date text, used to visually differentiate days outside the current month
    private void ReduceOpacity(TextMeshProUGUI text, bool darken)
    {
        // Change this method according to how you want to visually differentiate the days
        text.color = darken ? new Color(0f, 0f, 0f, .5f) : Color.black;
    }

    public void OnClick()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        string date = obj.GetComponent<TextMeshProUGUI>().text;
        int.TryParse(date, out int dayIndex);
        string filePath;

        if (obj.GetComponent<Date>().previousMonth)
            filePath = GetFilePath(currentDate.AddMonths(-1), dayIndex);
        else if (obj.GetComponent<Date>().nextMonth)
            filePath = GetFilePath(currentDate.AddMonths(1), dayIndex);
        else
            filePath = GetFilePath(currentDate, dayIndex);

        LoadAndDisplayJournalEntry(filePath, dayIndex);
    }

    // Retrieve file path that was saved from the journal
    public string GetFilePath(DateTime monthYear, int selectedDate)
    {
        string formattedDate = monthYear.ToString($"MMMM {selectedDate:00}, yyyy");
        string path = Application.persistentDataPath + "/journal_" + formattedDate.Replace(" ", "_").Replace(",", "") + ".json";
        return path;
    }

    private void LoadAndDisplayJournalEntry(string filePath, int selectedDate)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JournalEntry entry = JsonUtility.FromJson<JournalEntry>(json);
            DisplayJournalData(entry, selectedDate, calendarEntryUI);
            this.GetComponent<Animator>().SetTrigger("calendarIn");
            xButton.SetActive(false);
            transitionManager.CalendarActive(true);
        }
        else
        {
            transitionManager.CalendarActive(false);
        }
    }

    public void DisplayEmotionsCalendar(string filePath, GameObject dateObj)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JournalEntry journalEntry = JsonUtility.FromJson<JournalEntry>(json);


            if (journalEntry.gratitudeLevel <= .2f)
                dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/ungrateful");
            else if (journalEntry.gratitudeLevel > .2f && journalEntry.gratitudeLevel < .4f)
                dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/aLittleGrateful");
            else if (journalEntry.gratitudeLevel > .4f && journalEntry.gratitudeLevel < .6f)
                dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/kindOfGrateful");
            else if (journalEntry.gratitudeLevel > .6f && journalEntry.gratitudeLevel < .8f)
                dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/grateful");
            else if (journalEntry.gratitudeLevel > .8f && journalEntry.gratitudeLevel < 1f)
                dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/superGrateful");
        }
        else
        {
            dateObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"gratitudeLevel/incomplete");
        }
    }


    private void DisplayJournalData(JournalEntry entry, int selectedDate, GameObject calendarEntryUI)
    {

        date.text = $"{currentDate.ToString("MMMM")} {selectedDate}, {currentDate.Year}";

        if (entry.gratitudeLevel <= .2f)
            gratitudeLevel.sprite = Resources.Load<Sprite>($"gratitudeLevel/ungrateful");
        else if (entry.gratitudeLevel > .2f && entry.gratitudeLevel < .4f)
            gratitudeLevel.sprite = Resources.Load<Sprite>($"gratitudeLevel/aLittleGrateful");
        else if (entry.gratitudeLevel > .4f && entry.gratitudeLevel < .6f)
            gratitudeLevel.sprite = Resources.Load<Sprite>($"gratitudeLevel/kindOfGrateful");
        else if (entry.gratitudeLevel > .6f && entry.gratitudeLevel < .8f)
            gratitudeLevel.sprite = Resources.Load<Sprite>($"gratitudeLevel/grateful");
        else if (entry.gratitudeLevel > .8f && entry.gratitudeLevel < 1f)
            gratitudeLevel.sprite = Resources.Load<Sprite>($"gratitudeLevel/superGrateful");

        // Loop through the grateful buttons in the UI.
        for (int i = 0; i < grateful_buttons.Count; i++)
        {
            // Ensure we have corresponding data for each button.
            if (i < entry.finalButtonsData.Count)
            {
                var buttonData = entry.finalButtonsData[i];

                // Dynamically load the sprite by name. 
                Sprite iconSprite = Resources.Load<Sprite>($"activities/{buttonData.iconSpriteName}");

                // Check if the sprite exists to prevent null reference exceptions.
                if (iconSprite != null)
                {
                    grateful_buttons[i].icon.sprite = iconSprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite not found for: {buttonData.iconSpriteName}");
                }

                // Set the button's text.
                grateful_buttons[i].grtfl_text.text = buttonData.gratefulText;

                // Make sure the button is active and visible.
                grateful_buttons[i].gameObject.SetActive(true);
            }
            else
            {
                // If there's no data for this button, make sure it's inactive.
                grateful_buttons[i].gameObject.SetActive(false);
            }
        }

        // Assuming final_slots_strings is correctly populated from entry.finalSlotsStrings
        // And assuming grateful_strings is an array of TextMeshProUGUI that matches the UI structure
        for (int i = 0; i < grateful_strings.Length; i++)
        {
            // Check if we have corresponding string data.
            if (i < entry.finalSlotsStrings.Length && !string.IsNullOrEmpty(entry.finalSlotsStrings[i]) && entry.finalSlotsStrings[i] != "New Text")
            {
                gratefulFor_str_txt.SetActive(true);

                //
                if (!string.IsNullOrEmpty(entry.finalPromptText))
                    gratefulFor_str_txt.GetComponent<TextMeshProUGUI>().text = entry.finalPromptText;
                else
                    gratefulFor_str_txt.GetComponent<TextMeshProUGUI>().text = "GRATEFUL FOR";

                LoadPreferences.ApplyColors();
                grateful_strings[i].text = entry.finalSlotsStrings[i];
                grateful_strings[i].transform.parent.gameObject.SetActive(true); // Show the text field or its container.
            }
            else
            {
                grateful_strings[i].transform.parent.gameObject.SetActive(false); // Hide the text field or its container if there's no data.
            }
        }

        // Finally, make the UI for displaying the journal entry visible.
        calendarEntryUI.SetActive(true);
    }

}
