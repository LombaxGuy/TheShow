using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {
    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private GameObject trigger;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject doorTrigger;
    [SerializeField]
    private GameObject wrongWayTrigger;

    public static bool lightEntered = false;
    public static bool firstCleared = false;
    public static bool buttonPressed = false;
    public static bool passedJump = false;
    public static bool buttonRoom = false;
    public static bool wrongWay = false;


    private bool waited = false;
    private int annoyance = 0;

    public static float timer;
    public static float timeInSeconds;


    private float offtime;
    [SerializeField]
    private int stage;
    private int stageSaved;

    //Tallys
    [SerializeField]
    private int jumps;
    private bool hasJumped;
    [SerializeField]
    private int crouches;
    private bool hasCrouched;

    private void OnEnable()
    {

        EventManager.OnPlayerJump += OnPlayerJump;
        EventManager.OnPlayerCrouch += OnPlayerCrouch;


    }

    private void OnDisable()
    {

        EventManager.OnPlayerJump -= OnPlayerJump;
        EventManager.OnPlayerCrouch -= OnPlayerCrouch;

    }


    // Use this for initialization
    void Start () {
        manager = GameObject.Find("WorldManager");
        stage = 0;


    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;


        Intro();
        if (lightEntered && !firstCleared)
        {
            Light();          
        }

        if (firstCleared && !buttonRoom)
        {
            Crouch();
        }

        if (buttonRoom && !buttonPressed)
        {
            Button();
        }

        if (buttonPressed)
        {

        }
    }

    void Intro()
    {
        if (timer > 2 && stage == 0)
        {
            //Welcome
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub1",3);
            StagePrep();
            
        }

        if (timer > 4 && stage == 1)
        {
            //Please step into the light
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub2", 4);
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use Wasd to move around", 3);
            StagePrep();
            trigger.SetActive(true);
        }

        //if (timer > 4 && stage == 2 && hasJumped && !lightEntered)
        //{
        //    // Be in light first    
        //    manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("be in light", 2);
        //    //StagePrep();
        //}

        if (timer > 24 && annoyance == 0)
        {
            //Annoyed 1
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub4AN", 2);
            timer = 0;
            annoyance += 1;
            waited = true;
            
        }

        if (timer > 24 && annoyance == 1)
        {
            //Annoyed 2
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub5AN", 2);
            timer = 0;
            annoyance += 1;
            
        }

        if (timer > 24 && annoyance == 2)
        {
            //Annoyed 3
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub6AN", 2);
            timer = 0;
            annoyance += 1;
            
        }

        if (timer > 24 && annoyance == 3)
        {
            //Annoyed 3
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", 2);
            timer = 0;
            annoyance += 1;
            
        }

        if (timer > 24 && annoyance == 4)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", 2);
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            
        }

        if (timer > 24 && annoyance == 5)
        {
            //Annoyed 4
            manager.GetComponent<SubtitleControl>().StartSub("T0SubAN", 2);
            timer = 0;

        }
    }

    void Light()
    {
        if (timer > 4 && stage == 2 && !waited)
        {
            jumps = 0;
            //normal
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub10", 2);
            StagePrep();
        }

        if(timer > 3 && stage == 2 && waited)
        {
            jumps = 0;
            //Finally
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub9", 2);
            StagePrep();
        }

        if (timer > 3 && jumps >= 1 && stage == 3)
        {
            jumps = 0;
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub11", 2);

            StagePrep();
        }

        if (timer > 3 && jumps >= 1 && stage == 4)
        {
            jumps = 0;
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub12", 2);

            StagePrep();
        }
        if (timer > 3 && jumps >= 1 && stage == 5)
        {
            jumps = 0;
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub13", 2);

            StagePrep();
        }
        if (timer > 3 && jumps >= 1 && stage == 6)
        {
            jumps = 0;
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub14", 2);

            StagePrep();
        }
        if (timer > 3 && jumps >= 3 && stage == 7 && !waited)
        {
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("gotta go fast", 2);
            //gj
            door.GetComponent<DoorBehaviourComponent>().LockDoor(false);
            doorTrigger.SetActive(true);
            StagePrep();
        }

        if (timer > 3 && jumps >= 3 && stage == 7 && waited)
        {
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("ur 2 slow", 2);
            //could have gone faster
            door.GetComponent<DoorBehaviourComponent>().LockDoor(false);
            doorTrigger.SetActive(true);
            StagePrep();
        }

    }

    void Crouch()
    {

        if(timer > 3 && stage == 8)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub16", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 9)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub17", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 10)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub18", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 11)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub19", 2);
            StagePrep();
        }

    }

    void Button()
    {
        if(timer > 3 && stage == 12)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub20", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 13)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub27", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 14)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub28", 2);
            StagePrep();
        }

    }

    void StagePrep()
    {
        stage = stage + 1;
        timer = 0;
    }


    void OnPlayerJump()
    {
        hasJumped = true;
        jumps++;

    }

    void OnPlayerCrouch()
    {
        hasCrouched = true;
        crouches++;
    }
}
