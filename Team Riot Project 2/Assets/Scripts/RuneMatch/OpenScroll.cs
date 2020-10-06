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
    UnityEngine.Object[] textures;
    // Start is called before the first frame update
    void Start()
    {
        textures = Resources.LoadAll("runes", typeof(Texture2D));
        sprites = new GameObject[textures.Length];
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
        v3 scrollPos = this.transform.position;
        Vector2 runepos = new Vector2(scrollPos.x, scrollPos.y);
        runepos.x -= width / 5;
        //runePos.y -= height / 2;
        

        int wordlength = rand.Next(0, 10);
        v3 pos = this.transform.position;
        for (int i = 0; i < wordlength; i++)
        {
            var idx = rand.Next(0, 26);
            Texture2D tex = (Texture2D)textures[idx];
            GameObject gobj = new GameObject();
            gobj.AddComponent<BoxCollider2D>().size = new Vector2(tex.width, tex.height);
            gobj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0));
            
            pos.x = -width/5;
            pos.y = (height / 5) - i;
            pos.z = -7;
            gobj.transform.position = pos;
            
        }



        //Debug.Log(runePos.x);
        //Debug.Log(runePos.y);
    }
}
