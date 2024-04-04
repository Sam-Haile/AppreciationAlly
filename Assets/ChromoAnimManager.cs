using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromoAnimManager : MonoBehaviour
{
    public Animator canvasAnim;

    public Journal journal;

    public void TriggerFadeIn()
    {
        canvasAnim.SetTrigger("fadeIn");
    }

    public void TextBubbleFadeIn()
    {
        if(journal.currentStep.currentStepIndex == 5)
        {
            canvasAnim.SetTrigger("speechBubble");
        }
    }
}
