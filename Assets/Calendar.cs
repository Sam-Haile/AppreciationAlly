using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    private DateTime currentMonth;

    public Button nextMonthButton;
    public Button prevMonthButton;
    public TextMeshProUGUI monthDisplayText;

    void Start()
    {
        currentMonth = DateTime.Now; // Start with the current month
        UpdateMonthDisplay();

        nextMonthButton.onClick.AddListener(GoToNextMonth);
        prevMonthButton.onClick.AddListener(GoToPreviousMonth);
    }

    void UpdateMonthDisplay()
    {
        // Display the month and year (e.g., March 2024)
        monthDisplayText.text = currentMonth.ToString("MMMM yyyy");
    }

    void GoToNextMonth()
    {
        currentMonth = currentMonth.AddMonths(1);
        UpdateMonthDisplay();
    }

    void GoToPreviousMonth()
    {
        currentMonth = currentMonth.AddMonths(-1);
        UpdateMonthDisplay();
    }
}
