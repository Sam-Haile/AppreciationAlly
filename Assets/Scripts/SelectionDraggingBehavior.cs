using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionDraggingBehavior : MonoBehaviour
{
    //Dragging variables
    private bool isDragging = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;
    private SelectionDraggingBehavior selection;

    [Header("Color Selection Variables")]
    [SerializeField] private SpriteRenderer selectionSpriteRenderer;
    //[SerializeField] private RectTransform selectionRectTransform;
    [SerializeField] private RectTransform colorWheelRectTransform;
    [SerializeField] private Texture2D colorWheelTexture;

    private void OnEnable()
    {
        selection = GetComponent<SelectionDraggingBehavior>();
    }

    private void Update()
    {
        UpdateSelectionColor();
    }

    void UpdateSelectionColor()
    {
        Vector3 imagePos = colorWheelRectTransform.position;
        float globalPosX = selection.gameObject.transform.position.x - imagePos.x;
        float globalPosY = selection.gameObject.transform.position.y - imagePos.y;
        //selectionRectTransform.transform.position.y - imagePos.y;
        Debug.Log("selection pos = " + selection.gameObject.transform.position.x + ". image pos = " + imagePos.x + ". Difference = " + globalPosX);

        int localPosX = (int) (820f * (globalPosX * (colorWheelTexture.width / colorWheelRectTransform.rect.width)));
        int localPosY = (int) (820f * (globalPosY * (colorWheelTexture.height / colorWheelRectTransform.rect.height)));
        //Debug.Log(colorWheelTexture.width);
        //Debug.Log(colorWheelRectTransform.rect.width);
        //Debug.Log("globalPosX = " + globalPosX + ". colorWheelTexture.width / colorWheelRectTransform.rect.width = " + colorWheelTexture.width / colorWheelRectTransform.rect.width + "product = " + globalPosX * (colorWheelTexture.width / colorWheelRectTransform.rect.width) + ". localPosX = " + localPosX);

        selectionSpriteRenderer.color = colorWheelTexture.GetPixel(localPosX, localPosY);
        //Debug.Log("X is: " + localPosX + ". and Y is: " + localPosY);
        //Debug.Log("pixel color is: " + colorWheelTexture.GetPixel(localPosX, localPosY));
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
