using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionDraggingBehavior : MonoBehaviour
{
    [Header("Dragging Variables")]
    [SerializeField] private SelectionDraggingBehavior selection;
    private bool isDragging = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;

    [Header("Color Selection Variables")]
    [SerializeField] private SpriteRenderer selectionSpriteRenderer;
    [SerializeField] private RectTransform selectionRectTransform;
    [SerializeField] private RectTransform colorWheelRectTransform;
    [SerializeField] private Texture2D colorWheelTexture;
    [SerializeField] private BackgroundColor acceptCustomColorBackgroundColor;
    [SerializeField] private GameObject customColorGameObject;
    [SerializeField] private Vector2 initialOffset = Vector2.zero;
    private Color newColor = Color.white;

    void UpdateSelectionColor()
    {
        newColor = colorWheelTexture.GetPixel((int)(((selectionRectTransform.localPosition.x - initialOffset.x) + colorWheelRectTransform.rect.width / 2f) * (colorWheelTexture.width / colorWheelRectTransform.rect.width)), (int)(((selectionRectTransform.localPosition.y - initialOffset.y) + colorWheelRectTransform.rect.height / 2f) * (colorWheelTexture.height / colorWheelRectTransform.rect.height)));
        //Debug.Log("(" + (int)((selectionRectTransform.localPosition.x + colorWheelRectTransform.rect.width / 2f) * (colorWheelTexture.width / colorWheelRectTransform.rect.width)) + ", " + (int)((selectionRectTransform.localPosition.y + colorWheelRectTransform.rect.height / 2f) * (colorWheelTexture.height / colorWheelRectTransform.rect.height)) + ")");

        selectionSpriteRenderer.color = newColor;
    }

    public void ChangeCustomColor()
    {
        //define H, S, and V
        float H, S, V;

        //***** Modify brightness of primary color *****
        //determine H, S, and V from newColor
        Color.RGBToHSV(newColor, out H, out S, out V);

        //modify V to be darker
        //Debug.Log("Before: " + V);
        V = 0.65f;
        //Debug.Log("After: " + V);

        //convert from HSV to RGB
        Color PrimaryColorRBG = Color.HSVToRGB(H, S, V);

        //apply modified color to primaryColor
        acceptCustomColorBackgroundColor.primaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(PrimaryColorRBG);
        customColorGameObject.GetComponent<BackgroundColor>().primaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(PrimaryColorRBG);
        customColorGameObject.GetComponent<Image>().color = PrimaryColorRBG;

        //***** Modify brightness of secondary color *****
        //determine H, S, and V from newColor
        Color.RGBToHSV(newColor, out H, out S, out V);

        //modify V to be a little bit darker
        //Debug.Log("Before: " + V);
        V = 0.80f;
        //Debug.Log("After: " + V);

        //convert from HSV to RGB
        Color SecondaryColorRBG = Color.HSVToRGB(H, S, V);

        //apply modified color to secondaryColor
        acceptCustomColorBackgroundColor.secondaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(SecondaryColorRBG);
        customColorGameObject.GetComponent<BackgroundColor>().secondaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(SecondaryColorRBG);

        //***** Save Custom Color to PlayerPrefs *****
        SaveCustomColor(PrimaryColorRBG, SecondaryColorRBG);
    }

    void Update()
    {
        UpdateSelectionColor();

        //if (Input.GetMouseButtonUp(0))
        //    Debug.Log("Left mouse released!");
        //Debug.Log("isDragging= " + isDragging + ". and Input.GetMouseButtonUp(0)= " + Input.GetMouseButtonUp(0) + ". and Input.GetMouseButton(0)= " + Input.GetMouseButton(0));
        //***** Dropping *****
        //if left mouse released or touch ended while holding the selection,...
        if(isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            //drop the selection
            Drop();

            //return
            return;
        }

        //***** Dragging *****
        //if left mouse held down,...
        if(Input.GetMouseButton(0))
        {
            //Debug.Log("Clicked");
            //get mouse position
            Vector3 mousePos = Input.mousePosition;

            //store mouse position in screenPosition
            screenPosition = new Vector3(mousePos.x, mousePos.y);
        }
        //else if screen was touched,...
        else if(Input.touchCount > 0)
        {
            //get screen touch position and store it in screenPosition
            screenPosition = Input.GetTouch(0).position;
        }
        //else nothing happened,...
        else
        {
            //do nothing and return
            return;
        }

        //convert screenPosition to world coordinates
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        //if selection circle is already being dragged,...
        if(isDragging)
        {
            //drag the selection
            Drag();
        }
        //else selection circle is NOT already being dragged,...
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                SelectionDraggingBehavior draggable = hit.transform.gameObject.GetComponent<SelectionDraggingBehavior>();

                if (draggable != null)
                {
                    //make "selection" the selection to drag 
                    selection = draggable;

                    //initiate drag
                    InitiateDrag();
                }
            }
        }
    }

    void InitiateDrag()
    {
        //Debug.Log("InitiateDrag");
        isDragging = true;
    }

    void Drag()
    {
        //Debug.Log("Drag");
        selection.transform.position = new Vector2(worldPosition.x, worldPosition.y);
        //Debug.Log(selection.transform.position);
    }

    void Drop()
    {
        //Debug.Log("Drop");
        isDragging = false;
    }

    public void ResetSelectionPosition()
    {
        Drop();
        selectionRectTransform.localPosition = new Vector3(0f, 171f, 0f);
    }

    public void LoadCustomColor()
    {
        if (customColorGameObject)
        {
            Color newCol;

            if (UnityEngine.ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("CustomPrimaryColor", "6C6C6C"), out newCol))
            {
                customColorGameObject.GetComponent<Image>().color = newCol;

                customColorGameObject.GetComponent<BackgroundColor>().primaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(newCol);
                //Debug.Log("Load Primary");
            }

            if (UnityEngine.ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("CustomSecondaryColor", "C8C8C8"), out newCol))
            {
                customColorGameObject.GetComponent<Image>().color = newCol;

                customColorGameObject.GetComponent<BackgroundColor>().secondaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(newCol);
                //Debug.Log("Load Secondary");
            }
            //Debug.Log("Load Custom Color");
        }

        if (selectionRectTransform)
        {
            selectionRectTransform.localPosition = new Vector3(PlayerPrefs.GetFloat("CustomColorSelectionPositionX", 0f), PlayerPrefs.GetFloat("CustomColorSelectionPositionY", 171f), PlayerPrefs.GetFloat("CustomColorSelectionPositionZ", 0f));
            //Debug.Log("(" + PlayerPrefs.GetFloat("CustomColorSelectionPositionX", -Mathf.Infinity) + ", " + PlayerPrefs.GetFloat("CustomColorSelectionPositionY", -Mathf.Infinity) + ", " + PlayerPrefs.GetFloat("CustomColorSelectionPositionZ", -Mathf.Infinity) + ")");
        }
    }

    private void SaveCustomColor(Color primaryColorToSave, Color secondaryColorToSave)
    {
        PlayerPrefs.SetString("CustomPrimaryColor", UnityEngine.ColorUtility.ToHtmlStringRGB(primaryColorToSave));
        PlayerPrefs.SetString("CustomSecondaryColor", UnityEngine.ColorUtility.ToHtmlStringRGB(secondaryColorToSave));
        PlayerPrefs.SetFloat("CustomColorSelectionPositionX", selectionRectTransform.localPosition.x);
        PlayerPrefs.SetFloat("CustomColorSelectionPositionY", selectionRectTransform.localPosition.y);
        PlayerPrefs.SetFloat("CustomColorSelectionPositionZ", selectionRectTransform.localPosition.z);
        //Debug.Log("(" + PlayerPrefs.GetFloat("CustomColorSelectionPositionX", -Mathf.Infinity) + ", " + PlayerPrefs.GetFloat("CustomColorSelectionPositionY", -Mathf.Infinity) + ", " + PlayerPrefs.GetFloat("CustomColorSelectionPositionZ", -Mathf.Infinity) + ")");
    }
}
