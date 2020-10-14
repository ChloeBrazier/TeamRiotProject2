using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButton : MonoBehaviour
{

    string tagName;
    // Start is called before the first frame update
    void Start()
    {
        tagName = this.tag;
        //this.add
        //Debug.Log(tagName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clicked()
    {
        Debug.Log("Clicked");
    }

    void OnMouseEnter()
    {
        Debug.Log(tagName);
    }
}
