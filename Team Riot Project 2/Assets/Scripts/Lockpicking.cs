using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpicking : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Lock;
    public GameObject TopOfKeyWay;
    public Camera cam;
    public Rigidbody2D Pick;
    public float difficultyArc = 30;
    public float rightBound = -45;
    public float leftBound = 135;
    public float pickRotationSpeed=5f;
    public float lockRotationSpeed = 5f;

    private Vector3 previousMousePos;
    private float sweetSpotCenter;
    public float sweetSpotLeft;
    public float sweetSpotRight;
    public float movementSpotLeft;
    public float movementSpotRight;
    private bool hitSweetSpot = false;
    public GameObject lockBox;
    private GameObject activeBox;
    GameObject gameUI;
    bool tutorial;

    public List<AudioClip> clips;
    void Start()
    {
        //set cam
        cam = Camera.main;

        //just calculating sweetspot variables
        difficultyArc = 10 * Random.Range(3, 5);
        sweetSpotCenter=Random.Range(rightBound + (difficultyArc/2), leftBound - (difficultyArc / 2));
        sweetSpotLeft = sweetSpotCenter - (difficultyArc / 2);
        sweetSpotRight = sweetSpotCenter + (difficultyArc / 2);
        movementSpotLeft = sweetSpotCenter - difficultyArc ;
        movementSpotRight = sweetSpotCenter + difficultyArc;

        gameUI = GameObject.FindGameObjectWithTag("interface");

        if (lockBox == null)
            lockBox = Resources.Load("mazeBox") as GameObject;

        if (PlayerManager.instance != null)
        {
            tutorial = PlayerManager.instance.tutorial;
        }

        Vector3 textpos = new Vector3(151, 275, 0);
        if (tutorial == false)
        {
            activeBox = Instantiate(lockBox, textpos, Quaternion.identity);
            //runetut.GetComponentInChildren<Text>().text = "TESTING";
            activeBox.transform.parent = gameUI.transform;
            activeBox.AddComponent<TutorialBox>();
            activeBox.GetComponent<TutorialBox>().PushText("Lock Picking Tutorial:\n\n\n" + " \nWith this disenchantment, " +
                "you must unlock the enchantment by lock picking the weapon.");
            activeBox.GetComponent<TutorialBox>().PushText("Lock Picking Tutorial:\n\n\n" + " \nRotate the mouse around the lock to move the lockpick to the correct position.");
            activeBox.GetComponent<TutorialBox>().PushText("Lock Picking Tutorial:\n\n\n" + " \nPress the spacebar to turn the lock to match the position of the lockpick.");
            activeBox.GetComponent<TutorialBox>().PushText("Lock Picking Tutorial:\n\n\n" + " \nThe less the lock jiggles, the closer you will be to disenchanting the weapon.");
            activeBox.GetComponent<TutorialBox>().PushText("Lock Picking Tutorial:\n\n\n" + " \nMake sure to watch and see how far the lock can rotate for where the sweet spot could be.");

            Debug.Log(gameUI);
            //Debug.Log(runetut);
        }
    }
    // Update is called once per frame
    void Update()
    {
            
            if (Input.GetKey(KeyCode.Space))//if turning
            {
                Quaternion rotation=Quaternion.Euler(0,0,0);
                if (hitSweetSpot)//in sweet spot
                {
                    rotation = Quaternion.AngleAxis(90, Vector3.forward);
                    Debug.Log("Turning Lock");
                }
                else if(sweetSpotRight>Pick.rotation)//not in sweet spot to right
                {
                    float angle = 90 / ((sweetSpotRight - rightBound) / (Pick.rotation - rightBound));//calculate percent distance away and create angle
                    rotation = Quaternion.Euler(0,0,angle+ Random.Range(0,5));//random creates jitter when failed
                    Debug.Log("Near Right");
                }
                else if (sweetSpotLeft < Pick.rotation )//not in sweet spot to left
                {
                    float angle = 90 / ((leftBound - sweetSpotLeft) / (leftBound - Pick.rotation));//calculate percent distance away and create angle
                    rotation = Quaternion.Euler(0, 0, angle+Random.Range(0, 5));//random creates jitter when failed
                    Debug.Log("Near Left");
                }
                Lock.transform.rotation = Quaternion.RotateTowards(Lock.transform.rotation, rotation, lockRotationSpeed);
                Pick.transform.position = TopOfKeyWay.transform.position;//pick moves with it
                if (Lock.transform.rotation == Quaternion.AngleAxis(90, Vector3.forward)&&hitSweetSpot)
                {
                    //exit
                    PlayClip(2);
                    PlayerManager.instance.EndMinigame();
                }
            }
            else
            {
            //pick movement
                //calculate angle based on mousepos relative to pick pos.
                Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - Pick.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 40f;

                //Determine Rotation
                Quaternion rotation;//to be determined
                if (Pick.rotation-angle >= leftBound){rotation = Quaternion.AngleAxis(leftBound, Vector3.forward);}//if angle is over leftbound set rotation-axis angle to 135
                else if (Pick.rotation+angle <= rightBound){rotation = Quaternion.AngleAxis(rightBound, Vector3.forward); }//if angle is under rightbound set rotation-axis angle to -35
                else {rotation = Quaternion.AngleAxis(angle, Vector3.forward);}//normal functionality
                Pick.transform.rotation = Quaternion.RotateTowards(Pick.transform.rotation, rotation, pickRotationSpeed);//move pick

                //check if in sweetSpot
                if (Pick.rotation > sweetSpotLeft && Pick.rotation < sweetSpotRight)
                {
                    if(hitSweetSpot == false) PlayClip(0);
                    hitSweetSpot = true;
                    Debug.Log("In Sweet spot");
                }
                else
                {
                    hitSweetSpot = false;
                    Debug.Log("Not In Sweet spot");
                }
                //returns lock back to 0 rotation if not pressing spacebar
                Lock.transform.rotation = Quaternion.RotateTowards(Lock.transform.rotation, Quaternion.AngleAxis(0, Vector3.forward), lockRotationSpeed / 2);
                Pick.transform.position = TopOfKeyWay.transform.position;
            }
    }

    void OnDestroy()
    {
        if(activeBox != null)
        {
            Destroy(activeBox);
        }
    }

    void PlayClip(int index)
    {
        GetComponent<AudioSource>().clip = clips[index];
        GetComponent<AudioSource>().Play();
    }
}
