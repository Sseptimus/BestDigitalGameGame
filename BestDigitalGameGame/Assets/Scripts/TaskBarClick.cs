using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBarClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<TaskBarController>().ReopenWindow(gameObject);
    }
}
