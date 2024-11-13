using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public Sprite hoverBackground;
    public Sprite normalBackground;

    // change background on hover
    private void OnMouseEnter()
    {
        GetComponentInParent<SpriteRenderer>().sprite = hoverBackground;
    }

    private void OnMouseExit()
    {
        GetComponentInParent<SpriteRenderer>().sprite = normalBackground;
    }
}
