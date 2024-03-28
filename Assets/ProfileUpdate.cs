using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUpdate : MonoBehaviour
{
    public TextMeshProUGUI nameInput;

    public TextMeshProUGUI updateNamed;

    public LoadPreferences loadPreferences;

    public GameObject[] profilePictures;
    private int userPfpId;

    public Button notificationButton;


    private void Start()
    {
        UpdateNamePfp();
    }

    public void UpdateNamePfp()
    {
        nameInput.text = LoadPreferences.userName;

        int selectedId = PlayerPrefs.GetInt("PlayerPfp", 1);


        foreach (var pfp in profilePictures)
        {
            int id = pfp.GetComponent<ProfilePicture>().id;

            if (id != selectedId)
                pfp.gameObject.transform.localScale = new Vector3(.75f, .75f, 1f);
            else
            {
                pfp.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                userPfpId = id;
            }
        }
    }

    public void OnApplyName()
    {
        string nameNoSpace = updateNamed.text.Replace(" ", "");
        PlayerPrefs.SetString("Name", nameNoSpace);
        loadPreferences.ApplyName();
        PlayerPrefs.Save();
    }

    public void SetPfp(ProfilePicture selectedPfp)
    {
        foreach (var pfp in profilePictures)
        {
            int id = pfp.GetComponent<ProfilePicture>().id;

            if (id != selectedPfp.id)
                StartCoroutine(Onboarding.InterpolateScale(pfp.gameObject, new Vector3(.75f, .75f, 1f), .15f));
            else
            {
                StartCoroutine(Onboarding.InterpolateScale(pfp.gameObject, new Vector3(1f, 1f, 1f), .15f));
                userPfpId = id;
            }
        }
    }

    public void OnApplyPfp()
    {
        PlayerPrefs.SetInt("PlayerPfp", userPfpId);
        loadPreferences.ApplyPfp();
        PlayerPrefs.Save();
    }

    public void NotificationSettings()
    {
        Animator buttonAnimator = notificationButton.GetComponent<Animator>();
        bool currentActiveState = buttonAnimator.GetBool("Active");

        buttonAnimator.SetBool("Active", !currentActiveState);
    }

}
