using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float whisperTimer = 15.0f;
    [SerializeField] AudioSource whisperAudio;
    [SerializeField] AudioSource breathingAudio;
    public BossManager BossManager;

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
            whisperAudio.volume = Random.Range(0.25f, 0.75f);
            whisperTimer = Random.Range(15, 35);
        }
        whisperTimer -= Time.deltaTime;

        if (BossManager.BOSS_IS_WATCHING)
        {
            breathingAudio.volume = 0.9f;
            whisperAudio.volume = Random.Range(0.25f, 1.0f);
        }
        else
        {
            breathingAudio.volume = 0.382f;
        }
    }
}
