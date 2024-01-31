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
    #endregion

    private void Start()
    {
        SetupOnboardingSteps();

    }

    private void SetupOnboardingSteps()
    {
        // Example of setting up steps
        OnboardingStep step1 = new OnboardingStep("Welcome!", "Choose your favorite color", colorOptions);
        OnboardingStep step2 = new OnboardingStep("Step 2", "Subheader for step 2", null); // Replace null with relevant UI elements for step 2
        // ... More steps as needed

        // Linking the steps
        step1.NextStep = step2;
        // ... Link more steps as needed

        // Set the current step
        currentStep = step1;

        UpdateUIForCurrentStep();

    }

    public void GoToNextStep()
    {
        Debug.Log(currentStep.ToString());
        if (currentStep != null && currentStep.NextStep != null)
        {
            currentStep = currentStep.NextStep;
            UpdateUIForCurrentStep();
        }
    }

    private void UpdateUIForCurrentStep()
    {
        header.text = currentStep.HeaderText;
        subHeader.text = currentStep.SubHeaderText;

        // Update UI elements specific to the current step
        // For example, activate/deactivate certain UI elements
        foreach (var element in colorOptions)
        {
            element.SetActive(false);
        }

        if (currentStep.UiElements != null)
        {
            foreach (var element in currentStep.UiElements)
            {
                element.SetActive(true);
            }
        }
    }




    #region Step 1 (Color Selection)
    public void SetColor(BackgroundColor color)
    {
        ColorUtility.TryParseHtmlString("#" + color, out selectedColor);

        foreach (var colorOption in colorOptions)
        {
            if (colorOption.GetComponent<BackgroundColor>().hexCode != color.hexCode)
                StartCoroutine(InterpolateScale(colorOption, new Vector3(.75f, .75f, 1f), .15f));
            else
                StartCoroutine(InterpolateScale(colorOption, new Vector3(1f, 1f, 1f), .15f));
        }

        SetFavoriteColor();
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

    private void SetFavoriteColor()
    {
        PlayerPrefs.SetString("FavoriteColor", ColorUtility.ToHtmlStringRGBA(selectedColor));
        //PlayerPrefs.Save();
    }
    #endregion

}

