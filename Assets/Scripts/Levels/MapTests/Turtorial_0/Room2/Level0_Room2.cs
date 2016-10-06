using UnityEngine;
using System.Collections;

public class Level0_Room2 : RoomComponent
{
    private bool triggerCrouchLine = false;

    public bool TriggerCrouchLine
    {
        get { return triggerCrouchLine; }
        set { triggerCrouchLine = value; }
    }

    // Use this for initialization
    void Start()
    {
        worldManager = GameObject.Find("WorldManager");

        speakerManager = worldManager.GetComponent<SpeakerManager>();
        subtitleManager = worldManager.GetComponent<SubtitleControl>();
        tooltipManager = worldManager.GetComponent<Tooltip>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        switch (stage)
        {
            // When the player enteres the room.
            case 0:

                if (playerInRoom)
                {
                    stage = 10;
                    timer = 0;
                }

                break;

            case 10:

                if (timer > 4)
                {
                    PlaySoundAndSubtitlesLvl0(roomVoiceLines[0], "Room2Sub1");
                    stage = 15;
                }

                break;

            case 15:

                if (triggerCrouchLine)
                {
                    PlaySoundAndSubtitlesLvl0(roomVoiceLines[1], "Room2Sub2");
                    stage = 20;
                }

                break;

            default:
                break;
        }
    }
}
