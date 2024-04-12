using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckOnboarding : MonoBehaviour
{
    void Start()
    {
        LoadInitialScene();
    }

    void LoadInitialScene()
    {
        // Check if the user has completed onboarding
        if (PlayerPrefs.GetInt("HasCompletedOnboarding", 0) == 1)
        {
            SceneManager.LoadScene("HomeScreen");  // Load the homepage if onboarding is complete
        }
        else
        {
            SceneManager.LoadScene("Onboarding");  // Load the onboarding scene if it hasn't been completed
        }
    }

}
