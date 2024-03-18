using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep : MonoBehaviour
{
    public string speech;
    public int currentStepIndex;

    public TutorialStep NextStep { get; set; }


    public TutorialStep(string speech, int currentStepIndex)
    {
        this.speech = speech;
        this.currentStepIndex = currentStepIndex;
    }

    public override string ToString()
    {
        return $"{speech}";
    }
}
