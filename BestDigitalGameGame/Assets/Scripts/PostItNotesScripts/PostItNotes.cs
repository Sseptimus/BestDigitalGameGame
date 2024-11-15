using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class PostItNotes :  BaseWindowClass
{
    private bool m_bHeld;
    private Vector3 m_vec3MousePos;
    private SpriteRenderer m_sprContent;
    private SpriteRenderer m_sprTitleBar;
    private GameManager m_GameManager;
    private Canvas m_WindowCanvas;
    private BoxCollider2D m_ColTitleBar;

    private void Start()
    {
        //Initialising variables
        m_sprContent = gameObject.transform.Find("Content").GetComponent<SpriteRenderer>();
        m_GameManager = FindObjectOfType<GameManager>();
        m_ColTitleBar = transform.GetComponent<BoxCollider2D>();
        
        if (!Camera)
        {
            Camera = Camera.main;
        }
    }

    private void OnMouseDown()
    {
        //Mouse clicked on window
        m_bHeld = true;
        m_vec3MousePos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        //Mouse Drag Released
        m_bHeld = false;
    }

    public void Minimise()
    {
        //Minimise Button
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (m_bHeld)
        {
            //Move window if the window is being dragged
           // Vector3 moveVec;
            //moveVec.x = Mathf.Clamp(transform.position.x + (ConvertToWorldUnitsX(Input.mousePosition.x) - ConvertToWorldUnitsX(m_vec3MousePos.x)),m_GameManager.ScreenBounds.transform.position.x,m_GameManager.ScreenBounds.transform.position.x + m_GameManager.ScreenBoundsSize.sizeDelta.x-m_sprTitleBar.bounds.size.x);
           // moveVec.y = Mathf.Clamp(transform.position.y + (ConvertToWorldUnitsY(Input.mousePosition.y) - ConvertToWorldUnitsY(m_vec3MousePos.y)),m_GameManager.ScreenBounds.transform.position.y - m_GameManager.ScreenBoundsSize.sizeDelta.y+m_sprContent.bounds.size.y+m_sprTitleBar.bounds.size.y,m_GameManager.ScreenBounds.transform.position.y);
          //  moveVec.z = 0;
           // transform.SetPositionAndRotation(moveVec,Quaternion.identity);
            m_vec3MousePos = Input.mousePosition;
        }
    }
    
}
