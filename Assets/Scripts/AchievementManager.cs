using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<AchievementData> achievements = new List<AchievementData>();

    private void Start()
    {
        foreach (var achievement in achievements)
        {
            UpdateAchievement(achievement.badgeName, 0);
        }
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

    public void UpdateAchievement(string badgeName, int progressToAdd)
    {
        var achievement = achievements.Find(a => a.badgeName == badgeName);
        if (achievement != null)
        {
            if(badgeName == "Journal Explorer")
            {
                achievement.currentUserProgress = Journal.CountUniqueJournalEntries() + progressToAdd;
                Debug.Log(Journal.CountUniqueJournalEntries());
            }
            else if(badgeName == "Positivity Player")
            {
                achievement.currentUserProgress = GridGame.GetMiniGameCompletionCount() + progressToAdd;
            }
            else
                achievement.currentUserProgress += progressToAdd;
            // Add logic to save achievement progress here
            // You might want to save to PlayerPrefs, a file, or a database
        }
    }


}
