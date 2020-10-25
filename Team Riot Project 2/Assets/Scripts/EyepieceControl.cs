using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyepieceControl : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 startPosition = Vector2.zero;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    public void PointerEnter()
    {
        transform.position += Vector3.up * 40.0f;
    }

    public void PointerExit()
    {
        if(transform.position.y > startPosition.y)
        {
            transform.position = startPosition;
        }
    }

    public void Click()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        GetComponent<AudioSource>().Play();
        PlayerManager.instance.currentTool = PlayerTool.Eyepiece;
        transform.position = startPosition;
    }
}
