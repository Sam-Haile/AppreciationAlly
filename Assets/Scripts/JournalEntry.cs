using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalEntry : MonoBehaviour
{

    public DateTime entryDate;
    public string gratefulness;
    public List<string> gratefulFor;
    public List<string> threeEntries;

    [Serializable]
    public class JournalData
    {
        public List<JournalEntry> entries = new List<JournalEntry>();
    }




}
