using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalStep : MonoBehaviour
{

    public string headerText;
    public string subheaderText;
    public GameObject stepUI;
    public int currentStepIndex;

    public JournalStep NextStep { get; set; }
    public JournalStep PreviousStep { get; set; }
    public JournalStep gratefulPath { get; set; } 
    public JournalStep ungratefulPath { get; set; }

    public JournalStep(string headerText, string subheaderText, GameObject uiElements, int currentStepIndex)
    {
        this.headerText = headerText;
        this.stepUI= uiElements;
        this.subheaderText = subheaderText;
        this.currentStepIndex = currentStepIndex;
    }

    public override string ToString()
    {
        return $"HeaderText: {headerText}";
    }
}
