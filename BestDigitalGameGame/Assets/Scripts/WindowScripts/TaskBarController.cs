using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBarController : MonoBehaviour
{
    public GameManager m_gameManager;
    public GameObject m_TaskIconPrefab;

    [Header("TaskBar Icons")] 
    public Sprite m_sprChat;
    public Sprite m_sprChimpGame;

    private void Start()
    {
        if (!m_gameManager)
        {
            m_gameManager = FindObjectOfType<GameManager>();
        }

        m_gameManager.m_TaskBarController = this;
    }

    public void ReopenWindow(GameObject _IconPressed)
    {
        Debug.Log(_IconPressed.transform.GetSiblingIndex());
        Debug.Log(m_gameManager.OpenWindows.Count);
        m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].gameObject.SetActive(true);
        m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].OnGrabFocus();
    }
    
    public void WindowOpened(WindowController _newWindow)
    {
        GameObject newIcon = Instantiate(m_TaskIconPrefab, transform);
        switch (_newWindow.GetComponent<WindowController>().m_WindowType)
        {
            case GameManager.WindowType.Chat:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChat;
                break;
            case GameManager.WindowType.ChimpGame:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChimpGame;
                break;
            default:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChat;
                break;
        }
        newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChat;
    }

    public void WindowClosed(int _iIndex)
    {
        Destroy(transform.GetChild(_iIndex).gameObject);
    }
}
