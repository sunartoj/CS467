using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("STarted Dragging");

        itemBeingDragged = gameObject;
        startPosition = transform.position;     //save the start position
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;     //ummmmm
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ended drag");
        itemBeingDragged = null;
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }
}
