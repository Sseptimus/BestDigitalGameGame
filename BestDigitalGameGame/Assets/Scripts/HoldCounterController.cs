using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoldCounterController : MonoBehaviour
{
    public TextMeshProUGUI m_Timer;
    public TextMeshProUGUI m_Number;

    public TextMeshProUGUI m_NumberDropShadow;

    public void UpdateNumber(int _iNumber)
    {
        m_Number.text = _iNumber.ToString();
        m_NumberDropShadow.text = _iNumber.ToString();
    }

    private void Start()
    {
        m_Timer.overrideColorTags = true;
    }

    private void Update()
    {
        //Each 'hour' will take 1.875 real minutes
        float fFakeTotalSeconds = Time.time * 1.875f;
        int iHours = 8 + Mathf.FloorToInt(fFakeTotalSeconds / 3600);
        iHours -= Mathf.FloorToInt(iHours / 24) * 24;
        int iMinutes = Mathf.FloorToInt(fFakeTotalSeconds%3600/60);
        int iSeconds = Mathf.FloorToInt(fFakeTotalSeconds%60);
        int iMilliSeconds = Mathf.FloorToInt(fFakeTotalSeconds*1000%1000);
        m_Timer.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",iHours,iMinutes,iSeconds,iMilliSeconds);
        m_Timer.color = new Color32(255, (byte)Mathf.Clamp(-28.333f * (iHours-8) + 255f,0,255), (byte)Mathf.Clamp(-28.333f * (iHours-8) + 255f,0,255),255);
    }
}
