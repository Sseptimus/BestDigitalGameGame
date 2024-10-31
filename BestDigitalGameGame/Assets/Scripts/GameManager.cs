using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("Things to Referance")]
    public GameObject ComputerScreen;
    public GameObject Background;

    public WindowController WindowInFocus;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
