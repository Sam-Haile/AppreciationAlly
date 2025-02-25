using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    private Color ungratefulColor = new Color(233f / 255f, 49f / 255f, 50f / 255f);
    private Color littleGratefulColor = new Color(236f / 255f, 95f / 255f, 46f / 255f);
    private Color kindOfGratefulColor = new Color(255f / 255f, 214f/ 255f, 120f / 255f); 
    private Color gratefulColor = new Color(187f / 255f, 220f / 255f, 66f / 255f);
    private Color superGratefulColor = new Color(117f / 255f, 239f / 255f, 49f / 255f); 

    public Animator canvasAnim;
    public Animator chromoAnim;
    public Material chromoPrimaryColor;
    public GameObject chromo;
    public TextMeshProUGUI chromosSpeechBubble;
    public TextMeshProUGUI chromos2ndSpeechBubble;
    public TextMeshProUGUI nextButton;
    public GameObject backButton;
    private bool happy;
    private bool sad;

    public TextMeshProUGUI journalHeader;
    public TextMeshProUGUI journalSubheader;
    [HideInInspector]public JournalStep currentStep;
    public GameObject[] stepUIs;

    //Step 1s
    public Slider emotionSlider;
    public TextMeshProUGUI emotionRating;
    JournalStep step1;

    //Step 2 
    JournalStep step2Ungrtfl;
    JournalStep step2Grtfl;

    //Step 3
    public List<GratefulButton> gratefulButtons;

    //Step 4
    public TextMeshProUGUI journalPromptMesh;

    //Step 5
    public List<GratefulButton> selectedButtons;
    public TMP_InputField[] gratefulFor;
    public Slider finalSlider;
    public List<GratefulButton> final_Buttons;
    public TextMeshProUGUI[] final_slots;
    public string[] final_slots_strings;
    private int activeSlots = 0; // Keeps track of how many slots have text and should be visible.

    //Final Display fields

    private string[] ungratefulResponses = { "Feeling that way is okay. Let's find small good things in your day",
                                             "It's okay to have off days. Can you name one small thing you liked recently?",
                                             "Feeling less grateful sometimes is normal. Did you see something pretty or interesting recently?",
                                             "Not every day feels great, and that’s okay. Did something make you feel a little better recently?",
                                             "It's fine to not always feel thankful. Can you think of something that you didn't mind doing recently?",
                                             "You don't have to feel grateful all the time. What's one small thing you enjoyed or appreciated recently?"
    };

    private string[] gratefulResponses = { "That's great to hear! Can you share what's making you feel this way?",
                                            "Wonderful! What's been the best part of your day?",
                                            "That's fantastic! What made you feel so good today?",
                                            "So glad to hear that! Can you describe what's brought you joy?",
                                            "Amazing! What are you most grateful for today?",
                                            "That's really cool! What's something special that happened?",
                                            "Great to hear! What's one thing that made you smile the most?",
                                            "Lovely to hear you're feeling this way. Can you share a highlight?",
                                            "That's awesome! What's something good that stood out to you today?"};

    private string[] journalPrompts = {
        "What are three things you’re grateful for today?",
        "Write about a person who has positively impacted your life and why you’re thankful for them.",
        "What is a recent experience that made you feel truly grateful?",
        "Describe a place that makes you feel peaceful and grateful.",
        "What is a lesson you’ve learned recently that you’re thankful for?",
        "List five small things in your daily life that bring you joy.",
        "Write about a challenge you’ve faced and how it helped you grow.",
        "What’s a childhood memory you’re grateful for?",
        "Describe a simple pleasure that makes you feel thankful.",
        "What is a quality you love about yourself and why are you grateful for it?",
        "Write about an act of kindness you received recently.",
        "What’s a piece of advice you’re thankful someone gave you?",
        "List three things you love about your home.",
        "Write about a skill you’ve learned and how it’s improved your life.",
        "Who made you smile today, and why are you grateful for them?",
        "What’s something in nature that makes you feel awe and gratitude?",
        "Write about a book, movie, or song that changed your perspective and why you’re thankful for it.",
        "What’s a favorite tradition you’re grateful to have?",
        "What are three things your body allows you to do that you’re grateful for?",
        "Describe a time when someone supported you and why you’re thankful for them.",
        "Write about a meal you’ve enjoyed recently and why it brought you gratitude.",
        "What’s a mistake you made that ended up teaching you something valuable?",
        "Write about a moment when you felt completely at peace.",
        "What’s something you used to take for granted but now deeply appreciate?",
        "Write about a friendship that has positively shaped your life.",
        "What is a modern convenience you’re thankful for and why?",
        "Write about a time when someone expressed gratitude to you.",
        "What’s a favorite hobby or activity that brings you joy?",
        "Write about a time you accomplished something difficult and how it made you feel.",
        "What is your favorite time of day, and why are you grateful for it?",
        "Write about a teacher, mentor, or leader who inspired you.",
        "What’s a piece of technology you’re grateful for and how it helps you?",
        "Describe a holiday or celebration you’re grateful to have experienced.",
        "Write about your favorite season and why you’re thankful for it.",
        "What’s a recent compliment you received that you’re grateful for?",
        "Write about a goal you’ve achieved and the journey it took to get there.",
        "List three people you’re grateful to have in your life and why.",
        "Write about a pet or animal that has brought joy into your life.",
        "What is something you’ve done for someone else that made you feel grateful?",
        "Write about a time when you felt truly appreciated.",
        "What’s a unique trait or ability you have that you’re thankful for?",
        "Write about a tradition or custom from your culture that fills you with gratitude.",
        "What’s something about your current job or studies that you’re grateful for?",
        "Write about a smell, sound, or taste that reminds you of something positive.",
        "What’s a recent opportunity or experience that you’re grateful you had?",
        "Write about a dream or goal you’re working toward and why you’re thankful for the journey.",
        "List three things you’re grateful for that money can’t buy.",
        "Write about a time when you felt supported by your community.",
        "What’s something you’ve learned about gratitude itself?",
        "Reflect on how practicing gratitude has impacted your life and mindset."
    };

    private string[] availablePrompts = {

    };

    void Start()
    {
        SetupJournal();
    }

    private void SetupJournal()
    {
        chromoPrimaryColor.color = new Color(242f / 255f, 138f / 255f, 94f / 255f);

        step1 = new JournalStep("How grateful do you feel?", null, stepUIs[0], 0);
        step2Ungrtfl = new JournalStep("", null, stepUIs[1], 1);
        step2Grtfl = new JournalStep("", null, stepUIs[1], 1);
        JournalStep step3 = new JournalStep("What are you Grateful for Today?", "Choose up to 3 ", stepUIs[2], 2);
        //JournalStep step4 = new JournalStep("Let's Reflect on Your Day?", null, stepUIs[3], 3);
        JournalStep step4 = new JournalStep("Let's Reflect on Your Day?", null, stepUIs[3], 3);
        JournalStep step5 = new JournalStep("Your day", null, stepUIs[4], 4);
        JournalStep step6 = new JournalStep("Your day", null, null, 5);
        JournalStep step7 = new JournalStep("", null, null, 6);

        step1.PreviousStep = null;
        step1.NextStep = step2Grtfl;
        step1.gratefulPath = step2Grtfl;
        step1.ungratefulPath = step2Ungrtfl;

        step2Ungrtfl.NextStep = step3;
        step2Grtfl.NextStep = step3;
        step2Ungrtfl.PreviousStep = step1;
        step2Grtfl.PreviousStep = step1;
        step3.NextStep = step4;
        step3.PreviousStep = step1;

        step4.NextStep = step5;
        step4.PreviousStep = step3;

        step5.PreviousStep = step4;
        step5.NextStep = step6;

        step6.PreviousStep = step5;
        step6.NextStep = step7;

        step7.PreviousStep = null;
        step7.NextStep = null;

        currentStep = step1;
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        if (currentStep.headerText != null)
            journalHeader.text = currentStep.headerText;
        else
            journalHeader.text = "";

        if (currentStep.subheaderText != null)
            journalSubheader.text = currentStep.subheaderText;
        else
            journalSubheader.text = "";

        foreach (var step in stepUIs)
        {
            if (step != currentStep.stepUI)
                step.SetActive(false);
            else
                step.SetActive(true);
        }

        if(currentStep.currentStepIndex == 0)
        {
            chromoAnim.SetBool("happy", happy);
            chromoAnim.SetBool("sad", sad);
        }
        else if(currentStep.currentStepIndex == 1)
        {
            chromoAnim.SetBool("speaking", true);
        }
        else if(currentStep.currentStepIndex == 3)
        {
            SetJournalPrompt();
        }
        else if(currentStep.currentStepIndex == 5)
        {
            chromoAnim.SetTrigger("end");
        }
    }

    public void NextStep()
    {
        if (currentStep.currentStepIndex == 0)
        {
            sad = chromoAnim.GetBool("sad");
            happy = chromoAnim.GetBool("happy");

            if (emotionSlider.value < .56f)
            {
                currentStep = step1.ungratefulPath;
                CreateChromoResponse(false);
                UpdateUI();
            }
            else if (emotionSlider.value >= .56f)
            {
                currentStep = step1.gratefulPath;
                CreateChromoResponse(true);
                UpdateUI();
            }
        }
        else if(currentStep.currentStepIndex < 7)
        {
            // Add a condition to check if we're moving from step index 1 to step index 2
            if (currentStep.currentStepIndex == 1 && currentStep.NextStep.currentStepIndex == 2)
            {
                chromoAnim.SetTrigger("done");
                canvasAnim.SetTrigger("fadeIn");
            }

            currentStep = currentStep.NextStep;
            UpdateUI();
        }
    }

    public void PreviousStep()
    {
        if (currentStep.currentStepIndex >= 1)
        {
            if (currentStep.PreviousStep.currentStepIndex == 0)
            {
                chromoAnim.SetBool("speaking", false);
            }
            if (currentStep.currentStepIndex == 2)
            {
                chromoAnim.SetTrigger("back");
                canvasAnim.SetTrigger("fadeOut");
            }

            nextButton.text = "NEXT";
            currentStep = currentStep.PreviousStep;
        }
        UpdateUI();
    }

    /// <summary>
    /// This method returns a reponse according to the users reponse
    /// tailored to be empathetic to their current emotions
    /// </summary>
    private void CreateChromoResponse(bool isGrateful)
    {
        if (isGrateful)
            chromosSpeechBubble.text = gratefulResponses[UnityEngine.Random.Range(0, gratefulResponses.Length)];
        else
            chromosSpeechBubble.text = ungratefulResponses[UnityEngine.Random.Range(0, ungratefulResponses.Length)];
    }

    public void UpdateEmotion()
    {


        if (emotionSlider.value <= .2f)
        {
            chromoAnim.SetBool("sad", true);
            chromoAnim.SetBool("happy", false);
            chromoAnim.SetBool("meh", false);
            chromoAnim.SetBool("superHappy", false);
            chromoAnim.SetBool("superSad", true);
            emotionRating.text = "Ungrateful";
            chromoPrimaryColor.color = ungratefulColor;
        }
        else if (emotionSlider.value > .2f && emotionSlider.value < .4f)
        {
            chromoAnim.SetBool("sad", true);
            chromoAnim.SetBool("happy", false);
            chromoAnim.SetBool("meh", false);
            chromoAnim.SetBool("superHappy", false);
            chromoAnim.SetBool("superSad", false);
            emotionRating.text = "A Little Grateful";
            chromoPrimaryColor.color = Color.Lerp(ungratefulColor, littleGratefulColor, (emotionSlider.value - 0.2f) / 0.2f);
        }
        else if (emotionSlider.value > .4f && emotionSlider.value < .6f)
        {
            chromoAnim.SetBool("sad", false);
            chromoAnim.SetBool("happy", false);
            chromoAnim.SetBool("meh", true);
            chromoAnim.SetBool("superHappy", false);
            chromoAnim.SetBool("superSad", false);
            emotionRating.text = "Kind of Grateful";
            chromoPrimaryColor.color = Color.Lerp(littleGratefulColor, kindOfGratefulColor, (emotionSlider.value - 0.4f) / 0.2f);
        }
        else if (emotionSlider.value > .6f && emotionSlider.value < .8f)
        {
            chromoAnim.SetBool("sad", false);
            chromoAnim.SetBool("happy", true);
            chromoAnim.SetBool("meh", false);
            chromoAnim.SetBool("superHappy", false);
            chromoAnim.SetBool("superSad", false);
            emotionRating.text = "Grateful";
            chromoPrimaryColor.color = Color.Lerp(kindOfGratefulColor, gratefulColor, (emotionSlider.value - 0.6f) / 0.2f);
        }
        else if (emotionSlider.value > .8f)
        {
            chromoAnim.SetBool("sad", false);
            chromoAnim.SetBool("happy", true);
            chromoAnim.SetBool("meh", false);
            chromoAnim.SetBool("superHappy", true);
            chromoAnim.SetBool("superSad", false);
            emotionRating.text = "Super Grateful";
            chromoPrimaryColor.color = Color.Lerp(gratefulColor, superGratefulColor, (emotionSlider.value - 0.8f) / 0.2f);
        }
    }


    public void SaveInformation()
    {
        switch (currentStep.currentStepIndex)
        {
            case 0:
                backButton.SetActive(false);
                break;
            case 1:
                backButton.SetActive(true);
                break;
            case 3:
                selectedButtons.Clear();
                foreach (GratefulButton selectedButton in gratefulButtons)
                {
                    if (selectedButton.selected)
                        selectedButtons.Add(selectedButton);
                }
                break;
            case 4:
                DisplayFinalEntry();
                break;
            case 5:
                DeletePreviousEntriesForToday();
                Save();
                PostJournalSteps();
                backButton.SetActive(false);

                break;
            case 6:
                GratefulButton.selectedButtons.Clear();
                if (!DailyTasks.Instance.journal_Completed)
                {
                    AchievementManager.IncrementTracker("GratefulEntries", activeSlots);
                    AchievementManager.Instance.UpdateAchievement("Gratitude Gatherer");
                }
                break;
            default:
                break;
        }
    }


    public void Save()
    {
        List<GratefulButtonData> buttonDataList = new List<GratefulButtonData>();
        foreach (GratefulButton button in selectedButtons)
        {
            GratefulButtonData data = new GratefulButtonData
            {
                iconSpriteName = button.iconSpriteName,
                gratefulText = button.grtfl_text.text
            };
            buttonDataList.Add(data);
        }


        for (int i = 0; i < final_slots.Length; i++)
        {
            if (final_slots[i].text != "")
            {
                final_slots_strings[i] = final_slots[i].text;
            }
            else
                final_slots_strings[i] = "";
        }

        // Create a new JournalEntry with the collected data
        string currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
        JournalEntry entry = new JournalEntry(currentDate, finalSlider.value, buttonDataList, final_slots_strings);

        // Serialize the JournalEntry to JSON
        string json = JsonUtility.ToJson(entry, true); // Added 'true' for pretty print, optional
        string path = UnityEngine.Application.persistentDataPath + "/journal_" + currentDate.Replace(" ", "_").Replace(",", "") + ".json"; // Replace spaces with underscores to avoid potential issues in file names
        File.WriteAllText(path, json);
    }

    public static int CountUniqueJournalEntries()
    {
        string path = UnityEngine.Application.persistentDataPath;
        // Get all .json files starting with "journal_" prefix
        string[] journalFiles = Directory.GetFiles(path, "journal_*.json");

        // Each file is a unique entry by date, return the count
        return journalFiles.Length;
    }

    public void DeletePreviousEntriesForToday()
    {
        string todayFilename = "journal_" + DateTime.Now.ToString("MMMM dd, yyyy");
        var directory = new DirectoryInfo(UnityEngine.Application.persistentDataPath);
        var files = directory.GetFiles("journal_*.json");

        foreach (var file in files)
        {
            // If the file name contains today's date but is not the exact file name for today
            // (in case there are time stamps or other differentiators),
            // you can delete the file.
            if (file.Name.Contains(todayFilename) && !file.Name.Equals(todayFilename + ".json"))
            {
                file.Delete();
            }
        }
    }

    /// <summary>
    /// This field displays the previous entries on one page for the user to review
    /// </summary>
    private void DisplayFinalEntry()
    {
        // Saves the slider
        finalSlider.value = emotionSlider.value;

        // First clear existing buttons
        // Used if user goes back to modify selections
        foreach (var finalButton in final_Buttons)
        {
            finalButton.icon.sprite = null; 
            finalButton.grtfl_text.text = "";
            finalButton.gameObject.SetActive(false); // Hide the button as the default state.
        }

        // Now, update based on current selections.
        for (int i = 0; i < final_Buttons.Count; i++)
        {
            if (i < selectedButtons.Count && selectedButtons[i] != null)
            {
                final_Buttons[i].icon.sprite = selectedButtons[i].icon.sprite;
                final_Buttons[i].grtfl_text.text = selectedButtons[i].grtfl_text.text;
                final_Buttons[i].gameObject.SetActive(true); // Only make the button visible if it's being used.
            }
        }

        UpdateFinalSlotsVisibility();


        nextButton.text = "SUBMIT";
    }

    private void UpdateFinalSlotsVisibility()
    {
        activeSlots = 0;

        // Ensure this array is cleared or correctly initialized before updating
        final_slots_strings = new string[gratefulFor.Length];

        // Reset visibility and texts of all final slots before updating
        foreach (var slot in final_slots)
        {
            slot.transform.parent.gameObject.SetActive(false);
            slot.text = "";
        }

        // Update visibility and content based on current input fields
        for (int i = 0; i < gratefulFor.Length; i++)
        {
            final_slots_strings[i] = gratefulFor[i].text; // Always update the strings array

            if (!string.IsNullOrEmpty(gratefulFor[i].text))
            {
                final_slots[i].text = gratefulFor[i].text;
                final_slots[i].transform.parent.gameObject.SetActive(true);
                activeSlots++;
            }
        }
    }


    private void PostJournalSteps()
    {
        // If the grid game was NOT played first
        if (!DailyTasks.Instance.gridGame_Completed)
        {
            chromos2ndSpeechBubble.text = "Amazing reflection today!" +
                " If you're looking for a mood boost, why not try the grid game next?";
        }
        // If the grid game WAS played
        else if(DailyTasks.Instance.gridGame_Completed)
        {
            chromos2ndSpeechBubble.text = "Great job reflecting today!" +
                " Remember that choosing to focus on things you are grateful for can help you feel your best and combat stress.";
        }
    }

    public void JournalClose()
    {
        if(currentStep.currentStepIndex == 6)
        {
            DailyTasks.Instance.MarkTaskAsCompleted("Journal");
            this.gameObject.GetComponent<Animator>().SetTrigger("quit");
        }
    }

    /// <summary>
    /// Set the journal prompt to be displayed in the journal prompt step
    /// </summary>
    private void SetJournalPrompt()
    {
        //Debug.Log(DateTime.Now.Date);
        //Debug.Log(DailyTasks.Instance.journal_Completed);

        //if journalPromptMesh is real,...
        if (journalPromptMesh != null)
        {
            //update journalPromptMesh's text with a random jounral prompt
            journalPromptMesh.text = journalPrompts[UnityEngine.Random.Range(0, journalPrompts.Length - 1)];
        }
    }
}
