using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public BossManager MainBossManager;
    public PostProcessVolume ScenePostProcessLayer;
    public float VingetteIntensity = 0.16f;
    public float BossVingetteInt = 0.26f;
    public float GrainIntentisy = 0.46f;
    public float BossGrainInt = 0.76f;
    public float GrainSize = 1.27f;
    public float BossGrainSize = 2.27f;
    public float FadeSpeed;
    float LerpTimer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        ScenePostProcessLayer.profile.GetSetting<Vignette>().intensity.value = VingetteIntensity;
        ScenePostProcessLayer.profile.GetSetting<Grain>().intensity.value = GrainIntentisy;
        ScenePostProcessLayer.profile.GetSetting<Grain>().size.value = GrainSize;
    }

    // Update is called once per frame
    void Update()
    {
        //if boss is watching the player, lerp the post processing to make it scawy
        if(MainBossManager.CurrentLocation == BossManager.BossLocation.Right_Behind_YOU)
        {
            if(LerpTimer < 1){LerpTimer += FadeSpeed * Time.deltaTime;}
        }
        else
        {
            if(LerpTimer > 0){LerpTimer -= FadeSpeed * Time.deltaTime;}
        }
        ScenePostProcessLayer.profile.GetSetting<Vignette>().intensity.value = math.lerp(VingetteIntensity, BossVingetteInt, LerpTimer);
        ScenePostProcessLayer.profile.GetSetting<Grain>().intensity.value = math.lerp(GrainIntentisy, BossGrainInt, LerpTimer);
        ScenePostProcessLayer.profile.GetSetting<Grain>().size.value = math.lerp(GrainSize, BossGrainSize, LerpTimer);
    }
}
