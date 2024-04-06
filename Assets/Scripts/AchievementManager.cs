using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<AchievementData> achievements = new List<AchievementData>();

    private void Start()
    {
        UpdateAllAchievements();
    }

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

    public void UpdateAchievement(string badgeName)
    {
        var achievement = achievements.Find(a => a.badgeName == badgeName);
        if (achievement != null)
        {
            if(badgeName == "Journal Explorer")
            {
                achievement.currentUserProgress = Journal.CountUniqueJournalEntries();
            }
            else if(badgeName == "Positivity Player")
            {
                achievement.currentUserProgress = GetTrackerCount("MiniGameCompletionCount");
            }
            else if(badgeName == "Gratitude Gatherer")
            {
                achievement.currentUserProgress = GetTrackerCount("GratefulEntries");
            }
            else if(badgeName == "Consistent Companion")
            {
                achievement.currentUserProgress = GetTrackerCount("ConsecutiveDays");
            }
            else if(badgeName == "Daily Dynamo")
            {
                achievement.currentUserProgress = GetTrackerCount("DailyDynamo");
            }
        }
    }

    public static void IncrementTracker(string id, int numToIncreaseBy)
    {
        // Key to store the completion count
        string key = id;

        // Retrieve the current completion count. If it doesn't exist, default to 0.
        int currentCount = PlayerPrefs.GetInt(key, 0);

        // Increment the count
        currentCount += numToIncreaseBy;

        // Save the new count back to PlayerPrefs
        PlayerPrefs.SetInt(key, currentCount);

        // It's important to save changes
        PlayerPrefs.Save();
    }

    public static int GetTrackerCount(string id)
    {
        // Key to retrieve the completion count
        string key = id;

        // Retrieve and return the current completion count. Defaults to 0 if not set.
        return PlayerPrefs.GetInt(key, 0);
    }
    
    public void UpdateAllAchievements()
    {
        foreach (var achievement in achievements)
        {
            UpdateAchievement(achievement.badgeName);
        }
    }
}
