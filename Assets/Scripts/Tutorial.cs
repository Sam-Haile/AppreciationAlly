using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPage;
    public GameObject blackBg;
    public Animator tutorialAnimator;
    private TutorialStep currentStep;
    public TextMeshProUGUI speechBubble;
    private void Start()
    {
        SetupTutorial();
    }

    public void SetupTutorial()
    {

        TutorialStep step0 = new TutorialStep("Welcome to the homepage for Appreciation Alley", 0);
        TutorialStep step1 = new TutorialStep("Here you can find your daily sprinkle of inspiration", 1);
        TutorialStep step2 = new TutorialStep("This is the grid game, play it whenever you want to practice gratitude!", 2);
        TutorialStep step3 = new TutorialStep("This is your Journal! Every day, you can add what your grateful for.", 3);
        TutorialStep step4 = new TutorialStep("This calendar lets you track and view your journal entries.", 4);
        TutorialStep step5 = new TutorialStep("Theres so much to explore in Appreciation Alley! Have fun!", 5);
        TutorialStep step6 = new TutorialStep("Theres so much to explore in Appreciation Alley! Have fun!", 6);

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
            // Save the tutorial completion status using PlayerPrefs.
            PlayerPrefs.SetInt("TutorialDone", 1);
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
