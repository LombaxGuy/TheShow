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

    [SerializeField]
    private AudioClip[] playlist;

    public static bool lightEntered = false;
    public static bool inLight = false;
    public static bool firstCleared = false;
    public static bool buttonPressed = false;
    public static bool passedJump = false;
    public static bool buttonRoom = false;
    public static bool wrongWay = false;


    private bool waited = false;
    private int annoyance = 0;

    public static float timer;



    private float offtime;
    [SerializeField]
    private int stage;
    private int stageSaved;

    private SpeakerManager sManager;
    private SubtitleControl subManager;
    private Tooltip tooltipManager;

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
       sManager = manager.GetComponent<SpeakerManager>();
       subManager = manager.GetComponent<SubtitleControl>();
       tooltipManager = manager.GetComponent<Tooltip>();

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
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub1", "Tutorial_0", 3);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[0]);
            
        }

        if (timer > 10 && stage == 1)
        {
            //Please step into the light
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub2", "Tutorial_0", 4);
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
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub4AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            waited = true;
            sManager.PlaySpeakerSoundOnce(playlist[1]);

        }

        if (timer > 24 && annoyance == 1)
        {
            //Annoyed 2
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub5AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[2]);

        }

        if (timer > 24 && annoyance == 2)
        {
            //Annoyed 3
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub6AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[3]);

        }

        if (timer > 24 && annoyance == 3)
        {
            //Annoyed 3
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[4]);

        }

        if (timer > 24 && annoyance == 4)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", "Tutorial_0", 2);
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[5]);

        }

        if (timer > 24 && annoyance == 5)
        {
            //Annoyed 4
            manager.GetComponent<SubtitleControl>().StartSub("T0SubAN", "Tutorial_0", 2);
            timer = 0;
            sManager.PlaySpeakerSoundOnce(playlist[5]);

        }
    }

    void Light()
    {
        if (timer > 4 && stage == 2 && !waited)
        {
            jumps = 0;
            //normal
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub10", "Tutorial_0", 2);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[7]);
        }

        if(timer > 3 && stage == 2 && waited)
        {
            jumps = 0;
            //Finally
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub9", "Tutorial_0", 2);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[6]);
        }

        if(inLight)
        {
            if (timer > 3 && jumps >= 1 && stage == 3)
            {
                jumps = 0;
                manager.GetComponent<SubtitleControl>().StartSub("T0Sub11", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[8]);
            }

            if (timer > 3 && jumps >= 1 && stage == 4)
            {
                jumps = 0;
                manager.GetComponent<SubtitleControl>().StartSub("T0Sub12", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[9]);
            }
            if (timer > 3 && jumps >= 1 && stage == 5)
            {
                jumps = 0;
                manager.GetComponent<SubtitleControl>().StartSub("T0Sub13", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[10]);
            }
            if (timer > 3 && jumps >= 1 && stage == 6)
            {
                jumps = 0;
                manager.GetComponent<SubtitleControl>().StartSub("T0Sub14", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[11]);
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
    }

    void Crouch()
    {

        if(timer > 3 && stage == 8)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub16", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 9)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub17", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 10)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub18", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 11)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub19", "Tutorial_0", 2);
            StagePrep();
        }

    }

    void Button()
    {
        if(timer > 3 && stage == 12)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub20", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 13)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub27", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 14)
        {
            manager.GetComponent<SubtitleControl>().StartSub("T0Sub28", "Tutorial_0", 2);
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
        if(inLight)
        jumps++;

    }

    void OnPlayerCrouch()
    {
        hasCrouched = true;
        crouches++;
    }
}
