using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //static instance for singleton
    public static LevelManager instance;

    //Weapon prefab and active weapon
    [SerializeField]
    private GameObject weaponPrefab;
    public GameObject activeWeapon;

    //transforms for weapon movement
    public List<Transform> weaponLocList;

    //number of weapons that need to be completed for this level
    public int weaponsNeeded = 10;

    //number of weapons that have been completed
    public int weaponsCompleted = 0;

    //the amount of time the player has to complete the level and associated time values
    public float levelTime = 1000f;
    private float levelTick = 0;

    //UI elements
    public Text scoreUI;
    public Text quotaUI;
    public Text timerUI;
    public Text endUI;

    private bool levelEnded = false;

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

        //set quota UI based on current quota
        quotaUI.text = "Weapons Needed: " + weaponsNeeded;

        //set up level tick
        levelTick = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTick >= 0)
        {
            GameObject startButton = GameObject.FindGameObjectWithTag("startTutorial");
            //spawn a new weapon if active weapon is null
            if (activeWeapon == null && startButton == null)
            {
                activeWeapon = Instantiate(weaponPrefab, weaponLocList[0]);
                activeWeapon.transform.SetParent(null);
                StartCoroutine(MoveWeapon(activeWeapon, weaponLocList[1]));
            }


            //update UI to show time left in level
            timerUI.text = "Time: " + ((int)levelTick + 1);

            //decrement level time
            levelTick -= Time.deltaTime;
        }
        else
        {
            if(levelEnded == false)
            {
                //TODO: end the level
                EndLevel();
            }
        }
    }

    public void EndLevel()
    {
        //destroy current minigame if there's one active
        if(PlayerManager.instance.currentMinigame != null)
        {
            Destroy(PlayerManager.instance.currentMinigame);
        }

        //show 0 on timer
        timerUI.text = "Time: " + 0;

        //hide main UI and show level end UI
        PlayerManager.instance.ToggleUI(false);
        endUI.enabled = true;
        endUI.text = "Quota: " + weaponsNeeded + "\n\nWeapons Disenchanted: " + weaponsCompleted;
        if(weaponsCompleted >= weaponsNeeded)
        {
            endUI.text += "\n\n Level Complete!";
        }
        else
        {
            endUI.text += "\n\n Game Over";
        }

        //set level end bool
        levelEnded = true;
    }

    public IEnumerator MoveWeapon(GameObject weapon, Transform endLocation)
    {
        //vector from weapon to end location
        Vector2 startVec = weapon.transform.position;
        Vector2 moveVec = endLocation.position - weapon.transform.position;
        moveVec = moveVec.normalized;

        float startTime = Time.time;

        for(int i = 0; weapon.transform.position.x < endLocation.position.x; i++)
        {
            //weapon.transform.position += (Vector3) moveVec * 5 * Time.deltaTime;
            weapon.transform.position = new Vector2(Mathf.SmoothStep(startVec.x, endLocation.position.x, (Time.time - startTime)/1.5f), weapon.transform.position.y);
            yield return null;
        }
    }
}
