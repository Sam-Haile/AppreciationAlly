using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<AchievementData> achievements = new List<AchievementData>();

    public List<AchievementUI> achievementUIs = new List<AchievementUI>();

    private void Start()
    {
        InitializeAchievementUpdateStrategies();
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
    // Define a dictionary that maps badge names to functions that update achievements.
    // Assuming the existence of a context or class where these methods (like Journal.CountUniqueJournalEntries()) are accessible.
    private Dictionary<string, Func<int>> achievementUpdateStrategies;

    // Initialization of the dictionary, ideally done in the constructor or a setup method
    private void InitializeAchievementUpdateStrategies()
    {
        achievementUpdateStrategies = new Dictionary<string, Func<int>>
    {
        {"Journal Explorer", Journal.CountUniqueJournalEntries},
        {"Positivity Player", () => GetTrackerCount("MiniGameCompletionCount")},
        {"Gratitude Gatherer", () => GetTrackerCount("GratefulEntries")},
        {"Consistent Companion", () => GetTrackerCount("ConsecutiveDays")},
        {"Daily Dynamo", () => GetTrackerCount("DailyDynamo")},
        {"Image Innovator", ImageManager.CountUniqueImageEntries}
    };
    }

    public void UpdateAchievement(string badgeName)
    {
        var achievement = achievements.Find(a => a.badgeName == badgeName);
        if (achievement != null && achievementUpdateStrategies.TryGetValue(badgeName, out Func<int> updateStrategy))
        {
            // Update the current user progress by executing the corresponding strategy.
            achievement.currentUserProgress = updateStrategy.Invoke();
        }
    }

    public void DeleteKeyData(string id)
    {
        Debug.Log(PlayerPrefs.GetString("DailyDynamoAchievementDate", "Bruh"));

        //PlayerPrefs.SetInt(id, 0);
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
        for (int i = 0; i < achievements.Count; i++)
        {
            UpdateAchievement(achievements[i].badgeName);
            achievementUIs[i].UpdateAchievementUI();
        }
    }
}
