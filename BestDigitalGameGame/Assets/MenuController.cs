using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void StartGame() // Starts game by loading main scene
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ExitGame() // Quits Application, and editor if still editing
    {
        Application.Quit();
        EditorApplication.Exit(0);
    }
}
