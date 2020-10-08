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

    private int weaponsCompleted = 0;
    public GameObject currentMinigame;

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

    }

    public void StartMinigame(PlayerTool tool)
    {
        GameObject newMinigame;

        switch (tool)
        {
            case PlayerTool.Lockpick:
                newMinigame = minigames[1];
                currentMinigame = Instantiate(newMinigame);
                break;
            case PlayerTool.Loupe:
                newMinigame = minigames[0];
                currentMinigame = Instantiate(newMinigame);
                break;
            case PlayerTool.Eyepiece:
                newMinigame = minigames[2];
                currentMinigame = Instantiate(newMinigame);
                break;
        }
    }

    public void EndMinigame()
    {
        weaponsCompleted++;
        Destroy(currentMinigame);
    }
}
