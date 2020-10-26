using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //weapon's enchantments
    public List<PlayerTool> enchantments;

    //sprite renderer
    private SpriteRenderer spriteRenderer;

    private SpriteRenderer starSpriteRenderer;

    //effects type of weapon spawn
    bool tutorial;

    //camera
    private Camera camera;

    //used as preset color
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        //save sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        starSpriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        //set tutorial value to player manager's tutorial value
        tutorial = PlayerManager.instance.tutorial;

        //quantity of enchantments on weapon
        int randQuantityOfEnchantments = Random.Range(1, 4);

        //tutorial checks. Adding each minigame to Tool list
        if (tutorial == false)
        {
            enchantments.Add(PlayerTool.Lockpick);
            enchantments.Add(PlayerTool.Loupe);
            enchantments.Add(PlayerTool.Eyepiece);
        }
        else //normal case
        {
            for (int i=0; i<randQuantityOfEnchantments; i++)
            {
                //give this weapon randomized enchantments
                AddEnchantment();
            }
            if (randQuantityOfEnchantments>=2) {
                while (enchantments[0] == enchantments[1])
                {
                    enchantments.RemoveAt(1);
                    AddEnchantment();
                }
            }
            if (randQuantityOfEnchantments==3) {
                while (enchantments[2] == enchantments[1] || enchantments[2] == enchantments[0])
                {
                    enchantments.RemoveAt(2);
                    AddEnchantment();
                }
            }
        }
    }

    private void AddEnchantment() 
    {
        int randEnchantment = Random.Range(0, 3);
        switch (randEnchantment)
        {
            case 0:
                enchantments.Add(PlayerTool.Lockpick);
                break;
            case 1:
                enchantments.Add( PlayerTool.Loupe);
                break;
            case 2:
                enchantments.Add(PlayerTool.Eyepiece);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            if (enchantments.Contains(PlayerManager.instance.currentTool) && PlayerManager.instance.currentMinigame == null)
            {
                switch (PlayerManager.instance.currentTool)
                {
                    case PlayerTool.Lockpick:
                        spriteRenderer.color = new Color32(255,0,0,255);
                        break;
                    case PlayerTool.Loupe:
                        spriteRenderer.color = new Color32(124, 55, 189, 255);
                        break;
                    case PlayerTool.Eyepiece:
                        spriteRenderer.color = new Color32(0, 0, 255, 255);
                        break;
                }
            }
        }
        else 
        {
            spriteRenderer.color = spriteRenderer.material.color;
        }
        if (PlayerManager.instance.currentMinigame != null)
        {
            starSpriteRenderer.enabled = false;
        }
        else if (enchantments.Count>0) {
            starSpriteRenderer.enabled = true;
            switch (enchantments[0])
            {
                case PlayerTool.Lockpick:
                    starSpriteRenderer.color = new Color32(255, 0, 0, 255);
                    break;
                case PlayerTool.Loupe:
                    starSpriteRenderer.color = new Color32(124, 55, 189, 255);
                    break;
                case PlayerTool.Eyepiece:
                    starSpriteRenderer.color = new Color32(0, 0, 255, 255);
                    break;
            }
        }
    }

    private void OnMouseDown()
    {

        Debug.Log("Clicking on Weapon");
        //tutorial check
        if(tutorial == false)
        {
            if (PlayerManager.instance.currentMinigame == null && PlayerManager.instance.currentTool != PlayerTool.Unequipped)
            {
                //updating tutorial value from manager
                tutorial = PlayerManager.instance.tutorial;
                PlayerTool minigame = PlayerManager.instance.currentTool;
                PlayerManager.instance.StartMinigame(minigame);
            }
        }
        else //normal case
        {
            if (enchantments.Contains(PlayerManager.instance.currentTool) && PlayerManager.instance.currentMinigame == null)
            {
                enchantments.Remove(PlayerManager.instance.currentTool);
                tutorial = PlayerManager.instance.tutorial;
                PlayerManager.instance.StartMinigame(PlayerManager.instance.currentTool);
            }
        }

        
    }
}
