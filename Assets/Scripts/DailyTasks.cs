using System;
using UnityEngine;

public class DailyTasks : MonoBehaviour
{
    public static DailyTasks Instance { get; private set; }
    public bool gridGame_Completed;
    public bool journal_Completed;

    // var to track consecutive usage
    private const string LastUsageDateKey = "LastUsageDate";
    private const string ConsecutiveDaysKey = "ConsecutiveDays";
    private const string DailyDynamoAchievementKey = "DailyDynamoAchievementDate"; // New key for tracking achievement increment


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Reset the daily task flags
            CheckAndResetTaskCompletion();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TrackDailyUsage();
        CheckAndIncrementDailyDynamoAchievement();
    }

    public void CheckAndIncrementDailyDynamoAchievement()
    {
        // First, check if both tasks are completed
        if (gridGame_Completed && journal_Completed)
        {
            // Then, verify if the achievement has already been incremented today
            string lastIncrementDateStr = PlayerPrefs.GetString(DailyDynamoAchievementKey, "");
            DateTime lastIncrementDate;

            if (DateTime.TryParse(lastIncrementDateStr, out lastIncrementDate) && lastIncrementDate.Date == DateTime.UtcNow.Date)
            {
                // Achievement has already been incremented today, do nothing
                return;
            }

            // If not incremented today, increment the achievement
            AchievementManager.IncrementTracker("DailyDynamo", 1);
            AchievementManager.Instance.UpdateAchievement("Daily Dynamo");

            // Update the last increment date to today
            PlayerPrefs.SetString(DailyDynamoAchievementKey, DateTime.UtcNow.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
    }

    public void SaveTask(string taskName)
    {
        PlayerPrefs.SetString(taskName, System.DateTime.UtcNow.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

    private void CheckAndResetTaskCompletion()
    {
        CheckAndResetIndividualTaskCompletion("GridGame", ref gridGame_Completed);
        CheckAndResetIndividualTaskCompletion("Journal", ref journal_Completed);
    }

    private void CheckAndResetIndividualTaskCompletion(string taskKey, ref bool taskCompletionFlag)
    {
        // Get the last completion date from PlayerPrefs
        string lastCompletionDateStr = PlayerPrefs.GetString(taskKey, "");

        if (!string.IsNullOrEmpty(lastCompletionDateStr))
        {
            DateTime lastCompletionDate;
            if (DateTime.TryParse(lastCompletionDateStr, out lastCompletionDate))
            {
                // Compare the date part only to ignore time differences
                if (DateTime.UtcNow.Date > lastCompletionDate.Date)
                {
                    // It's a new day, reset the completion flag
                    taskCompletionFlag = false;

                    // Optionally, clear the PlayerPrefs if you don't need to keep the old date
                    PlayerPrefs.DeleteKey(taskKey);
                }
                else
                {
                    // It's the same day, set the completion flag based on saved state
                    taskCompletionFlag = true;
                }
            }
        }
        else
        {
            // If there's no completion date saved, assume the task has not been completed
            taskCompletionFlag = false;
        }
    }

    public void MarkTaskAsCompleted(string taskKey)
    {
        string existingCompletionDate = PlayerPrefs.GetString(taskKey, "");
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        if (existingCompletionDate != today)
        {
            PlayerPrefs.SetString(taskKey, today);
            PlayerPrefs.Save();
        }

        // Update the flag directly without needing to read PlayerPrefs again
        // This assumes you're calling CheckAndResetTaskCompletion at app start or similar
        if (taskKey == "GridGame")
            Instance.gridGame_Completed = true;
        else if (taskKey == "Journal")
            Instance.journal_Completed = true;
    }

    private void TrackDailyUsage()
    {
        DateTime now = DateTime.Now.Date;
        DateTime lastUsageDate = GetLastUsageDate();

        if (now == lastUsageDate)
        {
            // App has already been used today, do nothing
            return;
        }

        if (now == lastUsageDate.AddDays(1))
        {
            // Consecutive day
            IncrementConsecutiveDays();
        }
        else
        {
            // Not consecutive, reset
            ResetConsecutiveDays();
        }

        SetLastUsageDate(now);
    }

    private DateTime GetLastUsageDate()
    {
        string lastUsageDateString = PlayerPrefs.GetString(LastUsageDateKey, DateTime.MinValue.ToString());
        return DateTime.Parse(lastUsageDateString);
    }

    private void SetLastUsageDate(DateTime date)
    {
        PlayerPrefs.SetString(LastUsageDateKey, date.ToString());
        PlayerPrefs.Save();
    }

    private void IncrementConsecutiveDays()
    {
        int consecutiveDays = PlayerPrefs.GetInt(ConsecutiveDaysKey, 0);
        PlayerPrefs.SetInt(ConsecutiveDaysKey, ++consecutiveDays);
        AchievementManager.IncrementTracker("ConsecutiveDays", 1);
        PlayerPrefs.Save();
    }

    private void ResetConsecutiveDays()
    {
        PlayerPrefs.SetInt(ConsecutiveDaysKey, 1); // Reset to 1 because today is being counted as usage
        PlayerPrefs.Save();
    }

    // Call this method to get the current count of consecutive days
    public int GetConsecutiveDays()
    {
        return PlayerPrefs.GetInt(ConsecutiveDaysKey, 0);
    }
}
