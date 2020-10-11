using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //static instance for singleton
    public static LevelManager instance;

    //Weapon prefab


    //number of weapons that need to be completed for this level
    private int weaponsNeeded;

    //number of weapons that have been completed
    public int weaponsCompleted = 0;

    //the amount of time the player has to complete the level and associated time values
    private float levelTime;
    private float levelTick = 0;



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
        if(levelTick <= 1)
        {
            //TODO: update UI to show time left in level


            //increment level time
            levelTick += Time.deltaTime / levelTime;
        }
        else
        {
            //TODO: end the level
        }
    }

    
}
