using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Onboarding : MonoBehaviour
{

    private OnboardingStep currentStep;
    public TextMeshProUGUI header;
    public TextMeshProUGUI subHeader;
    #region Step 1
    private Color selectedColor;
    public GameObject[] colorOptions;
    public Image backgroundColor;
    public Image buttonPrefab;
    public Image inputPrefab;
    private string name;
    #endregion

    private void Start()
    {
        SetupOnboardingSteps();
    }

    private void SetupOnboardingSteps()
    {
        // Example of setting up steps
        OnboardingStep step1 = new OnboardingStep("Welcome Aboard!", "Begin your journey towards better mental well-being with Appreciation Alley. Cultivating daily gratitude and positivity.", null, 1);
        OnboardingStep step2 = new OnboardingStep("Choose your favorite color", "You can change this later", null, 2); // Replace null with relevant UI elements for step 2
        OnboardingStep step3 = new OnboardingStep("Tell me about yourself!", "You can change this later", null, 3); // Replace null with relevant UI elements for step 2
        OnboardingStep step4 = new OnboardingStep("!", "You can change this later", null, 3); // Replace null with relevant UI elements for step 2
        // ... More steps as needed

        // Linking the steps
        step1.NextStep = step2;
        step2.PreviousStep = step1;
        step2.NextStep = step3;
        step3.PreviousStep = step2;
        step3.NextStep= step4;
        step4.PreviousStep = step3;


        // Set the current step
        currentStep = step1;

        UpdateUIForCurrentStep();

    }

    public void GoToNextStep()
    {
        if (currentStep.ToString() != null && currentStep.NextStep.ToString() != null)
        {
            currentStep = currentStep.NextStep;
            UpdateUIForCurrentStep();
        }
    }

    public void GoToPreviousStep()
    {
        if (currentStep.ToString() != null && currentStep.PreviousStep.ToString() != null)
        {
            currentStep = currentStep.PreviousStep;
            UpdateUIForCurrentStep();
        }
    }

    private void UpdateUIForCurrentStep()
    {
        header.text = currentStep.HeaderText;
        subHeader.text = currentStep.SubHeaderText;

        // Update UI elements specific to the current step
        // For example, activate/deactivate certain UI elements
        //foreach (var element in colorOptions)
        //{
        //    element.SetActive(false);
        //}

        if (currentStep.UiElements != null)
        {
            foreach (var element in currentStep.UiElements)
            {
                element.SetActive(true);
            }
        }
    }


    public void UpdatePreferences()
    {
        switch (currentStep.currentStepIndex)
        {
            case 1:
                break;
            case 2:
                PlayerPrefs.SetString("FavoriteColor", ColorUtility.ToHtmlStringRGBA(selectedColor));
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetString("Name", name);
                PlayerPrefs.Save();
                break;
            case 4:
                break;
        }

    }

    public void TurnOffPage(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void MoveHeaders()
    {
        header.transform.position = new Vector2(header.transform.position.x , 755);
        subHeader.transform.position = new Vector2(subHeader.transform.position.x ,  735);
    }

    #region Step 1 (Color Selection)
    public void SetColor(BackgroundColor color)
    {
        ColorUtility.TryParseHtmlString("#" + color.hexCode, out selectedColor);
        backgroundColor.color = selectedColor;
        header.color = selectedColor;
        subHeader.color = selectedColor;
        buttonPrefab.color = selectedColor;
        inputPrefab.color = selectedColor;

        foreach (var colorOption in colorOptions)
        {
            if (colorOption.GetComponent<BackgroundColor>().hexCode != color.hexCode)
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


    #region Step2
    public void CheckNameValid(TextMeshProUGUI input)
    {
        Button nextButton = buttonPrefab.GetComponent<Button>();

        // Check if input is valid and enable/disable the button accordingly
        if (input != null && input.text.Length >= 3)
        {
            nextButton.interactable = true;
            name = input.text;
            // Consider saving the input when it is valid and, typically, after some user action like a button press
        }
        else
        {
            nextButton.interactable = false;
        }
    }
    #endregion

}

