using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroSceneManager : MonoBehaviour
{
    private VideoPlayer m_introVideo;

    private bool m_bHasStarted;
    // Start is called before the first frame update
    void Start()
    {
        m_introVideo = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_introVideo.isPlaying)
        {
            //To ensure it doesn't change before the video plays
            m_bHasStarted = true;
        }
        if (!m_introVideo.isPlaying && m_bHasStarted)
        {
           SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}
