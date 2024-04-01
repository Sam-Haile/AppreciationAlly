using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<AchievementData> achievements = new List<AchievementData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAchievement(string badgeName, int progressToAdd)
    {
        var achievement = achievements.Find(a => a.badgeName == badgeName);
        if (achievement != null)
        {
            achievement.currentUserProgress = progressToAdd;
            // Add logic to save achievement progress here
            // You might want to save to PlayerPrefs, a file, or a database
        }
    }
}
