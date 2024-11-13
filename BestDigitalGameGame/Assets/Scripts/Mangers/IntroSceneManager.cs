using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroSceneManager : MonoBehaviour
{
    private VideoPlayer m_introVideo;
    public sceneCrossfade sceneCrossfade;

    private bool m_bHasStarted;
    // Start is called before the first frame update
    void Start()
    {
        sceneCrossfade.ToggleFading();
        m_introVideo = GetComponent<VideoPlayer>();
        Invoke("ChangeScene", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_introVideo.isPlaying)
        {
            //To ensure it doesn't change before the video plays
            m_bHasStarted = true;
        }
    }

    void ChangeScene()
    {
        Invoke("ChangeSceneForReal", 1);
        sceneCrossfade.ToggleFading();
    }

    void ChangeSceneForReal()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
