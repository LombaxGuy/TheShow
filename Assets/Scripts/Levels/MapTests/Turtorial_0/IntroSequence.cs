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

    public static bool lightEntered = false;
    public static bool firstCleared = false;


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

        manager.GetComponent<SubtitleControl>().StartSub("sub1", 3);
        stage = 1;

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;



        if (timer > 4 && stage == 1)
        {
            manager.GetComponent<SubtitleControl>().StartSub("sub2",3);
            stage = stage + 1;
        }

        if (timer > 8 && stage == 2)
        {
            manager.GetComponent<SubtitleControl>().StartSub("sub3", 3);
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use Wasd to move around", 3);
            stage = stage + 1;
            stageSaved = stage;
            trigger.SetActive(true);
        }

        if(timer > 26 && stage == 3 && !lightEntered)
        {
            manager.GetComponent<SubtitleControl>().StartSub("Test1", 3);
            stage = stage + 1;
            stageSaved = stage;
        }

        if (timer > 40 && stage == 4 && !lightEntered)
        {
            manager.GetComponent<SubtitleControl>().StartSub("Test2", 3);
            stage = stage + 1;
            stageSaved = stage;
        }

        //After light
        if (lightEntered)
        {
            if (!hasJumped && jumps < 3 && stage == stageSaved)
            {
                manager.GetComponent<SubtitleControl>().StartSub("sub4", 3);
                manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use space to jump", 3);
                StagePrep();
            }
            else if (hasJumped && jumps < 3 && stage == stageSaved)
            {
                manager.GetComponent<SubtitleControl>().StartSub("JumpComplete2", 3);
                StagePrep();
            }
            else if(hasJumped && jumps >= 3 && stage == stageSaved)
            {
                manager.GetComponent<SubtitleControl>().StartSub("JumpComplete1", 3);
                StagePrep();
            }



            if (timer > 4 && hasJumped && jumps >= 3 && !hasCrouched && stage == stageSaved + 1)
            {
                manager.GetComponent<SubtitleControl>().StartSub("sub5", 3);
                manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use control to crouch", 3);
                StagePrep();
            }
            else if(timer > 4 && hasJumped && jumps >= 3 && hasCrouched && stage == stageSaved + 1)
            {
                manager.GetComponent<SubtitleControl>().StartSub("CrouchComplete2", 3);
                StagePrep();

            }

            if (timer > 4 && hasCrouched && stage == stageSaved + 2)
            {
                manager.GetComponent<SubtitleControl>().StartSub("sub6", 3);
                StagePrep();
                doorTrigger.SetActive(true);
                door.SetActive(false);
                stageSaved = stage;
            }

            if (timer > 12 && !firstCleared && stage == stageSaved + 1)
            {
                manager.GetComponent<SubtitleControl>().StartSub("triggered", 3);
                StagePrep();
            }

            if (timer > 12 && !firstCleared && stage == stageSaved + 1)
            {
                manager.GetComponent<SubtitleControl>().StartSub("triggered", 3);
                StagePrep();
            }

            if (timer > 12 && firstCleared && stage == stageSaved + 2)
            {
                manager.GetComponent<SubtitleControl>().StartSub("triggered", 3);
                StagePrep();
            }

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
