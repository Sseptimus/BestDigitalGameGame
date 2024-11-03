using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void StartGame() // Starts game by loading main scene
    {
        Debug.Log("Staring Game");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame() // Quits Application, and editor if still editing
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
