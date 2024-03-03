using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GratefulButton : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    private Color parsedSecondaryColor;
    public bool selected;

    private void Start()
    {
        if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("SecondaryColor"), out parsedSecondaryColor))
        {
            Debug.Log("Color taken succsesfully");
            return;
        }
    }

    public void OnClick()
    {
        if (!selected)
        {
            selected = true;
            this.GetComponent<Image>().color = parsedSecondaryColor;
            icon.color = Color.white;
            text.color = Color.white;
        }
        else
        {
            selected = false;
            this.GetComponent<Image>().color = Color.white;
            icon.color = Color.black;
            text.color = Color.black;
        }
    }
}
