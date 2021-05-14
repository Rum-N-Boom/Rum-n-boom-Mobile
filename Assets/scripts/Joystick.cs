using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler 
{
    public int MovimentRange = 100;
    Vector3 StartPos;
    public Vector3 InputDirection;
    public Image BgImage;
    Image thisImage;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Vector3.zero;
        int delta = (int)(eventData.position.x - StartPos.x);
        delta = Mathf.Clamp(delta, -MovimentRange, MovimentRange);
        newPos.x = delta;

        int delta2 = (int)(eventData.position.y - StartPos.y);
        delta2 = Mathf.Clamp(delta2, -MovimentRange, MovimentRange);
        newPos.y = delta2;

        transform.position = new Vector3(StartPos.x + newPos.x, StartPos.y + newPos.y, StartPos.z + newPos.z);
        UpdateVirtualAxes(new Vector3(newPos.x * 1 / MovimentRange, 0, newPos.y * 1 / MovimentRange));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = StartPos;
        UpdateVirtualAxes(new Vector3(0, 0, 0));
    }

    void Start()
    {
        thisImage = GetComponent<Image>();
        StartPos = transform.position;
    }

    void UpdateVirtualAxes(Vector3 value)
    {
        var delta = StartPos - value;
        delta.y = -delta.y;
        delta /= MovimentRange;

        InputDirection = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
