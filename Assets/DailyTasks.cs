using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTasks : MonoBehaviour
{
    public static DailyTasks Instance { get; private set; }


    private void Awake()
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

    public void SaveTask(string taskName)
    {
        PlayerPrefs.SetString(taskName, System.DateTime.UtcNow.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

}
