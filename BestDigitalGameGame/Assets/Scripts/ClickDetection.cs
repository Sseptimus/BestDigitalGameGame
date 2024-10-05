using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] BaseWindowClass
    TriggeredController;

    public BaseWindowClass.ClickType ClickType;
    private void OnMouseDown()
    {
        TriggeredController.Click(ClickType);
    }
}
