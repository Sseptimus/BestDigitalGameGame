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
    public Sprite m_sprChatMin;
    public Sprite m_sprChimpGame;
    public Sprite m_sprChimpGameMin;
    public Sprite m_sprMineGame;
    public Sprite m_sprMineGameMin;
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
        if (!m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].gameObject.activeSelf)
        {
            m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].gameObject.SetActive(true);
            m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].OnGrabFocus();
            switch (m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].m_WindowType)
            {
                case GameManager.WindowType.Chat:
                    transform.GetChild(_IconPressed.transform.GetSiblingIndex()).GetComponent<SpriteRenderer>()
                        .sprite = m_sprChat;
                    break;
                case GameManager.WindowType.ChimpGame:
                    transform.GetChild(_IconPressed.transform.GetSiblingIndex()).GetComponent<SpriteRenderer>()
                        .sprite = m_sprChimpGame;
                    break;
                case GameManager.WindowType.SliderGame:
                    transform.GetChild(_IconPressed.transform.GetSiblingIndex()).GetComponent<SpriteRenderer>()
                        .sprite = m_sprChimpGame;
                    break;
                case GameManager.WindowType.MineSweeper:
                    transform.GetChild(_IconPressed.transform.GetSiblingIndex()).GetComponent<SpriteRenderer>()
                        .sprite = m_sprMineGame;
                    break;
                default:
                    break;
            }
        }
        else
        {
            m_gameManager.OpenWindows[_IconPressed.transform.GetSiblingIndex()].Minimise();
        }
        
    }
    
    public void WindowOpened(WindowController _newWindow)
    {
        GameObject newIcon = Instantiate(m_TaskIconPrefab, transform);
        switch (_newWindow.m_WindowType)
        {
            case GameManager.WindowType.Chat:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChat;
                break;
            case GameManager.WindowType.ChimpGame:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChimpGame;
                break;
            case GameManager.WindowType.SliderGame:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprChimpGame;
                break;
            case GameManager.WindowType.MineSweeper:
                newIcon.GetComponent<SpriteRenderer>().sprite = m_sprMineGame;
                break;
        }
    }

    public void WindowMinimised(WindowController _minimisedWindow)
    {
        switch (_minimisedWindow.m_WindowType)
        {
            case GameManager.WindowType.Chat:
                transform.GetChild(m_gameManager.OpenWindows.IndexOf(_minimisedWindow)).GetComponent<SpriteRenderer>()
                    .sprite = m_sprChatMin;
                break;
            case GameManager.WindowType.ChimpGame:
                transform.GetChild(m_gameManager.OpenWindows.IndexOf(_minimisedWindow)).GetComponent<SpriteRenderer>()
                    .sprite = m_sprChimpGameMin;
                break;
            case GameManager.WindowType.MineSweeper:
                transform.GetChild(m_gameManager.OpenWindows.IndexOf(_minimisedWindow)).GetComponent<SpriteRenderer>()
                    .sprite = m_sprMineGameMin;
                break;
            case GameManager.WindowType.SliderGame:
                transform.GetChild(m_gameManager.OpenWindows.IndexOf(_minimisedWindow)).GetComponent<SpriteRenderer>()
                    .sprite = m_sprChimpGameMin;
                break;
            default:
                break;
        }
        
    }

    public void WindowClosed(int _iIndex)
    {
        Destroy(transform.GetChild(_iIndex).gameObject);
    }
}
