using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindowClass : MonoBehaviour
{
    public enum ClickType
    {
        Minimise
    }
    private Vector2 WorldUnitsInCamera;
    private Vector2 WorldToPixelAmount;
    public Camera Camera;
    
    void Awake ()
    {
        //Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
        WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
    }

    public virtual void Click(ClickType _clickType)
    {
        
    }

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
