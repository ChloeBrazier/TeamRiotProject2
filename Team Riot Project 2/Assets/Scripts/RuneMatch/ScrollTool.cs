using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using v3 = UnityEngine.Vector3;
public class ScrollTool : MonoBehaviour
{
    public GameObject scroll;
    private GameObject openscroll = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        /*Do whatever here as per your need*/
        if (scroll != null)
            return;
        else
            scroll = Instantiate(openscroll, new v3(0, 0, -6), Quaternion.identity);
    }

}
