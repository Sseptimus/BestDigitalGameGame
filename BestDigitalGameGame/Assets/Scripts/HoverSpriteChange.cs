using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSpriteChange : MonoBehaviour
{
    public Sprite m_sprHover;
    public Sprite m_sprDefault;
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = m_sprHover;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = m_sprDefault;
    }
}
