using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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
        if (openscroll != null)
        {
            bool dis = openscroll.GetComponent<OpenScroll>().display;
            if(dis == true)
            {
                openscroll.GetComponent<OpenScroll>().display = false;
                return;
            }
            if (dis == false)
            {
                openscroll.GetComponent<OpenScroll>().display = true;
                return;
            }
        }
        else if (openscroll == null)
        {
            openscroll = Instantiate(scroll, new v3(0, 0, -6), Quaternion.identity);
            openscroll.GetComponent<OpenScroll>().display = true;
        }

            
    }

}
