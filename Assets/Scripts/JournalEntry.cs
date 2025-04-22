using System;
using System.Collections.Generic;

[Serializable]
public class JournalEntry
{
    public string date;
    public float gratitudeLevel;
    public List<GratefulButtonData> finalButtonsData;
    public string[] finalSlotsStrings;
    public string finalPromptText;

    public JournalEntry(string date, float sliderValue, List<GratefulButtonData> finalButtonsData, string[] finalSlotsStrings, string finalPromptText)
    {
        this.date = date;
        this.gratitudeLevel = sliderValue;
        this.finalButtonsData = finalButtonsData;
        this.finalSlotsStrings = finalSlotsStrings;
        this.finalPromptText = finalPromptText;
    }
}

