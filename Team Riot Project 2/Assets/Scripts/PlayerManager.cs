using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTool
{ 
    Lockpick,
    Loupe,
    Eyepiece,
    Unequipped
}


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerTool currentTool = PlayerTool.Unequipped;

    //list of prefabs for minigames
    public List<GameObject> minigames;

    //list of UI elements
    public List<GameObject> toolUI;

    //use to track the current minigame being played
    public GameObject currentMinigame;

    public bool tutorial = false;

    List<int> toolCount = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //unequip current tool if the player right-clicks
        if(Input.GetKeyDown(KeyCode.Mouse1) && currentTool != PlayerTool.Unequipped)
        {
            UnequipTool();
        }

        int amt = 0;
        for(int i = 0; i < toolCount.Count; i++)
        {
            if(toolCount[i] == i)
            {
                amt++;
            }
        }
        if(amt == 3)
        {
            Debug.Log("Tutorial Complete");
            tutorial = true;
        }
    }

    public void StartMinigame(PlayerTool tool)
    {
        //hide tools in UI
        ToggleUI();

        //start new minigame
        GameObject newMinigame;
        
        switch (tool)
        {
            case PlayerTool.Lockpick:
                newMinigame = minigames[1];
                toolCount.Add(1);
                currentMinigame = Instantiate(newMinigame);
                break;
            case PlayerTool.Loupe:
                newMinigame = minigames[0];
                toolCount.Add(0);
                currentMinigame = Instantiate(newMinigame);
                break;
            case PlayerTool.Eyepiece:
                newMinigame = minigames[2];
                toolCount.Add(2);
                currentMinigame = Instantiate(newMinigame);
                break;
        }

        //set current tool to unequipped
        UnequipTool();
    }

    public void EndMinigame()
    {
        //start moving weapon offscreen
        StartCoroutine(LevelManager.instance.MoveWeapon(LevelManager.instance.activeWeapon, LevelManager.instance.weaponLocList[2]));

        //increase weapons completed and close current minigame
        //TODO: support weapons with multiple enchantments
        LevelManager.instance.weaponsCompleted++;
        LevelManager.instance.scoreUI.text = "Weapons Completed: " + LevelManager.instance.weaponsCompleted;
        Destroy(currentMinigame);

        //show tools in UI
        ToggleUI();
    }

    public void UnequipTool()
    {
        currentTool = PlayerTool.Unequipped;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ToggleUI()
    {
        foreach(GameObject tool in toolUI)
        {
            tool.SetActive(!tool.activeSelf);
        }
    }
}
