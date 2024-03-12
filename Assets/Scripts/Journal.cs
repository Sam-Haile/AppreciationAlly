using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Journal : MonoBehaviour
{
    public Animator chromoAnim;
    public GameObject chromo;
    public TextMeshProUGUI chromosSpeechBubble;
    public TextMeshProUGUI nextButton;

    public TextMeshProUGUI journalHeader;
    public TextMeshProUGUI journalSubheader;
    private JournalStep currentStep;
    public GameObject[] stepUIs;

    //Step 1
    public Slider emotionSlider;
    public TextMeshProUGUI emotionRating;
    JournalStep step1;

    //Step 2 
    JournalStep step2Ungrtfl;
    JournalStep step2Grtfl;

    //Step 3
    public List<GratefulButton> gratefulButtons;


    //Step 5
    public List<GratefulButton> selectedButtons;
    public TMP_InputField[] gratefulFor;
    public Slider finalSlider;
    public List<GratefulButton> final_Buttons;
    public TextMeshProUGUI[] final_slots;


    //Final Display fields

    private string[] ungratefulResponses = { "It's okay to feel that way sometimes. Lets try finding small things that might be good in your day",
                                             "It's alright to have days when we don't feel grateful. What's one thing you like about your day, even if it's tiny?",
                                             "It’s perfectly okay to feel less grateful some days. Do you remember seeing something today that was pretty or interesting?"
    };

    private string[] gratefulResponses = { "That's great to hear! Can you share what's making you feel this way?" };

    void Start()
    {
        SetupJournal();
    }


    private void SetupJournal()
    {
        step1 = new JournalStep("How grateful do you feel?", null, stepUIs[0], 0);
        step2Ungrtfl = new JournalStep("", null, /*add the text box here*/stepUIs[1], 1);
        step2Grtfl = new JournalStep("", null, /*add the text box here*/stepUIs[1], 1);
        JournalStep step3 = new JournalStep("What are you Grateful for Today?", "Choose up to 3 ", stepUIs[2], 2);
        JournalStep step4 = new JournalStep("Let's Reflect on Your Day?", null, stepUIs[3], 3);
        JournalStep step5 = new JournalStep("Your day", null, stepUIs[4], 4);
        JournalStep step6 = new JournalStep("Your day", null, stepUIs[1], 5);
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
    }

    public void NextStep()
    {
        if (currentStep.currentStepIndex == 0 && emotionSlider.value < .56f)
        {
            currentStep = step1.ungratefulPath;
            CreateChromoResponse(false);
            UpdateUI();
        }
        else if (currentStep.currentStepIndex == 0 && emotionSlider.value >= .56f)
        {
            currentStep = step1.gratefulPath;
            CreateChromoResponse(true);
            UpdateUI();
        }
        else
        {
            currentStep = currentStep.NextStep;
            UpdateUI();
        }
    }

    public void PreviousStep()
    {
        Debug.Log(currentStep.PreviousStep);
        if (currentStep.currentStepIndex >= 1)
        {
            Debug.Log("Trying previous step");
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

    // Update is called once per frame
    void Update()
    {
        if (chromo.transform.position.y <= -1.75f)
        {
            Rigidbody rb = chromo.GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.mass = 0f;
            chromoAnim.SetTrigger("land");
        }

    }

    public void UpdateEmotion()
    {
        if (emotionSlider.value <= .14f)
            emotionRating.text = "Ungrateful";
        else if (emotionSlider.value > .14f && emotionSlider.value < .28f)
            emotionRating.text = "Slightly Grateful";
        else if (emotionSlider.value > .28f && emotionSlider.value < .42f)
            emotionRating.text = "Somewhat Grateful";
        else if (emotionSlider.value > .42f && emotionSlider.value < .56f)
            emotionRating.text = "Moderately Grateful";
        else if (emotionSlider.value > .56f && emotionSlider.value < .7f)
            emotionRating.text = "Grateful";
        else if (emotionSlider.value > .7f && emotionSlider.value < .84f)
            emotionRating.text = "Highly Grateful";
        else if (emotionSlider.value > .84f && emotionSlider.value < 1f)
            emotionRating.text = "Very Grateful";
    }

    public void SaveInformation()
    {
        switch (currentStep.currentStepIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                selectedButtons.Clear();

                foreach (GratefulButton selectedButton in gratefulButtons)
                {
                    if (selectedButton.selected)
                    {
                        //Debug.Log(selectedButton.text);
                        selectedButtons.Add(selectedButton);
                    }
                }
                break;
            case 4:
                DisplayFinalEntry();
                break;
            case 5:
                // Serialization to JSON
                string currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
                JournalEntry entry = new JournalEntry(currentDate, finalSlider.value, final_Buttons, final_slots);
                string json = JsonUtility.ToJson(entry);
                
                // Saving the Data
                string path = UnityEngine.Application.persistentDataPath + "/journal_" + entry.date + ".json";
                System.IO.File.WriteAllText(path, json);

                Debug.Log("Saving File");

                PostJournalSteps();
                break;
            default:
                break;
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
            finalButton.icon.sprite = null; // Assuming you have a default sprite or null to clear it.
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
            // You don't need an else part here anymore, as you've already handled the reset above.
        }

        UpdateFinalSlotsVisibility();


        nextButton.text = "SUBMIT";
    }

    private void UpdateFinalSlotsVisibility()
    {
        int activeSlots = 0; // Keeps track of how many slots have text and should be visible.

        // Iterate over all possible input fields to check if they have text.
        for (int i = 0; i < gratefulFor.Length; i++)
        {
            if (!string.IsNullOrEmpty(gratefulFor[i].text) && activeSlots < final_slots.Length)
            {
                // If there's text, ensure the slot is visible and set the text.
                final_slots[activeSlots].text = gratefulFor[i].text;
                final_slots[activeSlots].transform.parent.gameObject.SetActive(true);
                activeSlots++;
            }
        }

        // Deactivate any remaining slots that didn't get text assigned.
        for (int k = activeSlots; k < final_slots.Length; k++)
        {
            final_slots[k].transform.parent.gameObject.SetActive(false);
        }
    }


    private void PostJournalSteps()
    {
        Debug.Log(DailyTasks.Instance.gridGame_Completed);
        // If the grid game was NOT played first
        if (!DailyTasks.Instance.gridGame_Completed)
        {
            chromosSpeechBubble.text = "Amazing reflection today!" +
                " If you're looking for a mood boost, why not try the grid game next? It's a fun way to appreciate the little things";
        }
        // If the grid game WAS played
        else if(DailyTasks.Instance.gridGame_Completed)
        {
            chromosSpeechBubble.text = "Great job reflecting today! " +
                "Remember, every word you write is a step toward understanding yourself better.";

        }
    }

    public void JournalClose()
    {
        if(currentStep.currentStepIndex == 6)
        {
            DailyTasks.Instance.JournalComplete();
            this.gameObject.GetComponent<Animator>().SetTrigger("quit");
        }
    }

}
