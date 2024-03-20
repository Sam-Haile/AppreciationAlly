using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public string badgeName;
    public string description;
    public int bronzeThreshold;
    public int silverThreshold;
    public int goldThreshold;
    public int platinumThreshold;
    public int currentUserProgress;

    public Image badgeMaterial;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI currentNum;
    //public TextMeshProUGUI descriptionTxt;

    public void Start()
    {
        LoadIfno();
    }

    public Achievement(string name, string description, int bronze, int silver, int gold, int platinum)
    {
        this.badgeName = name;
        this.description = description;
        bronzeThreshold = bronze;
        silverThreshold = silver;
        goldThreshold = gold;
        platinumThreshold = platinum;
        currentUserProgress = 0;
    }

    public void SaveAchievement()
    {
        SaveAchievements achievement = new SaveAchievements();
        achievement.currentProgress = this.currentUserProgress;
        achievement.achievementName = badgeName;
        string jsonData = JsonUtility.ToJson(achievement);

        string directoryPath = Application.persistentDataPath + "/achievements";
        if (!System.IO.Directory.Exists(directoryPath))
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        System.IO.File.WriteAllText(Application.persistentDataPath + "/achievements/" + badgeName + ".json", jsonData);
    }

    public void UpdateProgress(int progress)
    {
        currentUserProgress += progress;
        LoadIfno();
        SaveAchievement();
    }

    private void LoadIfno()
    {
        // Loads the info and updates the UI
        string path = Application.persistentDataPath + "/achievements/" + badgeName + ".json";

        if(System.IO.File.Exists(path))
        {
            string jsonData = System.IO.File.ReadAllText(path);
            SaveAchievements achievement = JsonUtility.FromJson<SaveAchievements>(jsonData);
            currentUserProgress = achievement.currentProgress;
        }
    }

    public void ResetProgress()
    {
        currentUserProgress = 0;
        SaveAchievement(); // This should save the reset state.
        LoadIfno(); // Make sure this is working correctly.
    }


}
