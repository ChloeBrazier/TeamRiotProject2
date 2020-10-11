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
    private float levelTime = 60f;
    private float levelTick = 0;

    //UI elements
    public Text scoreUI;
    public Text quotaUI;
    public Text timerUI;

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
            //spawn a new weapon if active weapon is null
            if(activeWeapon == null)
            {
                activeWeapon = Instantiate(weaponPrefab, weaponLocList[0]);
                activeWeapon.transform.SetParent(null);
                StartCoroutine(MoveWeapon(activeWeapon, weaponLocList[1]));
            }


            //TODO: update UI to show time left in level
            timerUI.text = "Time: " + ((int)levelTick + 1);

            //decrement level time
            levelTick -= Time.deltaTime;
        }
        else
        {
            //TODO: end the level
            Debug.Log("Clock Over");
        }
    }

    public IEnumerator MoveWeapon(GameObject weapon, Transform endLocation)
    {
        //vector from weapon to end location
        Vector3 moveVec = endLocation.position - weapon.transform.position;
        moveVec = moveVec.normalized;

        for(int i = 0; weapon.transform.position.x <= endLocation.position.x; i++)
        {
            weapon.transform.position += moveVec * 5 * Time.deltaTime;
            yield return null;
        }
    }
}
