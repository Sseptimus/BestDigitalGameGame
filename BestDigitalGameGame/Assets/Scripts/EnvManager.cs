using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    private float clipLoudness;

    private string microphoneDevice;
    private AudioSource audioSource;
    
    private int sampleSize = 256;
    
    private float[] samples = new float[256];
    
    public GameObject tri;
    
    void Start()
    {
        microphoneDevice = Microphone.devices[0];
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        StartMicrophone();
    }
    
    
    void Update()
    {
        tri.transform.SetPositionAndRotation(new Vector3(0,GetMicrophoneVolume()+80,0), quaternion.identity);
        Debug.Log(GetMicrophoneVolume());
    }
    
    void StartMicrophone()
    {
        audioSource.clip = Microphone.Start(microphoneDevice, true, 1, AudioSettings.outputSampleRate);
        while (!(Microphone.GetPosition(microphoneDevice) > 0)) { }
        audioSource.Play();
        
        samples = new float[sampleSize];
    }
    float GetMicrophoneVolume()
    {
        audioSource.GetOutputData(samples, 0);
        float sum = 0f;
        foreach (float sample in samples)
        {
            sum += sample * sample;
        }
        float avg = Mathf.Sqrt(sum / sampleSize);
        float decibel = 20 * Mathf.Log10(avg / 0.1f);

        return decibel;
    }
}
