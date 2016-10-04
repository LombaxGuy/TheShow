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
    private float timeBeforeStart = 2;

    private float timer = 0;
    private float timeUntilLineEnds = 0;

    private bool playerEnteredLight = false;

    private int stage = 0;


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

        timeUntilLineEnds = timeBeforeStart;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current stage: " + stage);
        timer += Time.deltaTime;

        switch (stage)
        {
                // On game start. Welcome...
            case 0:

                if (timer > timeBeforeStart)
                {
                    PlaySoundAndSubtitlesLvl0(room1Voicelines[0], "T0Sub1");
                    stage = 10;
                }

                break;

                // Player should enter the light.
            case 10:

                if (playerEnteredLight)
                {
                    stage = 15;
                }

                if (timer > timeUntilLineEnds && !playerEnteredLight)
                {
                    string tooltip = string.Format("Use {0}, {1}, {2} ,{3} to move around", KeyBindings.KeyMoveForward, KeyBindings.KeyMoveLeft, KeyBindings.KeyMoveBackward, KeyBindings.KeyMoveRight);

                    tooltipManager.DisplayTooltipForSeconds(tooltip, 4);

                    PlaySoundAndSubtitlesLvl0(room1Voicelines[1], "T0Sub2");

                    stage = 11;
                }
                break;

                // Waiting for the player to enter the light.
            case 11:

                if (playerEnteredLight)
                {
                    stage = 15;
                }

                break;

                // When player enters light.
            case 15:

                PlaySoundAndSubtitlesLvl0(room1Voicelines[2], "T0Sub3");

                stage = 20;

                break;

            default:
                break;
        }
    }

    private void PlaySoundAndSubtitlesLvl0(AudioClip audioClip, string subtitleName)
    {
        float subtitleLength = audioClip.length + 1;
        speakerManager.PlaySpeakerSoundOnce(audioClip);

        Debug.Log(subtitleLength);
        subtitleManager.StartSub(subtitleName, "Tutorial_0", subtitleLength);

        timeUntilLineEnds = subtitleLength;
        timer = 0;
    }
}
