using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //weapon's enchantments
    public List<PlayerTool> enchantments;

    //sprite renderer
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //save sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();


        //give this weapon randomized enchantments
        int randEnchantment = Random.Range(0, 3);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicking on Weapon");
        if(PlayerManager.instance.currentTool == enchantments[0] && PlayerManager.instance.currentMinigame == null)
        {
            PlayerManager.instance.StartMinigame(PlayerManager.instance.currentTool);
        }
    }
}
