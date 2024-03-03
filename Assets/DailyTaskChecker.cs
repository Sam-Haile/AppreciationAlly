using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyTaskChecker : MonoBehaviour
{

    public Sprite taskIncomplete;
    public Sprite taskCompleted;

    public Image gridGameTask;
    public Image journalTask;
    public Slider taskProgressChecker;
    public TextMeshProUGUI progressStatement;
    private float tasksCompleted = 0;

    private string[] tasks = { "GridGame", "Journal" };


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HomeScreen")
        {
            tasksCompleted = 0;

            foreach(var task in tasks)
            {
                CheckDailyTasksCompletion(task);
            }

            DisplayProgress();
        }
    }

    private void DisplayProgress()
    {
        taskProgressChecker.value = (float)tasksCompleted / tasks.Length;
        float progressAsPercentage = (float)(taskProgressChecker.value * 100);
        progressStatement.text = progressAsPercentage.ToString() + "%" + " of goals complete!";
    }

    private void CheckDailyTasksCompletion(string task)
    {
        bool allTasksCompletedToday = true;

        string today = System.DateTime.UtcNow.ToString("yyyy-MM-dd");

        switch (task)
        {
            case "GridGame":
                if (PlayerPrefs.GetString(task) == today)
                {
                    Debug.Log("Checking the grid game");
                    gridGameTask.sprite = taskCompleted;
                    tasksCompleted++;
                }
                else
                {
                    gridGameTask.sprite = taskIncomplete;
                    allTasksCompletedToday = false;
                }
                break;
            case "Journal":
                if (PlayerPrefs.GetString(task) == today)
                {
                    journalTask.sprite = taskCompleted;
                    tasksCompleted++;
                }
                else
                {
                    journalTask.sprite = taskIncomplete;
                    allTasksCompletedToday = false;
                }
                break;
        default:
            break;


        }

        


        if (allTasksCompletedToday)
        {
            //Debug.Log("All daily tasks completed for today!");
            // Additional actions here
        }
        else
        {
            //Debug.Log("Not all daily tasks have been completed today.");
            // Additional actions here
        }
    }
}
