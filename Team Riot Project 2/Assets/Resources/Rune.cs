using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using v3 = UnityEngine.Vector3;
public class Rune : MonoBehaviour
{
    private Vector3 pos;
    private bool isDragging;
    public string answer;
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
            GameObject[] runes = GameObject.FindGameObjectsWithTag("rune");
            foreach (var rune in runes)
            {
                if (IsColliding(rune))
                {
                    string a1 = rune.GetComponent<Rune>().answer;
                    if(a1 == answer)
                    {
                        SelfDestruct(rune);
                        SelfDestruct(this.gameObject);
                        Debug.Log("FOUND");
                        //mmDebug.Log(answer);
                    }
                    else
                    {
                        transform.position = pos;
                    }
                    
                }
            }
        }
        GameObject openscroll = GameObject.FindGameObjectWithTag("openscroll");
        bool dis = openscroll.GetComponent<OpenScroll>().display;
        if (dis == false)
        {
            this.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = true;
            
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

    public bool IsColliding(GameObject obj)
    {
        v3 n_pos = obj.transform.position;
        v3 mypos = this.transform.position;
        v3 diff = new v3(0, 0, 0);
        
        diff.x = Math.Abs(n_pos.x - mypos.x);
        diff.y = Math.Abs(n_pos.y - mypos.y);

        if(diff.x <0.5 && diff.y < 0.5 && diff.x >0 && diff.y > 0)
        {
            //Debug.Log("TEST");
            //Debug.Log(n_pos);
            //Debug.Log(diff);

            return true;
        }

        
        return false;
    }

    void SelfDestruct(GameObject _obj)
    {
        Destroy(_obj);
    }
}
