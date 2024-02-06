using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{
    public Image selectedPfp;
    public Sprite[] pfps;

    public TextMeshProUGUI greeting;

    private void Start()
    {
        int userPfpId = PlayerPrefs.GetInt("PlayerPfp", -1);

        if(userPfpId >= 0 && userPfpId < pfps.Length)
        {
            selectedPfp.sprite = pfps[userPfpId];
        }
        else
        {
            Debug.LogWarning("Pfp Index Out of Range");
            selectedPfp.sprite = pfps[0];
        }

        string userName = PlayerPrefs.GetString("Name", "Friend");

        greeting.text = "Hello " + userName + "!";
    }

}
