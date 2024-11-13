using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum WindowType
    {
        Chat,
        ChimpGame,
        MineSweeper,
        SliderGame,
        PopUp,
        Counter
    }
    
    //Singleton Class should only be one in scene

    [Header ("Helper Counter")]
    public int m_iHelperCounter;

    [Header ("Suspicion")]
    public int m_iSuspicion;
    
    [Header ("Things to Referance")]
    public GameObject ComputerScreen;
    public GameObject Background;

    public WindowController WindowInFocus;

    public List<WindowController> OpenWindows;

    public TaskBarController m_TaskBarController;

    // Start is called before the first frame update
    void Start()
    {

        if (!WindowInFocus)
        {
            WindowInFocus = FindObjectOfType<WindowController>();
        }

        if (FindObjectOfType<GameManager>() != this)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            SceneManager.LoadScene("Main Menu");
        }

    }

    public void AddWindow(WindowController _newWindow)
    {
        OpenWindows.Add(_newWindow);
        m_TaskBarController.WindowOpened(_newWindow);
    }

    public void RemoveWindow(WindowController _removedWindow)
    {
        m_TaskBarController.WindowClosed(OpenWindows.IndexOf(_removedWindow));
        OpenWindows.Remove(_removedWindow);
    }

    public void MinimiseWindow(WindowController _minimisedWindow)
    {
        m_TaskBarController.WindowMinimised(_minimisedWindow);
    }
}
