using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class WindowController :  BaseWindowClass
{
    public bool m_bHeld;
    private Vector3 m_vec3MousePos;
    [SerializeField] private GameObject self;
    public GameObject ComputerScreen;
    public GameObject Background;
    public SpriteRenderer Content;
    public SpriteRenderer TitleBar;

    private void OnMouseDown()
    {
        m_bHeld = true;
        m_vec3MousePos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        m_bHeld = false;
    }
    public override void Click(ClickType _clickType)
    {
        switch (_clickType)
        {
            case ClickType.Minimise:
                self.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        if (m_bHeld && (ConvertToWorldUnitsX(Input.mousePosition.x) > ComputerScreen.transform.position.x + Background.GetComponent<SpriteRenderer>().bounds.size.x
            || ConvertToWorldUnitsX(Input.mousePosition.x) < ComputerScreen.transform.position.x
            || ConvertToWorldUnitsY(Input.mousePosition.y) > ComputerScreen.transform.position.y
            || ConvertToWorldUnitsY(Input.mousePosition.y) < ComputerScreen.transform.position.y - Background.GetComponent<SpriteRenderer>().bounds.size.y))
        {
            m_bHeld = false;
        }
        if (m_bHeld)
        {
            Vector3 moveVec;
            moveVec.x = Mathf.Clamp(transform.position.x + (ConvertToWorldUnitsX(Input.mousePosition.x) - ConvertToWorldUnitsX(m_vec3MousePos.x)),ComputerScreen.transform.position.x,ComputerScreen.transform.position.x + Background.GetComponent<SpriteRenderer>().bounds.size.x-TitleBar.bounds.size.x);
            moveVec.y = Mathf.Clamp(transform.position.y + (ConvertToWorldUnitsY(Input.mousePosition.y) - ConvertToWorldUnitsY(m_vec3MousePos.y)),ComputerScreen.transform.position.y - Background.GetComponent<SpriteRenderer>().bounds.size.y+Content.bounds.size.y+TitleBar.bounds.size.y,ComputerScreen.transform.position.y);
            moveVec.z = 0;
            
            transform.SetPositionAndRotation(moveVec,Quaternion.identity);
            m_vec3MousePos = Input.mousePosition;
        }
    }
    
}
