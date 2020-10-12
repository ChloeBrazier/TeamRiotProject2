using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //weapon's enchantments
    public List<PlayerTool> enchantments;

    //sprite renderer
    private SpriteRenderer spriteRenderer;
    bool tutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        //save sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();


        //give this weapon randomized enchantments
        int randEnchantment = Random.Range(0, 3);
        if(tutorial == false)
        {
            enchantments.Add(PlayerTool.Lockpick);
            enchantments.Add(PlayerTool.Loupe);
            enchantments.Add(PlayerTool.Eyepiece);
        }
        else
        {
            switch (randEnchantment)
            {
                case 0:
                    enchantments.Add(PlayerTool.Lockpick);
                    spriteRenderer.color = Color.red;
                    break;
                case 1:
                    enchantments.Add(PlayerTool.Loupe);
                    spriteRenderer.color = new Color32(124, 55, 189, 255);
                    break;
                case 2:
                    enchantments.Add(PlayerTool.Eyepiece);
                    spriteRenderer.color = Color.blue;
                    break;
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.currentMinigame == null)
        {
            tutorial = PlayerManager.instance.tutorial;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicking on Weapon");
        //Debug.Log(enchantments[0].ToString());
        if(tutorial == false)
        {
            if (PlayerManager.instance.currentMinigame == null)
            {
                PlayerManager.instance.StartMinigame(PlayerManager.instance.currentTool);
            }
        }
        else
        {
            if (PlayerManager.instance.currentTool == enchantments[0] && PlayerManager.instance.currentMinigame == null)
            {
                PlayerManager.instance.StartMinigame(PlayerManager.instance.currentTool);
            }
        }
    }
}
