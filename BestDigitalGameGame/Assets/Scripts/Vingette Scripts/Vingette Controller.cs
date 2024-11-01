using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class VingetteController : MonoBehaviour
{

    enum State//what is te fade currently doing
    {
        FadingOut,
        FadingIn
    }
    public enum TargetScene//what is the current scene to move too
    {
        Cubical,
        Screen,
        LeftPeek,
        UpPeek,
        Pinboard
    }

    public Camera MainCamera;

    [Header ("Scene Positions")]//final camera positions of each place. can be hard coded, left public for ease
     public Vector3 Screen;
     public Vector3 Cubical;
     public Vector3 LeftPeek;
     public Vector3 UpPeek;
     public Vector3 Pinboard;
     public TargetScene m_TargetScene;

    [Header ("Fade Variables")]//speed and end size of the mask. No Z coor, because we dont fade it
     public float FadeSpeed;
     public float MaxXScale;
     public float MaxYScale;

    //these are variables for the Scene moving
    private float m_CurrentLerp;
    private State m_State;
    public bool m_Fading;
    private float xScale;
    private float yScale;

    void Update()
    {
        if(m_Fading)//if meant to be fading, run the transition
        {
            Transition(m_TargetScene);
        }
    }

    // Transition runs the fade out of the scene, then changes the background scene, then fades in the scene.
    void Transition(TargetScene _TargetScene)
    {
        switch(m_State)//checks with action to take. Loops every frame if called until move is finished
        {
            case State.FadingOut://lerps the x scale of the mask to zero

                xScale = math.lerp(MaxXScale, 0, m_CurrentLerp);//get the current y scale
                yScale = math.lerp(MaxYScale, 0, m_CurrentLerp);//get the current x scale
                gameObject.transform.localScale = new Vector3(xScale,yScale,1);//set the scales. dont scale Z axis

                m_CurrentLerp += FadeSpeed * Time.deltaTime;//incriment lerp by fade speed
                if(m_CurrentLerp >= 1)
                {
                    switch(m_TargetScene)//if the lerp is done, move the camera to the new position
                    {   
                        case TargetScene.Cubical:
                        MainCamera.transform.position = Cubical;
                        break;

                        case TargetScene.Screen:
                        MainCamera.transform.position = Screen;
                        break;

                        case TargetScene.LeftPeek:
                        MainCamera.transform.position = LeftPeek;
                        break;

                        case TargetScene.Pinboard:
                        MainCamera.transform.position = Pinboard;
                        break;

                        case TargetScene.UpPeek:
                        MainCamera.transform.position = UpPeek;
                        break;
                    }
                    m_CurrentLerp = 0;
                    m_State = State.FadingIn;
                }
            break;
            case State.FadingIn://if its done moving, and meant to be fading in
            
                xScale = math.lerp(0, MaxXScale, m_CurrentLerp);//get the current y scale
                yScale = math.lerp(0, MaxYScale, m_CurrentLerp);//get the current x scale
                gameObject.transform.localScale = new Vector3(xScale,yScale,1);//set the scales. dont scale Z axis
                m_CurrentLerp += FadeSpeed * Time.deltaTime;//incriment lerp by fade speed back down again

                if(m_CurrentLerp >= 1)//if the lefp is finished again, end the loop
                {
                    m_Fading = false;//end the loop
                    m_State = State.FadingOut;//reset for next time
                    m_CurrentLerp = 0;
                }
            break;
        }
    }
}
