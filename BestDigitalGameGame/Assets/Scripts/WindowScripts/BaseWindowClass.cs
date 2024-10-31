using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for root window functionality
// Author: Nick Lees
public class BaseWindowClass : MonoBehaviour
{
    
    private Vector2 WorldUnitsInCamera;
    private Vector2 WorldToPixelAmount;
    public Camera Camera;
    
    void Awake ()
    {
        if (!Camera)
        {
            Camera = Camera.main;
        }
        
        //Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
        WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
    }

    
    //Functions to change MousePos to World Units
    public float ConvertToWorldUnitsX(float _InputX)
    {
        return ((_InputX / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
               Camera.transform.position.x;
    }

    public float ConvertToWorldUnitsY(float _InputY)
    {
        return ((_InputY / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
               Camera.transform.position.y;
    }
}
