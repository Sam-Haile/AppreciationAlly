using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static JournalEntry;
using System;

public class DataManager : MonoBehaviour
{
    private string dataPath;

    private void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "journalData.json");
    }
    
    public void SaveData(JournalEntry.JournalData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath, jsonData);
    }

    public JournalData LoadData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<JournalData>(jsonData);
        }

        return new JournalData(); // Return an empty data structure if the file doesn't exist
    }


    public void SubmitEntry(List<string> gratefulFor, List<string> threeEntries)
    {
        JournalData data = LoadData();

        JournalEntry newEntry = new JournalEntry
        {
            entryDate = DateTime.Now,
            gratefulFor = gratefulFor,
            threeEntries = threeEntries
        };

        data.entries.Add(newEntry);
        SaveData(data);
    }

}


