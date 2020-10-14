using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButton : MonoBehaviour
{

    string tagName;
    
    public GameObject tutorialBox;
    // Start is called before the first frame update
    void Start()
    {
        tagName = this.tag;
        tutorialBox = GameObject.FindGameObjectWithTag("tutorialBox");
        //this.add
        Debug.Log(tagName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeIDX()
    {

    }

    public void Clicked()
    {
        if(tagName == "esc")
        {
            
            var forward = GameObject.FindGameObjectWithTag("forward");
            var backward = GameObject.FindGameObjectWithTag("backward");
            Destroy(tutorialBox);
            Destroy(forward);
            Destroy(backward);
            Destroy(gameObject);
        }
        
        if (tagName == "forward")
        {
      
          
            tutorialBox.GetComponent<RuneTutorial>().idx++;
        }
        if (tagName == "backward")
        {
           
            tutorialBox.GetComponent<RuneTutorial>().idx--;
        }



        Debug.Log("Clicked");
    }

    void OnMouseEnter()
    {
        Debug.Log(tagName);
    }
}
