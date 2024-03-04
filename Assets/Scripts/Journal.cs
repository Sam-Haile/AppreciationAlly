using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public Animator chromoAnim;
    public GameObject chromo;
    public TextMeshProUGUI journalHeader;
    public TextMeshProUGUI journalSubheader;

    public Slider emotionSlider;
    public TextMeshProUGUI emotionRating;

    private JournalStep currentStep;
    JournalStep step1;
    JournalStep step2Ungrtfl;
    JournalStep step2Grtfl;

    public GameObject[] stepUIs;
    public TextMeshProUGUI chromosSpeechBubble;

    public List<GratefulButton> gratefulButtons;
    public List<GratefulButton> selectedButtons;

    // "Let's reflect on your day fields"

    public TextMeshProUGUI slot1;
    public TextMeshProUGUI slot2;
    public TextMeshProUGUI slot3;
    public string[] gratefulFor;

    public Slider finalEmotionSlider;
    public GratefulButton[] final_grtfl_buttons;
    public TextMeshProUGUI[] final_grtfl_strings;


    //Final Display fields

    private string[] ungratefulResponses = { "It's okay to feel that way sometimes. Lets try finding small things that might be good in your day",
                                             "It's alright to have days when we don't feel grateful. What's one thing you like about your day, even if it's tiny?",
                                             "It’s perfectly okay to feel less grateful some days. Do you remember seeing something today that was pretty or interesting?"
    };

    private string[] gratefulResponses = { "That's great to hear! Can you share what's making you feel this way?" };

    void Start()
    {
        gratefulFor = new string[2];
        SetupJournal();
    }


    private void SetupJournal()
    {
        step1 = new JournalStep("How grateful do you feel?", null, stepUIs[0], 0);
        step2Ungrtfl = new JournalStep("", null, /*add the text box here*/stepUIs[1], 1);
        step2Grtfl = new JournalStep("", null, /*add the text box here*/stepUIs[1], 1);
        JournalStep step3 = new JournalStep("What are you Grateful for Today?", "Choose 3 or More", stepUIs[2], 2);
        JournalStep step4 = new JournalStep("Let's Reflect on Your Day?", null, stepUIs[3], 3);
        JournalStep step5 = new JournalStep("Your day", null, stepUIs[4], 4);

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

        step4.PreviousStep = step3;
        step4.NextStep = step5;

        step5.NextStep = null;
        step5.PreviousStep = step4;

        currentStep = step1;
        UpdateUI();
    }

    public void UpdateUI()
    {
        journalHeader.text = currentStep.headerText;
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
        if (currentStep.PreviousStep != null)
            currentStep = currentStep.PreviousStep;
    }

    /// <summary>
    /// This method returns a reponse according to the users reponse
    /// tailored to be empathetic to their current emotions
    /// </summary>
    private void CreateChromoResponse(bool isGrateful)
    {
        if (isGrateful)
            chromosSpeechBubble.text = gratefulResponses[Random.Range(0, gratefulResponses.Length)];
        else
            chromosSpeechBubble.text = ungratefulResponses[Random.Range(0, ungratefulResponses.Length)];
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
                Debug.Log(emotionRating.text);
                break;
            case 2:
                break;
            case 3:
                foreach (GratefulButton selectedButton in gratefulButtons)
                {
                    if (selectedButton.selected)
                    {
                        Debug.Log(selectedButton.text);
                        selectedButtons.Add(selectedButton);
                    }
                }

                DisplayFinalEntry();

                break;
            case 4:
                //gratefulFor[0] = slot1.text;
                //gratefulFor[1] = slot2.text;
                //gratefulFor[2] = slot3.text;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// This field displays the previous entries on one page for the user to review
    /// </summary>
    public void DisplayFinalEntry()
    {
        // Show the level of gratefulness felt by the user
        finalEmotionSlider.value = emotionSlider.value;

        for (int i = 0; i < selectedButtons.Count; i++)
        {
            if (selectedButtons[i] != null)
            {
                final_grtfl_buttons[i].icon = selectedButtons[i].icon;
                final_grtfl_buttons[i].text = selectedButtons[i].text;
            }
            else
                final_grtfl_buttons[i].gameObject.SetActive(false);
        }

        // Display the 3 strings user inputs
        for (int i = 0; i < gratefulFor.Length; i++)
            final_grtfl_strings[i].text = gratefulFor[i];



    }
}
