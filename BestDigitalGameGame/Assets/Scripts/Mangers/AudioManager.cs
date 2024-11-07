using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float whisperTimer = 15.0f;
    [SerializeField] AudioSource whisperAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whisperTimer <= 0)
        {
            whisperAudio.Play();
            whisperAudio.volume = Random.Range(0.55f, 1);
            whisperTimer = Random.Range(15, 35);
        }
        whisperTimer -= Time.deltaTime;
    }
}
