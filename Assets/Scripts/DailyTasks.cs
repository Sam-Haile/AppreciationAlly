using System;
using UnityEngine;

public class DailyTasks : MonoBehaviour
{
    public static DailyTasks Instance { get; private set; }
    public bool gridGame_Completed;
    public bool journal_Completed;

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
}
