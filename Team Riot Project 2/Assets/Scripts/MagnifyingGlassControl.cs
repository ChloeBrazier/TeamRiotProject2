using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassControl : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Update is called once per frame
    public void PointerEnter()
    {
        transform.position += Vector3.up * 40.0f;
    }

    public void PointerExit()
    {
        transform.position += Vector3.down * 40.0f;
    }

    public void Click()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
