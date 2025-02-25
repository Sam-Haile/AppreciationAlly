using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPreferences : MonoBehaviour
{
    public Image[] selectedPfp;

    public Sprite[] pfps;

    public TextMeshProUGUI greeting;
    public TextMeshProUGUI settingsName;
    public static string userName;

    public GameObject tutorialScreen;
    public GameObject gridGameTutorialScreen;

    public Toggle tog;
    public Animator notifTog;

    public bool isDarkMode;
    public Toggle darkModeToggle;

    private void Start()
    {
        if (tutorialScreen != null)
        {
            if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
                tutorialScreen.SetActive(true);
            else
                tutorialScreen.SetActive(false);
        }

        if(gridGameTutorialScreen != null)
        {
            if(PlayerPrefs.GetInt("GridGameTutorialDone", 0) == 0)
                gridGameTutorialScreen.SetActive(true);
            else
                gridGameTutorialScreen.SetActive(false);
        }

        CheckToggle();

        if (notifTog != null)
        {

            if (NotificationManager.DetermineToggleStatus())
                notifTog.SetTrigger("on");
            else
                notifTog.SetTrigger("off");
        }

        ImageManager.LoadImageStates();

        ApplyName();

        ApplyPfp();

        ApplyColors();

        ApplyDarkMode();
    }


    public void LoadSceneIndex(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ApplyName()
    {
        if (greeting != null)
        {
            //Apply users username
            userName = PlayerPrefs.GetString("Name", "Friend");
            greeting.text = "Hello " + userName + "!";
            settingsName.text = userName;
        }
    }

    public void ApplyPfp()
    {
        //Apply users pfp
        foreach (var p in selectedPfp)
        {
            if (p != null && pfps != null)
            {

                int userPfpId = PlayerPrefs.GetInt("PlayerPfp", -1);

                if (userPfpId >= 0 && userPfpId < pfps.Length)
                {
                    p.sprite = pfps[userPfpId];
                }
                else
                {
                    Debug.LogWarning("Pfp Index Out of Range");
                    p.sprite = pfps[0];
                }
            }
        }
    }

    public static void ApplyColors()
    {
        //Apply users color
        string primaryColor = PlayerPrefs.GetString("PrimaryColor", "0E46A7");
        string secondaryColor = PlayerPrefs.GetString("SecondaryColor", "0E46A7");
        var uiElementsPrimary = GameObject.FindGameObjectsWithTag("PrimaryColor");
        var uiElementsSecondary = GameObject.FindGameObjectsWithTag("SecondaryColor");

        Color primColor;
        Color secColor;

        if (ColorUtility.TryParseHtmlString("#" + primaryColor, out primColor) && ColorUtility.TryParseHtmlString("#" + secondaryColor, out secColor))
        {
            foreach (var uiElement in uiElementsPrimary)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.color = primColor;
                }
                else
                {
                    var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                    if (textMeshComponent != null)
                    {
                        textMeshComponent.color = primColor;
                    }
                }
            }

            foreach (var uiElement in uiElementsSecondary)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.color = secColor;
                }
                else
                {
                    var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                    if (textMeshComponent != null)
                    {
                        textMeshComponent.color = secColor;
                    }
                }
            }

        }
        else
        {
            Debug.LogError("Invalid color hex code.");
        }

    }

    public void OnRotationToggleChanged()
    {
        Rotate.ToggleRotation(!tog.isOn);
    }

    public void CheckToggle()
    {
        if (tog != null)
        {
            if (PlayerPrefs.GetInt("RotationEnabled", 0) == 0)
                tog.isOn = true;
            else
                tog.isOn = false;
        }
    }

    public void ToggleDarkMode()
    {
        //toggle between light mode and dark mode
        isDarkMode = !isDarkMode;
        //Debug.Log(isDarkMode);

        //save isDarkMode
        if (isDarkMode)
            PlayerPrefs.SetInt("isDarkModeEnabled", 1);
        else
            PlayerPrefs.SetInt("isDarkModeEnabled", 0);

        //apply dark mode
        ApplyDarkMode();
    }

    public void ApplyDarkMode()
    {
        //make sure that dark mode toggle is displaying correctly
        CheckDarkModeToggle();

        //if loaded isDarkMode value is set to true,...
        if (PlayerPrefs.GetInt("isDarkModeEnabled", 0) == 1)
        {
            isDarkMode = true;
            //Debug.Log("dark mode is ON");

            //***** Make all "BackgroundColor" elements gray *****
            var uiElementsBackground = GameObject.FindGameObjectsWithTag("BackgroundColor");

            foreach (var uiElement in uiElementsBackground)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if (imageComponent != null)
                {
                    //Debug.Log("Found image with name: " + uiElement.name);
                    Color newCol;
                    if (ColorUtility.TryParseHtmlString("#333333", out newCol))
                        imageComponent.color = newCol;
                }
            }

            //***** Make all "TextColor" elements white *****
            var uiElementsText = GameObject.FindGameObjectsWithTag("TextColor");

            foreach (var uiElement in uiElementsText)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if(imageComponent != null)
                {
                    imageComponent.color = Color.white;
                }
                //if (uiElement.name == "Settings")
                //    Debug.Log("Found" + uiElement.name);
                var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                if(textMeshComponent != null)
                {
                    //textMeshComponent.overrideColorTags = true;
                    textMeshComponent.color = Color.white;
                }
            }

            //***** Make all "ReverseTextColor" elements black *****
            var uiElementsReverseText = GameObject.FindGameObjectsWithTag("ReverseTextColor");

            foreach (var uiElement in uiElementsReverseText)
            {
                var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                if (textMeshComponent != null)
                {
                    textMeshComponent.color = Color.black;
                }
            }
        }
        //else loaded isDarkMode value is set to false,...
        else
        {
            isDarkMode = false;
            //Debug.Log("dark mode is OFF");

            //***** Make all "BackgroundColor" elements white *****
            var uiElementsBackground = GameObject.FindGameObjectsWithTag("BackgroundColor");

            foreach (var uiElement in uiElementsBackground)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if (imageComponent != null)
                {
                    //Debug.Log("Found image with name: " + uiElement.name);
                    imageComponent.color = Color.white;
                }
            }

            //***** Make all "TextColor" elements black *****
            var uiElementsText = GameObject.FindGameObjectsWithTag("TextColor");

            foreach (var uiElement in uiElementsText)
            {
                var imageComponent = uiElement.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.color = Color.black;
                }

                var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                if (textMeshComponent != null)
                {
                    textMeshComponent.color = Color.black;
                }
            }

            //***** Make all "ReverseTextColor" elements white *****
            var uiElementsReverseText = GameObject.FindGameObjectsWithTag("ReverseTextColor");

            foreach (var uiElement in uiElementsReverseText)
            {
                var textMeshComponent = uiElement.GetComponent<TextMeshProUGUI>();
                if (textMeshComponent != null)
                {
                    textMeshComponent.color = Color.white;
                }
            }
        }
    }

    public void CheckDarkModeToggle()
    {
        if(darkModeToggle != null)
        {
            if(PlayerPrefs.GetInt("isDarkModeEnabled", 0) == 1)
                darkModeToggle.isOn = true;
            else
                darkModeToggle.isOn= false;
        }
    }
}
