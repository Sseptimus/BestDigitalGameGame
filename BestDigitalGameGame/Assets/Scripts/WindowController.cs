using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WindowController : MonoBehaviour
{
    private bool m_bHeld;
    private Vector3 m_vec3MousePos;
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
    private void OnMouseDown()
    {
        m_bHeld = true;
        m_vec3MousePos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        m_bHeld = false;
    }

    private void Update()
    {
        if (m_bHeld)
        {
            Vector3 moveVec;
            moveVec.x = transform.position.x +
                        (ConvertToWorldUnitsX(Input.mousePosition.x) - ConvertToWorldUnitsX(m_vec3MousePos.x));
            moveVec.y = transform.position.y + (ConvertToWorldUnitsY(Input.mousePosition.y) - ConvertToWorldUnitsY(m_vec3MousePos.y));
            moveVec.z = 0;
            transform.SetPositionAndRotation(moveVec,Quaternion.identity);
            m_vec3MousePos = Input.mousePosition;
        }
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