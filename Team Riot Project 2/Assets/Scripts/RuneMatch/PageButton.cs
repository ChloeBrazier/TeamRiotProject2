using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageButton : MonoBehaviour
{

    string tagName;
    
    public GameObject tutorialBox;
    GameObject introbox;
    GameObject startButton; 
    // Start is called before the first frame update
    void Start()
    {
        tagName = this.tag;
        tutorialBox = GameObject.FindGameObjectWithTag("tutorialBox");
        startButton = GameObject.FindGameObjectWithTag("startTutorial");
        introbox = GameObject.FindGameObjectWithTag("introBox");

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

        

        if (tagName == "esc")
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

            if(introbox != null)
            {
                introbox.GetComponent<TutorialBox>().idx++;
            }
            if (tutorialBox != null)
            {
                tutorialBox.GetComponent<TutorialBox>().idx++;
            }
            
        }
        if (tagName == "backward")
        {
            if (introbox != null)
            {
                introbox.GetComponent<TutorialBox>().idx--;
            }
            if (tutorialBox != null)
            {
                tutorialBox.GetComponent<TutorialBox>().idx--;
            }
        }

        if(tagName == "startTutorial")
        {
            
            Destroy(GameObject.FindGameObjectWithTag("introBox"));
            //SceneManager.LoadScene("TutorialTaurian", LoadSceneMode.Additive);
            Destroy(gameObject);
        }



        Debug.Log("Clicked");
    }
}
