using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneTutorial : MonoBehaviour
{
    public List<string> textContent = new List<string>();

    public int idx = 0;
    int old = -1;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponentInChildren<Text>().text = "TESTING";
    }

    // Update is called once per frame
    void Update()
    {
        if (old != idx && idx >= 0)
        {
            gameObject.GetComponentInChildren<Text>().text = textContent[idx];
            old = idx;
        }
        //gameObject.GetComponentInChildren<Text>().text = "TESTING";
    }

    public void PushText(string text)
    {
        textContent.Add(text);
    }
}
