using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<PlayerTool> enchantments;

    // Start is called before the first frame update
    void Start()
    {
        int randEnchantment = Random.Range(0, 3);

        //give this weapon a maze enchantment
        enchantments.Add(PlayerTool.Lockpick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicking on Weapon");
        if(PlayerManager.instance.currentTool == enchantments[0])
        {
            PlayerManager.instance.StartMinigame(PlayerManager.instance.currentTool);
        }
    }
}
