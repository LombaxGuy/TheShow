using UnityEngine;
using System.Collections;

public class Level0_Room2 : RoomComponent
{
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
                }

                break;

            case 10:

                //PlaySoundAndSubtitlesLvl0(roomVoiceLines[0], "T0Sub4");
                stage = 15;

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
