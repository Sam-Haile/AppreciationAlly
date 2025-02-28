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

    // Start is called before the first frame update
    void Start()
    {
        SetupGridGameTutorial();
    }

    public void SetupGridGameTutorial()
    {
        TutorialStep step0 = new TutorialStep("Welcome to the Grid Game", 0);
        TutorialStep step1 = new TutorialStep("Here you can practice appreciation", 1);
        TutorialStep step2 = new TutorialStep("To start, select how many sets of images you would like to play through", 2);
        TutorialStep step3 = new TutorialStep("Then, play by selecting which of the 4 images you are most grateful for", 3);
        TutorialStep step4 = new TutorialStep("Take a moment after each selection to reflect on what you are grateful for", 4);
        TutorialStep step5 = new TutorialStep("Come back every day to practice some appreciation", 5);
        TutorialStep step6 = new TutorialStep("Come back every day to practice some appreciation", 6);

        step0.NextStep = step1;
        step1.NextStep = step2;
        step2.NextStep = step3;
        step3.NextStep = step4;
        step4.NextStep = step5;
        step5.NextStep = step6;
        step6.NextStep = null;

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
            //Save the grid game tutorial completion status using PlayerPrefs.
            PlayerPrefs.SetInt("GridGameTutorialDone", 1);
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
