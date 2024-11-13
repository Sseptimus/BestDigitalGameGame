using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoldCounterController : MonoBehaviour
{
    public TextMeshProUGUI m_Number;

    public TextMeshProUGUI m_NumberDropShadow;

    public void UpdateNumber(int _iNumber)
    {
        m_Number.text = _iNumber.ToString();
        m_NumberDropShadow.text = _iNumber.ToString();
    }
}
