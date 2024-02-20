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

    public Slider emotionSlider;
    public TextMeshProUGUI emotionRating;



    void Start()
    {
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
}
