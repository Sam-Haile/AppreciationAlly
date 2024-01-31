using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingStep : MonoBehaviour
{
    public string HeaderText;
    public string SubHeaderText;
    public GameObject[] UiElements;
    public int currentStepIndex;

    public OnboardingStep NextStep { get; set; }
    public OnboardingStep PreviousStep { get; set; } 



    public OnboardingStep(string headerText, string subHeaderText, GameObject[] uiElements, int currentStepIndex)
    {
        HeaderText = headerText;
        SubHeaderText = subHeaderText;
        UiElements = uiElements;
        this.currentStepIndex = currentStepIndex;
    }

    public override string ToString()
    {
        return $"HeaderText: {HeaderText}, SubHeaderText: {SubHeaderText}";
    }
}
