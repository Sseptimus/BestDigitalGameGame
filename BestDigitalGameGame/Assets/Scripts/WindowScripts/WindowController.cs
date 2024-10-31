using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class WindowController :  BaseWindowClass
{
    private bool m_bHeld;
    private Vector3 m_vec3MousePos;
    private SpriteRenderer m_sprContent;
    private SpriteRenderer m_sprTitleBar;
    private GameManager m_GameManager;
    private BoxCollider2D m_ColTitleBar;

    private void Start()
    {
        m_sprContent = gameObject.transform.Find("Content").GetComponent<SpriteRenderer>();
        m_sprTitleBar = gameObject.transform.Find("TitleBar").GetComponent<SpriteRenderer>();
        m_GameManager = FindObjectOfType<GameManager>();
        m_ColTitleBar = transform.GetComponent<BoxCollider2D>();
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

    public void Minimise()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (m_bHeld && (ConvertToWorldUnitsX(Input.mousePosition.x) > m_GameManager.ComputerScreen.transform.position.x + m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.x
            || ConvertToWorldUnitsX(Input.mousePosition.x) < m_GameManager.ComputerScreen.transform.position.x
            || ConvertToWorldUnitsY(Input.mousePosition.y) > m_GameManager.ComputerScreen.transform.position.y
            || ConvertToWorldUnitsY(Input.mousePosition.y) < m_GameManager.ComputerScreen.transform.position.y - m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.y))
        {
            m_bHeld = false;
        }
        if (m_bHeld)
        {
            Vector3 moveVec;
            moveVec.x = Mathf.Clamp(transform.position.x + (ConvertToWorldUnitsX(Input.mousePosition.x) - ConvertToWorldUnitsX(m_vec3MousePos.x)),m_GameManager.ComputerScreen.transform.position.x,m_GameManager.ComputerScreen.transform.position.x + m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.x-m_sprTitleBar.bounds.size.x);
            moveVec.y = Mathf.Clamp(transform.position.y + (ConvertToWorldUnitsY(Input.mousePosition.y) - ConvertToWorldUnitsY(m_vec3MousePos.y)),m_GameManager.ComputerScreen.transform.position.y - m_GameManager.Background.GetComponent<SpriteRenderer>().bounds.size.y+m_sprContent.bounds.size.y+m_sprTitleBar.bounds.size.y,m_GameManager.ComputerScreen.transform.position.y);
            moveVec.z = 0;
            
            transform.SetPositionAndRotation(moveVec,Quaternion.identity);
            m_vec3MousePos = Input.mousePosition;
        }
    }
    
}
