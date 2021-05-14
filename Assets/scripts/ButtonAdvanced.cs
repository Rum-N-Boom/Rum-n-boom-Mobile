using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAdvanced : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool hold;

    public void OnPointerDown(PointerEventData eventData)
    {
        hold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hold = false;
    }
}