using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridGameTutorial : MonoBehaviour
{
    public GameObject tutorialPage;
    public GameObject blackBg;
    public Animator tutorialAnimator;
    private TutorialStep currentStep;
    public TextMeshProUGUI speechBubble;
    public GameObject calendarUI;

    // Start is called before the first frame update
    void Start()
    {
        SetupGridGameTutorial();
    }

    public void SetupGridGameTutorial()
    {
        TutorialStep step0 = new TutorialStep("Welcome to the homepage for Appreciation Ally", 0);
        TutorialStep step1 = new TutorialStep("Here you can find your daily sprinkle of inspiration", 1);

        step0.NextStep = step1;
        step1.NextStep = null;

        currentStep = step0;

        UpdateText();
        UpdateUI();
    }
    public void NextSentence()
    {
        if (currentStep.currentStepIndex < 6 && currentStep.ToString() != null)
        {
            currentStep = currentStep.NextStep;
            UpdateText();
        }
        else
        {
            tutorialPage.gameObject.SetActive(false);
            // Save the grid game tutorial completion status using PlayerPrefs.
            //PlayerPrefs.SetInt("GridGameTutorialDone", 1);
            PlayerPrefs.Save();
        }
    }

    private void UpdateText()
    {
        speechBubble.text = currentStep.ToString();
    }

    public void OnClick()
    {
        NextSentence();
        UpdateText();
        UpdateUI();

        if (currentStep.currentStepIndex == 1)
        {
            blackBg.SetActive(false);
        }

        if (currentStep.currentStepIndex <= 6)
            tutorialAnimator.SetTrigger(currentStep.currentStepIndex.ToString());

    }


    private void UpdateUI()
    {
        switch (currentStep.currentStepIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }
}
