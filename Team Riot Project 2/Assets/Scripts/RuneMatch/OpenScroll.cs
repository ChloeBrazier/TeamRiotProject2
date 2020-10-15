using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using v3 = UnityEngine.Vector3;
public class OpenScroll : MonoBehaviour
{

    public bool display;
    GameObject[] sprites;
    UnityEngine.Object[] runetext;
    UnityEngine.Object[] lettertext;
    bool mazeBuilt = false;
    public GameObject runetut;
    Button textbutton;
    public GameObject gameUI;
    GameObject playerManager;
    public bool mazeWon = false;
    bool tutorial;
    // Start is called before the first frame update
    void Start()
    {
        runetext = Resources.LoadAll("runes", typeof(Texture2D));
        lettertext = Resources.LoadAll("Letters", typeof(Texture2D));
        playerManager = GameObject.FindGameObjectWithTag("playerManager");
        gameUI = GameObject.FindGameObjectWithTag("interface");
        this.tag = "openscroll";
        //Debug.Log(lettertext[0].name);
        sprites = new GameObject[runetext.Length];
        tutorial = playerManager.GetComponent<PlayerManager>().tutorial;
        //gameUI = GameObject.FindGameObjectWithTag("interface");
        v3 textpos = new v3(151, 275, 0);
        if(tutorial == false)
        {
            runetut = Instantiate(runetut, textpos, Quaternion.identity);
            //runetut.GetComponentInChildren<Text>().text = "TESTING";
            runetut.transform.parent = gameUI.transform;
            runetut.AddComponent<RuneTutorial>();
            runetut.GetComponent<RuneTutorial>().PushText("Rune Matching Tutorial:\n\n\n" + " \nThis is one of the enchantment games" +
                " you will be playing to disenchant a weapon.");
            runetut.GetComponent<RuneTutorial>().PushText("Rune Matching Tutorial:\n\n\n" + "\nEach side of the scroll will have a set of runes to match with letters. " +
                "Click and drag the correct rune with the letter");
            runetut.GetComponent<RuneTutorial>().PushText("Rune Matching Tutorial:\n\n\n" + "\nUse trial and error to test the correct matching runes to disenchant the weapon.");

                //Debug.Log(gameUI);
                //Debug.Log(runetut);
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        
        //disabled display
        if (display == false)
        {
            this.GetComponent<Renderer>().enabled = false;
            

        }
        else //enabled display
        {
            //tutorial run 
            if (mazeBuilt == false && tutorial == false)
            {
                //Debug.Log("TUTORIAL RUN");
                BuildMatch();
                mazeBuilt = true;
                
            } //normal run 
            else if (mazeBuilt == false && tutorial == true)
            {
                //Debug.Log("NORMAL RUN");
                BuildMatch();
                mazeBuilt = true;
            }
            this.GetComponent<Renderer>().enabled = true;
            GameObject[] runes = GameObject.FindGameObjectsWithTag("rune");
            if (runes.Length == 0 && mazeWon == false)
            {
                //Debug.Log("DONE");
                mazeBuilt = false;
                mazeWon = true;
                display = false;
                
                //Debug.Log("Finished rune matching");
                PlayerManager.instance.EndMinigame();
                Destroy(runetut);
                Destroy(this.gameObject);
            }
        }

        
    }

    //builds matching game for player 
    void BuildMatch()
    {
        //scroll dimensions 
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = this.GetComponent<SpriteRenderer>().bounds.size.y;
        var rand = new System.Random();

        int idx = 0;
        int wordlength = rand.Next(3, 6);
        v3 pos = this.transform.position;
        //list pair, names, and list index 
        List<GameObject> pair = new List<GameObject>();
        List<String> names = new List<String>();
        List<int> index = new List<int>();
        for (int i = 0; i < wordlength; i++)
        {
            
            
            //getting unique letters 
            while(index.Contains(idx) == true)
                idx = rand.Next(1, 26);
            //save the index 
            index.Add(idx);
            //get texture for game object
            Texture2D tex = (Texture2D)runetext[idx];
            GameObject gobj = new GameObject();
            //create sprite
            gobj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0));
            gobj.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
            gobj.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gobj.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            gobj.AddComponent<Rune>().answer = idx.ToString();
            gobj.tag = "rune";
            gobj.name = runetext[idx].name;
            names.Add(runetext[idx].name);
            pos.x = -width/5;
            pos.y = (-wordlength +i) + height/4;
            pos.z = -7;
            gobj.transform.position = pos;
            pair.Add(gobj);
            
        }
        // index.Reverse();
        for (int i = 0; i < wordlength; i++)
        {
            int u = index[i];
            int id = rand.Next(0, index.Count);
            int g = index[id];
            index[id] = u;
            index[i] = g;
        }

        for (int i = 0; i < wordlength; i++)
        {
            //int pairlength = rand.Next(0, wordlength);
            int u = index[i];
            //text.text = names[i];
            GameObject obj = new GameObject();
            foreach (var item in lettertext)
            {
                if(item.name == (u+1).ToString())
                {
                    //Debug.Log("FOUND");
                    Texture2D tex = (Texture2D)item;
                    obj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0));
                    obj.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    obj.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    obj.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    obj.AddComponent<Rune>().answer = u.ToString();
                    obj.name = u.ToString();
                    obj.tag = "rune";
                    //Debug.Log(p.transform.position.z);
                    pos.x = width / 7;
                    pos.y = (-wordlength + i) + height / 4;
                    pos.z = -7;
                    obj.transform.position = pos;
                    break;
                }
                
            }
            
            
        }

    }

    public bool SetTutorial
    {
        set
        {
            tutorial = value;
        }
    }
}
