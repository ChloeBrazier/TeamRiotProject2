using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using v3 = UnityEngine.Vector3;
public class Rune : MonoBehaviour
{
    private Vector3 pos;
    private bool isDragging;

    private float z;
    public float dragSpeed = 1f;
    Vector3 lastMousePos;

    void Start()
    {
        pos = this.transform.position;
        this.z = this.pos.z;
        // pos.z = -7;
        Vector3 theScale = this.transform.localScale * 0.8f;
        this.transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }


    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
        pos.z = -7;
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }
}
