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

        if (firstCleared && !buttonPressed)
        {
            Crouch();
        }

        if (buttonPressed && !wrongWay)
        {

        }

        if (wrongWay)
        {

        }
    }

    void Intro()
    {
        if (timer > 2 && stage == 0)
        {
            //Welcome
            StagePrep();
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("1", 2);
        }

        if (timer > 4 && stage == 1)
        {
            //Please step into the light
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
            timer = 0;
            annoyance += 1;
            waited = true;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("1 annoyed", 2);
        }

        if (timer > 24 && annoyance == 1)
        {
            //Annoyed 2
            timer = 0;
            annoyance += 1;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("2 annoyed", 2);
        }

        if (timer > 24 && annoyance == 2)
        {
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("3 annoyed", 2);
        }

        if (timer > 24 && annoyance == 3)
        {
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("4 annoyed", 2);
        }

        if (timer > 24 && annoyance == 4)
        {
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("5 annoyed", 2);
        }

        if (timer > 24 && annoyance == 5)
        {
            //Annoyed 4
            timer = 0;
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("6 annoyed", 2);


        }
    }

    void Light()
    {
        if (timer > 4 && stage == 2 && !waited)
        {
            jumps = 0;
            //normal
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("normal", 2);
            StagePrep();
        }

        if(timer > 4 && stage == 2 && waited)
        {
            jumps = 0;
            //Finally
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("finally", 2);
            StagePrep();
        }

        if (timer > 4 && jumps >= 1 && stage == 3)
        {
            jumps = 0;
            //exelent now again

            StagePrep();
        }

        if (timer > 4 && jumps >= 1 && stage == 4)
        {
            jumps = 0;
            //and again

            StagePrep();
        }
        if (timer > 4 && jumps >= 1 && stage == 5)
        {
            jumps = 0;
            //again

            StagePrep();
        }
        if (timer > 4 && jumps >= 3 && stage == 6)
        {
            jumps = 0;
            //3 more

            StagePrep();
        }
        if (timer > 4 && stage == 7 && !waited)
        {
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("gotta go fast", 2);
            //gj
            door.SetActive(false);
            StagePrep();
        }

        if (timer > 4 && stage == 7 && waited)
        {
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("ur 2 slow", 2);
            //could have gone faster
            door.SetActive(false);
            StagePrep();
        }

    }

    void Crouch()
    {

    }

    void Button()
    {

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
