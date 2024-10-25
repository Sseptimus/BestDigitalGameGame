using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusGrab : MonoBehaviour, IPointerDownHandler
{
    public WindowController ownedWindow;

    public void OnPointerDown(PointerEventData eventData)
    {
        ownedWindow.OnGrabFocus();
    }
}
