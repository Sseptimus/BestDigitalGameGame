using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour 
{
        // Pipe a string name here in case you want to use something other than default device (0)
        private string _device;

        // Recording settings
        [Header("Recording Settings")]
        public bool m_LoopRecording = true;
        public int m_SecondsToRecord = 999;
        public int m_RecordingSampleRate = 44100;

        public int m_MaxVolumeSampleWindow = 2048; // can run this smaller, but you'll have lots of wobble in the read. bigger number is larger sample space

        // Demonstration of the volume working
        [Header("Bar Demonstration")]
        public Transform m_Bar;
        public Transform m_BarIndicator;
        public Vector3 m_BarSize = new Vector3(0.5f, 4.0f, 0.1f);

        // Defines a min/max so that we can easily adjust the ranges of sounds we're looking at
        // we'll clamp volume to this range, so values outside this range will be clamped at the edges
        public Vector2 m_VolumeRange = new Vector2(0.0f, 0.5f);

        // Internal clip used for the recording. Initialized in InitializeMicrophoneRecording
        private AudioClip clipRecord;
        
        // Internal tracking of whether the microphone is active/initialized
        private bool isRunning;

        // Mic and recording initialization
        private void InitializeMicrophoneRecording()
        {
            if (_device == null || _device == "") 
            {
                _device = Microphone.devices[0];
            }
            clipRecord = Microphone.Start(_device, m_LoopRecording, m_SecondsToRecord, m_RecordingSampleRate);
            isRunning = true;
        }

        // Halts the recording
        private void StopMicrophone()
        {
            Microphone.End(_device);
            isRunning = false;
        }

        // Samples the data in the microphone to get a peak volume over the last n samples
        // Will return -1 if invalid sampling conditions
        private float GetLevelMax(int _sampleWindow)
        {
            float levelMax = -1.0f;
            if (!isRunning)
            {
                return levelMax;
            }
            
            float[] waveData = new float[_sampleWindow];
            int micPosition = Microphone.GetPosition(_device) - (_sampleWindow + 1); // can also pipe null into the microphone device functions to get the default
            if (micPosition < 0) // Not initialized
            {
                return levelMax;
            }
            
            clipRecord.GetData(waveData, micPosition);
            // Getting a peak on the last n samples
            for (int i = 0; i < _sampleWindow; i++) 
            {
                //float wavePeak = waveData[i] * waveData[i]; // original version, edit below
                float wavePeak = waveData[i]; // this was originally powered, I have swapped it off of this, not sure if this mathematically correct but seems to have better results
                if (levelMax < wavePeak) 
                {
                    levelMax = wavePeak;
                }
            }
            return levelMax;
        }

        private void OnValidate()
        {
            m_Bar.transform.localScale = m_BarSize;
        }

        private void Update()
        {
            // GetLevelMax quals to the highest normalized value, a small number because < 1 (it was originally powered, I have changed that)
            float micVolume = GetLevelMax(m_MaxVolumeSampleWindow);
            // Rebinding to a different range because it seems super quiet
            float invLerpVolume = Mathf.InverseLerp(m_VolumeRange.x, m_VolumeRange.y, Mathf.Clamp(micVolume, m_VolumeRange.x, m_VolumeRange.y));

            // Demonstration
            m_Bar.transform.localScale = m_BarSize;
            Vector3 barBot = m_Bar.transform.position - m_Bar.transform.up * m_BarSize.y * 0.5f;
            Vector3 barTop = m_Bar.transform.position + m_Bar.transform.up * m_BarSize.y * 0.5f;
            m_BarIndicator.transform.position = Vector3.Lerp(barBot, barTop, invLerpVolume);

            // Comment this out to remove debug output
            Debug.Log(invLerpVolume);
        }

        // Start mic when scene starts
        private void OnEnable()
        {
            if (!isRunning)
            {
                InitializeMicrophoneRecording();
            }
        }

        // Stop mic when loading a new level or quitting application
        private void OnDisable()
        {
            if (isRunning)
            {
                StopMicrophone();
            }
        }

        // Close mic if destroyed
        private void OnDestroy()
        {
            if (isRunning)
            {
                StopMicrophone();
            }
        }

        // Make sure the mic gets started & stopped when application gets focused - not sure if this is needed but saw this in a code snippet so have included :)
        private void OnApplicationFocus(bool _focus) 
        {
            if (_focus && !isRunning)
            {
                InitializeMicrophoneRecording();
            }      
            else if (!_focus && isRunning)
            {
                StopMicrophone();
            }
        }
    }