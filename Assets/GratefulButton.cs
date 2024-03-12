using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GratefulButton : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI grtfl_text;
    private Color parsedSecondaryColor;
    public bool selected;

    // Assuming this static list is managed somewhere accessible
    public static List<GratefulButton> selectedButtons = new List<GratefulButton>();


    private void Start()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("SecondaryColor"), out parsedSecondaryColor);
    }

    public void OnClick()
    {
        if (!selected && selectedButtons.Count < 3)
        {
            selected = true;
            this.GetComponent<Image>().color = parsedSecondaryColor;
            icon.color = Color.white;
            grtfl_text.color = Color.white;
            selectedButtons.Add(this);
        }
        else
        {
            selected = false;
            this.GetComponent<Image>().color = Color.white;
            icon.color = Color.black;
            grtfl_text.color = Color.black;
            selectedButtons.Remove(this);
        }
    }
}
