using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour
{
    private GameObject worldManager;

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

    public bool lightEntered = false;
    public bool inLight = false;
    public bool firstCleared = false;
    public bool buttonPressed = false;
    public bool passedJump = false;
    public bool buttonRoom = false;
    public bool wrongWay = false;


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
    void Start ()
    {
       worldManager = GameObject.Find("WorldManager");

       stage = 0;
       sManager = worldManager.GetComponent<SpeakerManager>();
       subManager = worldManager.GetComponent<SubtitleControl>();
       tooltipManager = worldManager.GetComponent<Tooltip>();

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
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub1", "Tutorial_0", playlist[0].length + 1);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[0]);
            
        }

        if (timer > 10 && stage == 1)
        {
            //Please step into the light
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub2", "Tutorial_0", 4);

            string tooltip = string.Format("Use {0}, {1}, {2} ,{3} to move around", KeyBindings.KeyMoveForward, KeyBindings.KeyMoveLeft, KeyBindings.KeyMoveBackward, KeyBindings.KeyMoveRight);
            worldManager.GetComponent<Tooltip>().DisplayTooltipForSeconds(tooltip, 3);
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
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub4AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            waited = true;
            sManager.PlaySpeakerSoundOnce(playlist[1]);

        }

        if (timer > 24 && annoyance == 1)
        {
            //Annoyed 2
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub5AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[2]);

        }

        if (timer > 24 && annoyance == 2)
        {
            //Annoyed 3
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub6AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[3]);

        }

        if (timer > 24 && annoyance == 3)
        {
            //Annoyed 3
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", "Tutorial_0", 2);
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[4]);

        }

        if (timer > 24 && annoyance == 4)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub7AN", "Tutorial_0", 2);
            //Annoyed 3
            timer = 0;
            annoyance += 1;
            sManager.PlaySpeakerSoundOnce(playlist[5]);

        }

        if (timer > 24 && annoyance == 5)
        {
            //Annoyed 4
            worldManager.GetComponent<SubtitleControl>().StartSub("T0SubAN", "Tutorial_0", 2);
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
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub10", "Tutorial_0", 2);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[7]);
        }

        if(timer > 3 && stage == 2 && waited)
        {
            jumps = 0;
            //Finally
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub9", "Tutorial_0", 2);
            StagePrep();
            sManager.PlaySpeakerSoundOnce(playlist[6]);
        }

        if(inLight)
        {
            if (timer > 3 && jumps >= 1 && stage == 3)
            {
                jumps = 0;
                worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub11", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[8]);
            }

            if (timer > 3 && jumps >= 1 && stage == 4)
            {
                jumps = 0;
                worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub12", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[9]);
            }
            if (timer > 3 && jumps >= 1 && stage == 5)
            {
                jumps = 0;
                worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub13", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[10]);
            }
            if (timer > 3 && jumps >= 1 && stage == 6)
            {
                jumps = 0;
                worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub14", "Tutorial_0", 2);
                StagePrep();
                sManager.PlaySpeakerSoundOnce(playlist[11]);
            }

            if (timer > 3 && jumps >= 3 && stage == 7 && !waited)
            {
                worldManager.GetComponent<Tooltip>().DisplayTooltipForSeconds("gotta go fast", 2);
                //gj
                door.GetComponent<DoorBehaviourComponent>().LockDoor(false);
                doorTrigger.SetActive(true);
                StagePrep();
            }

            if (timer > 3 && jumps >= 3 && stage == 7 && waited)
            {
                worldManager.GetComponent<Tooltip>().DisplayTooltipForSeconds("ur 2 slow", 2);
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
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub16", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 9)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub17", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 10)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub18", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 11)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub19", "Tutorial_0", 2);
            StagePrep();
        }

    }

    void Button()
    {
        if(timer > 3 && stage == 12)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub20", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 13)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub27", "Tutorial_0", 2);
            StagePrep();
        }

        if (timer > 3 && stage == 14)
        {
            worldManager.GetComponent<SubtitleControl>().StartSub("T0Sub28", "Tutorial_0", 2);
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
