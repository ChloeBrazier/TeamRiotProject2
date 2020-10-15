using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialBox : MonoBehaviour
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
        //gameObject.GetComponentInChildren<Text>().text = textContent[idx];
        if (idx >= textContent.Count)
        {
            idx = textContent.Count - 1;
        }
        if (idx < 0)
        {
            idx = 0;
        }
        if (old != idx)
        {
            gameObject.GetComponentInChildren<Text>().text = textContent[idx];
            old = idx;
        }
        Debug.Log(idx);
        //Debug.Log(textContent.Count);
        //gameObject.GetComponentInChildren<Text>().text = "TESTING";
    }

    public void PushText(string text)
    {
        textContent.Add(text);
    }
}
