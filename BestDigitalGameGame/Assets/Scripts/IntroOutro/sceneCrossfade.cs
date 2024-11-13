using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneCrossfade : MonoBehaviour
{
    public float fadeSpeed;
    float currentOpactity = 1.0f;
    bool FadeIn = true;
    public SpriteRenderer fadeSprite;

    // Update is called once per frame
    void Update()
    {
        if (FadeIn && currentOpactity > 0){ currentOpactity -= fadeSpeed * Time.deltaTime; }
        else if (!FadeIn && currentOpactity < 1) { currentOpactity += fadeSpeed * Time.deltaTime; }

        fadeSprite.color = new Color(1, 1, 1, currentOpactity);
    }

    public void ToggleFading()
    { FadeIn = !FadeIn; }
}
