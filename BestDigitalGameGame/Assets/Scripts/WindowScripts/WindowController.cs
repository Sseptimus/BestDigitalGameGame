using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

// class for basic window controls
// Author: Nick Lees
public class WindowController :  BaseWindowClass
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
        m_sprTitleBar = gameObject.transform.Find("TitleBar").GetComponent<SpriteRenderer>();
        m_GameManager = FindObjectOfType<GameManager>();
        m_ColTitleBar = transform.GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        //Mouse clicked on window
        m_bHeld = true;
        m_vec3MousePos = Input.mousePosition;
        OnGrabFocus();
    }

    public void OnGrabFocus()
    {
        if (m_GameManager.WindowInFocus != null)
        {
            m_GameManager.WindowInFocus.LoseFocus();
        }
        
        //Reset reference to current focused window
        m_GameManager.WindowInFocus = this;
        
        //Changing collision box to just title bar
        m_ColTitleBar.size = new Vector2(5,0.5f);
        m_ColTitleBar.offset = new Vector2(2.5f, -0.25f);
        
        //Moving window elements to FocusedWindow layout
        
        SpriteRenderer[] ChildRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var Renderer in ChildRenderers)
        {
             Renderer.sortingLayerName = "FocusedWindow";
        }
        Canvas[] ChildCanvas = GetComponentsInChildren<Canvas>();
        foreach (var Canvas in ChildCanvas)
        {
           Canvas.sortingLayerName = "FocusedWindow";
        }
    }

    public void LoseFocus()
    {
        //Changing collision box to whole window
        m_ColTitleBar.size = new Vector2(5,4.5f);
        m_ColTitleBar.offset = new Vector2(2.5f, -2.25f);
        
        //Reset window elements to NonFocused Layer
        SpriteRenderer[] ChildRenderers = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(ChildRenderers.Length);
        foreach (var Renderer in ChildRenderers)
        {
            Renderer.sortingLayerName = "NonFocusedWindows";
            Debug.Log(Renderer.name);
        }
        
        Canvas[] ChildCanvas = GetComponentsInChildren<Canvas>();
        foreach (var Canvas in ChildCanvas)
        {
            Canvas.sortingLayerName = "NonFocusedWindows";
        }
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
        if (m_bHeld && (ConvertToWorldUnitsX(Input.mousePosition.x) > m_GameManager.ComputerScreen.transform.position.x + m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.x
            || ConvertToWorldUnitsX(Input.mousePosition.x) < m_GameManager.ComputerScreen.transform.position.x
            || ConvertToWorldUnitsY(Input.mousePosition.y) > m_GameManager.ComputerScreen.transform.position.y
            || ConvertToWorldUnitsY(Input.mousePosition.y) < m_GameManager.ComputerScreen.transform.position.y - m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.y))
        {
            //When mouse leaves screen stop dragging window
            m_bHeld = false;
        }
        if (m_bHeld)
        {
            //Move window if the window is being dragged
            Vector3 moveVec;
            moveVec.x = Mathf.Clamp(transform.position.x + (ConvertToWorldUnitsX(Input.mousePosition.x) - ConvertToWorldUnitsX(m_vec3MousePos.x)),m_GameManager.ComputerScreen.transform.position.x,m_GameManager.ComputerScreen.transform.position.x + m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.x-m_sprTitleBar.bounds.size.x);
            moveVec.y = Mathf.Clamp(transform.position.y + (ConvertToWorldUnitsY(Input.mousePosition.y) - ConvertToWorldUnitsY(m_vec3MousePos.y)),m_GameManager.ComputerScreen.transform.position.y - m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.y+m_sprContent.bounds.size.y+m_sprTitleBar.bounds.size.y,m_GameManager.ComputerScreen.transform.position.y);
            moveVec.z = 0;
            transform.SetPositionAndRotation(moveVec,Quaternion.identity);
            m_vec3MousePos = Input.mousePosition;
        }
    }
    
}
