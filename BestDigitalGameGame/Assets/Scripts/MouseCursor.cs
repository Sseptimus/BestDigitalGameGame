using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // texture for mouse
    public CursorMode cursorMode = CursorMode.Auto; // sets hardware or software mode
    public Vector2 hotSpot = Vector2.zero; // sets the offset for clicking
    public Texture2D defaultMouseTex;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        Cursor.SetCursor(defaultMouseTex, Vector2.zero, cursorMode);
    }
}
