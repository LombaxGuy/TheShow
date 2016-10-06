using UnityEngine;
using System.Collections;

public class Level0_Room3 : RoomComponent
{

    private void OnEnable()
    {
        EventManager.OnMusicSaved += OnMusicSaved;
    }

    private void OnDisable()
    {
        EventManager.OnMusicSaved -= OnMusicSaved;
    }

    void Start()
    {
        worldManager = GameObject.Find("WorldManager");

        speakerManager = worldManager.GetComponent<SpeakerManager>();
        subtitleManager = worldManager.GetComponent<SubtitleControl>();
        tooltipManager = worldManager.GetComponent<Tooltip>();
    }

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

                if (timer > 2)
                {
                    PlaySoundAndSubtitlesLvl0(roomVoiceLines[0], "Room3Sub1");
                    stage = 15;
                }

                break;

            default:
                break;
        }
    }

    private void OnMusicSaved()
    {
        PlaySoundAndSubtitlesLvl0(roomVoiceLines[1], "Room3Sub2");
    }
}
