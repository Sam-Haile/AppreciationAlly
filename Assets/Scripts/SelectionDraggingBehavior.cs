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

    [Header("Debug")]
    [SerializeField] private Color newColor = Color.white;

    //private void OnEnable()
    //{
    //    selection = GetComponent<SelectionDraggingBehavior>();
    //}

    private void Update()
    {
        UpdateSelectionColor();
    }

    void UpdateSelectionColor()
    {
        newColor = colorWheelTexture.GetPixel((int)((selectionRectTransform.localPosition.x + colorWheelRectTransform.rect.width / 2f) * (colorWheelTexture.width / colorWheelRectTransform.rect.width)), (int)((selectionRectTransform.localPosition.y + colorWheelRectTransform.rect.height / 2f) * (colorWheelTexture.height / colorWheelRectTransform.rect.height)));
        //Debug.Log((int)((selectionRectTransform.localPosition.x + colorWheelRectTransform.rect.width / 2f) * (colorWheelTexture.width / colorWheelRectTransform.rect.width)));

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

        //Color PrimaryColorHSV = Color.RGBToHSV(newColor, out H, out S, out V);
        //Color newPrimaryColor = Color.HSVToRGB(newColor.r, newColor.g, newColor.b);
        //Color newPrimaryColor = new Color(newColor.r - 0.72f, newColor.g - 0.72f, newColor.b - 0.72f);
        //Color newSecondaryColor = new Color(newColor.r - 0.36f, newColor.g - 0.36f, newColor.b - 0.36f);
        //acceptCustomColorBackgroundColor.primaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(newPrimaryColor);
        //acceptCustomColorBackgroundColor.secondaryColor = UnityEngine.ColorUtility.ToHtmlStringRGB(newSecondaryColor);
        //new Color(newColor.r, newColor.g, newColor.b);
    }

    void FixedUpdate()
    {
        //***** Dropping *****
        //if left mouse or touch ended while holding the selection,...
        if(isDragging && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            //drop the selection
            Drop();

            //return
            return;
        }

        //***** Dragging *****
        //if left mouse clicked,...
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
            //, Mathf.Infinity, LayerMask.GetMask("")
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
    }

    void Drop()
    {
        //Debug.Log("Drop");
        isDragging = false;
    }
}
