using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using v3 = UnityEngine.Vector3;
public class OpenScroll : MonoBehaviour
{
    List<GameObject> runes;
    
    GameObject[] sprites;
    UnityEngine.Object[] runetext;
    UnityEngine.Object[] lettertext;
    // Start is called before the first frame update
    void Start()
    {
        runetext = Resources.LoadAll("runes", typeof(Texture2D));
        lettertext = Resources.LoadAll("Letters", typeof(Texture2D));
        sprites = new GameObject[runetext.Length];
        BuildMatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildMatch()
    {
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = this.GetComponent<SpriteRenderer>().bounds.size.y;
        var rand = new System.Random();
        

        int wordlength = rand.Next(3, 6);
        v3 pos = this.transform.position;
        List<GameObject> pair = new List<GameObject>();
        List<String> names = new List<String>();
        List<int> index = new List<int>();
        for (int i = 0; i < wordlength; i++)
        {
            var idx = rand.Next(1, 26);
            index.Add(idx);
            Texture2D tex = (Texture2D)runetext[idx];
            GameObject gobj = new GameObject();
            
            gobj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0));
            gobj.AddComponent<BoxCollider2D>().size = new Vector2(tex.width, tex.height);
            gobj.AddComponent<Rune>();
            gobj.name = runetext[idx].name;
            names.Add(runetext[idx].name);
            pos.x = -width/5;
            pos.y = (-wordlength +i) + height/4;
            pos.z = -7;
            gobj.transform.position = pos;
            pair.Add(gobj);
        }
        
        
        for (int i = 0; i < wordlength; i++)
        {
            int pairlength = rand.Next(0, wordlength);
            var p = pair[i];
            Texture2D t2 = p.GetComponent<SpriteRenderer>().sprite.texture;
            int u = index[i];
            Texture2D tex = (Texture2D)lettertext[u];

            //text.text = names[i];
            GameObject obj = new GameObject();
            obj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, t2.width, t2.height), new Vector2(0, 0));
            //obj.AddComponent<TextMesh>().text = names[i];
            //Debug.Log(p.transform.position.z);
            pos.x = width / 7;
            pos.y = (-wordlength + i) + height / 4;
            pos.z = -7;
            obj.transform.position = pos;
            
        }


        //Debug.Log(pairlength);
        //Debug.Log(runePos.y);
    }
}
