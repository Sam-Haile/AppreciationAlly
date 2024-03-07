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
    private string userName;
    private void Start()
    {

        if (greeting != null)
        {
            //Apply users username
            userName = PlayerPrefs.GetString("Name", "Friend");
            greeting.text = "Hello " + userName + "!";
            settingsName.text = userName;
        }


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
                uiElement.GetComponent<Image>().color = primColor;
            }

            foreach (var uiElement in uiElementsSecondary)
            {
                uiElement.GetComponent<Image>().color = secColor;
            }
        }
        else
        {
            Debug.LogError("Invalid color hex code.");
        }

    }


    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}