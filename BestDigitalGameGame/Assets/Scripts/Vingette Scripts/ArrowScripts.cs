using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowScripts : MonoBehaviour
{
    [Header ("Camera and Controller")]//set camera and controller to referance
    public Camera MainCamera;
    public VingetteController.TargetScene TargetScene;


    [Header ("Speed")]
    public float HoverTimeRequired;

    float HoverTimeCurrent = 0.0f;
    float HoverTimeScalar = 1.0f;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HoverTimeCurrent += HoverTimeScalar * Time.deltaTime;
            if(HoverTimeCurrent >= HoverTimeRequired)
            {
                HoverTimeCurrent = 0.0f;
                MainCamera.GetComponentInChildren<VingetteController>().m_TargetScene = TargetScene;
                MainCamera.GetComponentInChildren<VingetteController>().m_Fading = true;
            }
        }
    }
}
