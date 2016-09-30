using UnityEngine;
using System.Collections;
using System;

public class Level0_Room1 : MonoBehaviour
{
    private GameObject worldManager;

    private SpeakerManager speakerManager;
    private SubtitleControl subtitleManager;
    private Tooltip tooltipManager;

    [SerializeField]
    private AudioClip[] room1Voicelines;

    [SerializeField]
    private Collider lightConeTrigger;

    [SerializeField]
    private float timeBeforeStart = 2;

    private float timer = 0;
    private float currentTime = 0;

    private bool playerEnteredLight = false;

    public float Timer
    {
        get { return timer; }
    }

    public bool PlayerEnteredLight
    {
        set { playerEnteredLight = value; }
    }

    // Use this for initialization
    void Start()
    {
        worldManager = GameObject.Find("WorldManager");

        speakerManager = worldManager.GetComponent<SpeakerManager>();
        subtitleManager = worldManager.GetComponent<SubtitleControl>();
        tooltipManager = worldManager.GetComponent<Tooltip>();

        currentTime = timeBeforeStart;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBeforeStart)
        {
            //Welcome
            float subtitleLength = room1Voicelines[0].length + 1;

            speakerManager.PlaySpeakerSoundOnce(room1Voicelines[0]);
            subtitleManager.StartSub("T0Sub1", "Tutorial_0", subtitleLength);
            
            currentTime += subtitleLength;

        }

        if (timer > currentTime && !playerEnteredLight)
        {
            string tooltip = string.Format("Use {0}, {1}, {2} ,{3} to move around", KeyBindings.KeyMoveForward, KeyBindings.KeyMoveLeft, KeyBindings.KeyMoveBackward, KeyBindings.KeyMoveRight);

            tooltipManager.DisplayTooltipForSeconds(tooltip, 4);
        }
    }
}
