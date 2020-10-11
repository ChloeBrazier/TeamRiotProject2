using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using v3 = UnityEngine.Vector3;
public class OpenScroll : MonoBehaviour
{

    public bool display;
    GameObject[] sprites;
    UnityEngine.Object[] runetext;
    UnityEngine.Object[] lettertext;
    public bool mazeWon = false;
    // Start is called before the first frame update
    void Start()
    {
        runetext = Resources.LoadAll("runes", typeof(Texture2D));
        lettertext = Resources.LoadAll("Letters", typeof(Texture2D));
        this.tag = "openscroll";
        //Debug.Log(lettertext[0].name);
        sprites = new GameObject[runetext.Length];
        if(mazeWon == false)
        {
            BuildMatch();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(display == false)
        {
            this.GetComponent<Renderer>().enabled = false;
            

        }
        else
        {
            this.GetComponent<Renderer>().enabled = true;
            GameObject[] runes = GameObject.FindGameObjectsWithTag("rune");
            if (runes.Length == 0 && mazeWon == false)
            {
                //Debug.Log("DONE");
                mazeWon = true;
                display = false;
                Debug.Log("Finished rune matching");
                PlayerManager.instance.EndMinigame();
                Destroy(this.gameObject);
            }
        }

        
    }

    void BuildMatch()
    {
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = this.GetComponent<SpriteRenderer>().bounds.size.y;
        var rand = new System.Random();

        int idx = 0;
        int wordlength = rand.Next(3, 6);
        v3 pos = this.transform.position;
        List<GameObject> pair = new List<GameObject>();
        List<String> names = new List<String>();
        List<int> index = new List<int>();
        for (int i = 0; i < wordlength; i++)
        {
            //select random rune 
            //var idx = rand.Next(1,26);
            

            while(index.Contains(idx) == true)
                idx = rand.Next(1, 26);

            index.Add(idx);
            Texture2D tex = (Texture2D)runetext[idx];
            GameObject gobj = new GameObject();
            
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
            //var p = pair[i];
            //Texture2D t2 = p.GetComponent<SpriteRenderer>().sprite.texture;
            int u = index[i];
           
            

            //text.text = names[i];
            GameObject obj = new GameObject();
            foreach (var item in lettertext)
            {
                if(item.name == (u+1).ToString())
                {
                    //Debug.Log("FOUND");
                   // Debug.Log(u);
                    Texture2D tex = (Texture2D)item;
                    obj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0));
                    obj.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    obj.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    obj.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    obj.AddComponent<Rune>().answer = u.ToString();
                    obj.name = u.ToString();
                    obj.tag = "rune";
                    //obj.AddComponent<TextMesh>().text = names[i];
                    //Debug.Log(p.transform.position.z);
                    pos.x = width / 7;
                    pos.y = (-wordlength + i) + height / 4;
                    pos.z = -7;
                    obj.transform.position = pos;
                    break;
                }
                
            }
            
            
        }


        //Debug.Log(pairlength);
        //Debug.Log(runePos.y);
    }
}
