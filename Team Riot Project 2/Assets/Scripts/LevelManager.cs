using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int weaponsNeeded = 5;

    //number of weapons that have been completed
    public int weaponsCompleted = 0;

    //the amount of time the player has to complete the level and associated time values
    public float levelTime = 10f;
    private float levelTick = 0;

    //current level
    private int level = 0;

    //UI elements
    public Text scoreUI;
    public Text quotaUI;
    public Text timerUI;
    public Text endUI;

    //Game music variables
    public List<AudioClip> music;
    private AudioSource audio;

    private bool levelEnded = false;
    public GameObject introbox;
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

        //save audio source and play regular level music
        audio = GetComponent<AudioSource>();
        PlayMusic(0);

        //set quota UI based on current quota
        quotaUI.text = "Weapons Needed: " + weaponsNeeded;
        
        //set up level tick
        levelTick = levelTime;
        introbox = GameObject.FindGameObjectWithTag("introBox");
        introbox.AddComponent<TutorialBox>();
        introbox.GetComponent<TutorialBox>().PushText("Hello!\n\n" +
            "welcome to Where the Magic Doesn’t Happen! " +
            "Today is your first day at the disenchantment compound, " +
            "all sorts of illegally enchanted weapons pass through this facility. " +
            "Your job is simple: disenchant the magical items that come your way.");
        introbox.GetComponent<TutorialBox>().PushText("Since its your first day, " +
            "we'll teach you how it all works. We've supplied you with an enchanted Lock pick, a wizard's eye glass, " +
            "and Runic Scroll.");
        introbox.GetComponent<TutorialBox>().PushText("Each weapon's enchantments will only appear" +
            " when selecting the correct tool. Once the enchantment is open, a minigame will appear." +
            "Completeting the minigame before time runs out will successfully disenchant the weapon.");
        introbox.GetComponent<TutorialBox>().PushText("Keep in mind that there is a quota we must meet " +
            "for disenchanting weapons. Make sure to keep up as things can get very fast paced. Oh and be mindful of weapons with multiple enchantments.");
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

            //decrement level time once the tutorial is complete
            if(PlayerManager.instance.tutorial == true)
            {
                //update UI to show time left in level
                timerUI.text = "Time: " + ((int)levelTick);
                levelTick -= Time.deltaTime;
            }
            else
            {
                timerUI.text = "Time: ∞";
            }
        }
        else
        {
            if(levelEnded == false)
            {
                //TODO: end the level
                EndLevel();
            }
            else
            {
                //reload this scene when R is pressed
                if(Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(0);
                }
                if (weaponsCompleted >= weaponsNeeded)
                {
                    //need key bind to start new level here
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        PlayMusic(0);
                        endUI.enabled = false;
                        PlayerManager.instance.ToggleUI(true);
                        Destroy(PlayerManager.instance.currentMinigame);
                        levelTick = levelTime + level * levelTime/2;
                        weaponsCompleted = 0;
                        weaponsNeeded = 5 * level+5;
                        quotaUI.text = "Weapons Needed: " + weaponsNeeded;
                        levelEnded = false;
                    }
                }
            }
        }

        //quit game if escape key is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
            level++;
            endUI.text += "\n\n Level"+level +" Complete!";
            PlayMusic(1);
        }
        else
        {
            endUI.text += "\n\n Game Over";
            PlayMusic(2);
        }

        endUI.text += "\n\n Press R to restart from beginning";
        if (weaponsCompleted >= weaponsNeeded)
        {
            endUI.text += "\n\n Press Enter to go to next level";
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

    public void PlayMusic(int index)
    {
        if(index >= music.Count)
        {
            Debug.Log("Exceeded MUSIC LIST COUNT");
            return;
        }
        else
        {
            audio.clip = music[index];
            audio.Play();
        }
        
    }
}
