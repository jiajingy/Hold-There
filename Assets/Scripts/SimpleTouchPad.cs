using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 origin;
    private Vector2 direction;

    public float smoothing;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    private void Awake()
    {
        Debug.Log("awake");
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        // Set start point
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            origin = eventData.position;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        
        // Compare difference between our start point and current pointer position
        if (pointerID == eventData.pointerId)
        {
            Vector2 curPosition = eventData.position;
            Vector2 directionRaw = curPosition - origin;
            direction = directionRaw.normalized;
        }

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        
        // Reset everything
        if (pointerID == eventData.pointerId)
        {
            direction = Vector2.zero;
            touched = false;
        }

    }

    public Vector2 GetDirection()
    {
        

        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);

        

        return smoothDirection;
    }

    void OnDisable()
    {
        origin = Vector2.zero;
        direction = Vector2.zero;
        smoothDirection = Vector2.zero;
        touched = false;
    }

    
}
