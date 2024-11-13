using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class OutroPostProsFadeUp : MonoBehaviour
{
    public float OutroFadeSpeed;
    public float GrainFadeSpeed;
    public PostProcessVolume ScenePostProcessLayer;

    // Update postprocessing to make sure it 
    void Update()
    {
        //slow adjust vingette
        if(ScenePostProcessLayer.profile.GetSetting<Vignette>().intensity.value >= 0.299)
        {ScenePostProcessLayer.profile.GetSetting<Vignette>().intensity.value -= OutroFadeSpeed;}
        //slow adjust film grain intensity and size
        if(ScenePostProcessLayer.profile.GetSetting<Grain>().intensity.value < 1.0f)
        {ScenePostProcessLayer.profile.GetSetting<Grain>().intensity.value += GrainFadeSpeed;}

        if(ScenePostProcessLayer.profile.GetSetting<Grain>().size.value < 2.0f)
        {ScenePostProcessLayer.profile.GetSetting<Grain>().size.value += 2 * GrainFadeSpeed;}
    }
}
