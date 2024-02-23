using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Onboarding : MonoBehaviour
{

    private OnboardingStep currentStep;
    public TextMeshProUGUI header;
    public TextMeshProUGUI subHeader;
    #region Step 1
    private UnityEngine.Color selectedPrimaryColor;
    private UnityEngine.Color selectedSecondaryColor;
    public GameObject[] colorOptions;
    public GameObject[] profilePictures;
    public Image backgroundColor;
    public Image buttonPrefab;
    public Image inputPrefab;
    public TextMeshProUGUI inputText;
    public Button nextButton;
    private string userName;
    private int userPfpId;
    public bool userChoice;

    #endregion

    private void Start()
    {
        SetupOnboardingSteps();
    }

    private void SetupOnboardingSteps()
    {
        // Example of setting up steps
        OnboardingStep step1 = new OnboardingStep("Welcome Aboard!", "Begin your journey towards better mental well-being with Appreciation Alley. Cultivating daily gratitude and positivity.",   1);
        OnboardingStep step2 = new OnboardingStep("Choose your favorite color", "You can change this later",   2);
        OnboardingStep step3 = new OnboardingStep("Tell me about yourself!", "You can change this later",   3);
        OnboardingStep step4 = new OnboardingStep("", "Hi friend! I'm Chromo, your guide to a world of fun, feelings, and fantastic adventures!",   4);
        OnboardingStep step5 = new OnboardingStep("", "Would you like a tour of the app?",   5);

        OnboardingStep yesPath = new OnboardingStep("", "Great! Let's get this adventure rolling!",   6);
        OnboardingStep noPath = new OnboardingStep("", "Alright, If you ever want to do the tutorial head over to the settings!",   6);

        // Linking the steps
        step1.NextStep = step2;
        step2.PreviousStep = step1;
        step2.NextStep = step3;
        step3.PreviousStep = step2;
        step3.NextStep = step4;
        step4.PreviousStep = step3;
        step4.NextStep = step5;
        step5.PreviousStep = step4;
        step5.NextStep = noPath;
        step5.YesPath = yesPath;
        step5.NoPath = noPath;

        // Set the current step
        currentStep = step1;


        //Defalt values
        userPfpId = 1;
        userName = "Friend";


        UpdateUIForCurrentStep();
    }


    public void GoToNextStep()
    {
        if ( currentStep.currentStepIndex < 6 && currentStep.ToString() != null && currentStep.NextStep.ToString() != null)
        {

            if (currentStep.currentStepIndex == 5 && userChoice)
            {
                currentStep = currentStep.YesPath;
                UpdateUIForCurrentStep();
            }
            else if (currentStep.currentStepIndex == 5 && !userChoice)
            {
                currentStep = currentStep.NoPath;
                UpdateUIForCurrentStep();
            }
            else
            {
                currentStep = currentStep.NextStep;
                UpdateUIForCurrentStep();
            }
        }
    }

    public void GoToPreviousStep()
    {
        if (currentStep.ToString() != null && currentStep.PreviousStep.ToString() != null)
        {
            currentStep = currentStep.PreviousStep;
        }
    }

    private void UpdateUIForCurrentStep()
    {
        header.text = currentStep.HeaderText;
        subHeader.text = currentStep.SubHeaderText;

    }

    public void SetTutorialChoice(bool choice)
    {
        userChoice = choice;
    }

    public void UpdatePreferences()
    {
        switch (currentStep.currentStepIndex)
        {
            case 1:
                break;
            case 2:
                PlayerPrefs.SetString("PrimaryColor", ColorUtility.ToHtmlStringRGBA(selectedPrimaryColor));
                PlayerPrefs.SetString("SecondaryColor", ColorUtility.ToHtmlStringRGBA(selectedSecondaryColor));
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetString("Name", userName);
                PlayerPrefs.SetInt("PlayerPFP", userPfpId);
                PlayerPrefs.Save();
                break;
        }
    }

    public void SetPfp(ProfilePicture selectedPfp)
    {
        foreach (var pfp in profilePictures)
        {
            int id = pfp.GetComponent<ProfilePicture>().id;

            if (id != selectedPfp.id)
                StartCoroutine(InterpolateScale(pfp.gameObject, new Vector3(.75f, .75f, 1f), .15f));
            else
            {
                StartCoroutine(InterpolateScale(pfp.gameObject, new Vector3(1f, 1f, 1f), .15f));
                userPfpId = id;
            }
        }
    }

    public void TurnOffPage(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void MoveHeaders()
    {
        header.transform.position = new Vector2(header.transform.position.x, 535);
        subHeader.transform.position = new Vector2(subHeader.transform.position.x, 515);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    #region Step 1 (Color Selection)
    public void SetColor(BackgroundColor color)
    {
        ColorUtility.TryParseHtmlString("#" + color.primaryColor, out selectedPrimaryColor);
        ColorUtility.TryParseHtmlString("#" + color.secondaryColor, out selectedSecondaryColor);
        backgroundColor.color = selectedPrimaryColor;
        header.color = selectedPrimaryColor;
        subHeader.color = selectedPrimaryColor;
        buttonPrefab.color = selectedPrimaryColor;
        inputPrefab.color = selectedSecondaryColor;

        foreach (var colorOption in colorOptions)
        {
            if (colorOption.GetComponent<BackgroundColor>().primaryColor != color.primaryColor)
                StartCoroutine(InterpolateScale(colorOption, new Vector3(.75f, .75f, 1f), .15f));
            else
                StartCoroutine(InterpolateScale(colorOption, new Vector3(1f, 1f, 1f), .15f));
        }

    }

    private IEnumerator InterpolateScale(GameObject objToTransform, Vector3 targetScale, float duration)
    {
        Vector3 originalScale = objToTransform.transform.localScale;
        float timer = 0;

        while (timer < duration)
        {
            objToTransform.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is set to the exact target scale at the end
        objToTransform.transform.localScale = targetScale;

    }

    #endregion


    #region Step2 (Name Selection)
    public void CheckNameValid(TextMeshProUGUI input)
    {
        // Check if input is valid and enable/disable the button accordingly
        if (input != null && input.text.Length >= 3)
        {
            nextButton.interactable = true;
            userName = input.text;
            // Consider saving the input when it is valid and, typically, after some user action like a button press
        }
        else
        {
            nextButton.interactable = false;
        }
    }

    public void SetButtonOff(Button button)
    {
        button.interactable = false;
    }

    public void SetNextButtonOff(string status)
    {
        if (status == "on")
            nextButton.interactable = true;
        else
            nextButton.interactable = false;
    }
    #endregion

}

