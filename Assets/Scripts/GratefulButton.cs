using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GratefulButton : MonoBehaviour
{
    public Image icon;
    public string iconSpriteName;
    public TextMeshProUGUI grtfl_text;
    private Color parsedSecondaryColor;
    public bool selected;
    // Assuming this static list is managed somewhere accessible
    public static List<GratefulButton> selectedButtons = new List<GratefulButton>();


    private void Start()
    {
        //if jounral component exisst on the Canvas,...
        if (GameObject.Find("Canvas").GetComponent<Journal>() != null)
        {
            //clear the selected buttons list
            GameObject.Find("Canvas").GetComponent<Journal>().ClearSelectedButtons();
        }

        //selectedButtons.Clear();
        //Debug.Log("Selected Buttons: ");
        //for (int i = 0; i < selectedButtons.Count; i++)
        //{
        //    Debug.Log(selectedButtons[i].grtfl_text.text);
        //}
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("SecondaryColor"), out parsedSecondaryColor);
    }

    public void OnClick()
    {
        if (!selected && selectedButtons.Count < 3)
        {
            selected = true;
            //this.GetComponent<Image>().color = parsedSecondaryColor;
            //icon.color = Color.white;
            //grtfl_text.color = Color.white;
            selectedButtons.Add(this);
        }
        else
        {
            selected = false;

            ////if loaded isDarkMode value is set to true,...
            //if (PlayerPrefs.GetInt("isDarkModeEnabled", 0) == 1)
            //{
            //    Color newCol;
            //    if (ColorUtility.TryParseHtmlString("#333333", out newCol))
            //        this.GetComponent<Image>().color = newCol;
            //    icon.color = Color.white;
            //    grtfl_text.color = Color.white;
            //}
            ////else loaded isDarkMode value is set to false,...
            //else
            //{
            //    //set color to white
            //    this.GetComponent<Image>().color = Color.white;
            //    icon.color = Color.black;
            //    grtfl_text.color = Color.black;
            //}

            selectedButtons.Remove(this);
        }

        ApplySelectionColor();

        Debug.Log(gameObject.name + " " + selected);
        string debugMessage = "";
        for (int i = 0; i < selectedButtons.Count; i++)
        {
            debugMessage = debugMessage + " " + selectedButtons[i].grtfl_text.text;
        }
        Debug.Log("Grateful Button Clicked | Selected Buttons: " + debugMessage);
    }

    public void ApplySelectionColor()
    {
        if (selected)
        {
            this.GetComponent<Image>().color = parsedSecondaryColor;
            icon.color = Color.white;
            grtfl_text.color = Color.white;
        }
        else
        {
            //if loaded isDarkMode value is set to true,...
            if (PlayerPrefs.GetInt("isDarkModeEnabled", 0) == 1)
            {
                Color newCol;
                if (ColorUtility.TryParseHtmlString("#333333", out newCol))
                    this.GetComponent<Image>().color = newCol;
                icon.color = Color.white;
                grtfl_text.color = Color.white;
            }
            //else loaded isDarkMode value is set to false,...
            else
            {
                //set color to white
                this.GetComponent<Image>().color = Color.white;
                icon.color = Color.black;
                grtfl_text.color = Color.black;
            }
        }
    }
}
