using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class JournalEntry : MonoBehaviour
{
    public string date; // YYYY-MM-DD format

    public float gratitudeLevel;
    public List<GratefulButton> final_Buttons;
    public TextMeshProUGUI[] final_slots;

    // Constructor
    public JournalEntry(string date, float sliderValue, List<GratefulButton> final_Buttons, TextMeshProUGUI[] final_slots)
    {
        this.date = date;
        this.gratitudeLevel = sliderValue;
        this.final_Buttons = final_Buttons;
        this.final_slots = final_slots;
    }


}
