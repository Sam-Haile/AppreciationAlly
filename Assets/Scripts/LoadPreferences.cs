using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPreferences : MonoBehaviour
{
    public Image selectedPfp;
    public Sprite[] pfps;

    public TextMeshProUGUI greeting;

    private void Start()
    {

        //Apply users pfp

        if (selectedPfp != null && pfps != null)
        {

            int userPfpId = PlayerPrefs.GetInt("PlayerPfp", -1);

            if (userPfpId >= 0 && userPfpId < pfps.Length)
            {
                selectedPfp.sprite = pfps[userPfpId];
            }
            else
            {
                Debug.LogWarning("Pfp Index Out of Range");
                selectedPfp.sprite = pfps[0];
            }
        }

        if (greeting != null)
        {
            //Apply users username
            string userName = PlayerPrefs.GetString("Name", "Friend");
            greeting.text = "Hello " + userName + "!";
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
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
