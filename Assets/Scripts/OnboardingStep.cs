using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingStep : MonoBehaviour
{
    public string HeaderText;
    public string SubHeaderText;
    public int currentStepIndex;

    public OnboardingStep NextStep { get; set; }
    public OnboardingStep PreviousStep { get; set; }
    public OnboardingStep YesPath { get; set; } // For tutorial
    public OnboardingStep NoPath { get; set; }   // For skipping tutorial




    public OnboardingStep(string headerText, string subHeaderText, int currentStepIndex)
    {
        HeaderText = headerText;
        SubHeaderText = subHeaderText;
        this.currentStepIndex = currentStepIndex;
    }

    public override string ToString()
    {
        return $"HeaderText: {HeaderText}, SubHeaderText: {SubHeaderText}";
    }
}
