using UnityEngine;
using System.Collections;
using System;

public class Level0_Room1 : RoomComponent
{
    [SerializeField]
    private float timeBeforeStart = 2;

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
                    PlaySoundAndSubtitlesLvl0(roomVoiceLines[0], "Room1Sub1");
                    stage = 10;
                }

                break;

                // Player should enter the light.
            case 10:

                if (timer > timeUntilLineEnds)
                {
                    PlaySoundAndSubtitlesLvl0(roomVoiceLines[1], "Room1Sub2");

                    stage = 11;
                    timer = 0;
                }

                break;

                // Waiting for the player to leave the room. After 5 seconds a tooltip will be displayed showing how to move.
            case 11:

                if (timer > timeUntilLineEnds + 5 && playerInRoom)
                {
                    string tooltip = string.Format("Use {0}, {1}, {2} ,{3} to move around", KeyBindings.KeyMoveForward, KeyBindings.KeyMoveLeft, KeyBindings.KeyMoveBackward, KeyBindings.KeyMoveRight);

                    tooltipManager.DisplayTooltipForSeconds(tooltip, 4);

                    stage = 15;
                }

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
