using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<Achievement> achievements = new List<Achievement>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object alive when loading new scenes.
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance of this object.
        }
    }


    public void UpdateAchievement(string achievementName, int progressToAdd)
    {
        Achievement achievementToUpdate = achievements.Find(a => a.name == achievementName);
        if (achievementToUpdate != null)
        {
            achievementToUpdate.UpdateProgress(progressToAdd);
        }
    }

    public void ResetAllAchievements()
    {
        foreach (var achievement in achievements)
        {
            achievement.ResetProgress();
        }
    }

}
