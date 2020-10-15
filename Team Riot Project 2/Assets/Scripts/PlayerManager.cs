using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //list of maze prefabs
    public List<GameObject> mazes;

    //list of UI elements
    public List<GameObject> toolUI;

    //use to track the current minigame being played
    public GameObject currentMinigame;

    public bool tutorial;

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

        tutorial = false;

    }

    // Update is called once per frame
    void Update()
    {
        //unequip current tool if the player right-clicks
        if(Input.GetKeyDown(KeyCode.Mouse1) && currentTool != PlayerTool.Unequipped)
        {
            UnequipTool();
        }

        
    }

    public void StartMinigame(PlayerTool tool)
    {
        //hide tools in UI
        ToggleUI(false);

        

        //start new minigame
        GameObject newMinigame = null;
        
        switch (tool)
        {
            case PlayerTool.Lockpick:
                newMinigame = minigames[1];
                toolCount.Add(1);
                currentMinigame = Instantiate(newMinigame);
                break;
            case PlayerTool.Loupe:
                //randomize which maze is spawned
                int randMaze = Random.Range(0, 3);
                switch(randMaze)
                {
                    case 0:
                        newMinigame = mazes[0];
                        break;
                    case 1:
                        newMinigame = mazes[1];
                        break;
                    case 2:
                        newMinigame = mazes[2];
                        break;
                }
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
        Debug.Log("ENDING MIN GAME");
        tutorial = TutorialCheck(1);
        //start moving weapon offscreen //When tutorial is done 
        if (tutorial == true)
        {
            StartCoroutine(LevelManager.instance.MoveWeapon(LevelManager.instance.activeWeapon, LevelManager.instance.weaponLocList[2]));
        }
        else //normal case 
        {
            StartCoroutine(LevelManager.instance.MoveWeapon(LevelManager.instance.activeWeapon, LevelManager.instance.weaponLocList[1]));
        }
        //increase weapons completed and close current minigame
        //TODO: support weapons with multiple enchantments
        LevelManager.instance.weaponsCompleted++;
        LevelManager.instance.scoreUI.text = "Weapons Completed: " + LevelManager.instance.weaponsCompleted;
        Destroy(currentMinigame);

        //show tools in UI
        ToggleUI(true);
    }

    private bool TutorialCheck(int _num)
    {
        if(tutorial == false)
        {
            //checker for number of unique tools selected
            int amt = 0;
            //Debug.Log(toolCount.Count);
            for (int i = 0; i < toolCount.Count; i++)
            {

                Debug.Log("Tutorial Count SIZE: " + toolCount.Count);
                if (toolCount.Contains(i) || toolCount.Contains(2))//if we have any of the following unique tools by index
                {
                    //we increase the tool amount
                    amt++;
                }
            }
            if (amt == _num) //if we hit the total number of mini games 
            {

                Debug.Log("Tutorial Complete");

                return true;
            }
        }

        return false;
    }

    public void UnequipTool()
    {
        currentTool = PlayerTool.Unequipped;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ToggleUI(bool toggle)
    {
        foreach(GameObject tool in toolUI)
        {
            tool.SetActive(toggle);
        }
    }
}
