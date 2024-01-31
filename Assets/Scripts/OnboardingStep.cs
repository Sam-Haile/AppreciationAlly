using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingStep : MonoBehaviour
{
    public string HeaderText;
    public string SubHeaderText;
    public GameObject[] UiElements;

    public OnboardingStep NextStep;

    public OnboardingStep(string headerText, string subHeaderText, GameObject[] uiElements)
    {
        HeaderText = headerText;
        SubHeaderText = subHeaderText;
        UiElements = uiElements;
    }

    public override string ToString()
    {
        return $"HeaderText: {HeaderText}, SubHeaderText: {SubHeaderText}";
    }
}
