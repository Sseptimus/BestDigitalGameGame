using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public Button startGameButton;
    public GameObject logoObject;
    public Texture startIMG;
    public Texture animationVid;
    public sceneCrossfade sceneCrossfade;
    private RawImage img;

    void Start()
    {
        img = (RawImage)logoObject.GetComponent<RawImage>();
        img.texture = startIMG;
        
        startGameButton.onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        img.texture = animationVid;
        videoPlayer.Play();
        Invoke("StartGame", 3);
    }

    public void StartGame() // Starts game by loading main scene
    {
        videoPlayer.Play();
        sceneCrossfade.ToggleFading();
        Invoke("ChangeScene", 1);
    }

    public void ExitGame() // Quits Application, and editor if still editing
    {
        sceneCrossfade.ToggleFading();
        Invoke("Quit", 2);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Quit()
    {
        Application.Quit();
    }
}
