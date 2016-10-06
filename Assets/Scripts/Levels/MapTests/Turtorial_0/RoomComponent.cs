using UnityEngine;
using System.Collections;

public class RoomComponent : MonoBehaviour
{
    protected GameObject worldManager;

    protected SpeakerManager speakerManager;
    protected SubtitleControl subtitleManager;
    protected Tooltip tooltipManager;

    [SerializeField]
    protected AudioClip[] roomVoiceLines;

    protected float timer = 0;
    protected float timeUntilLineEnds = 0;

    protected int stage = 0;

    protected bool playerInRoom = false;

    public bool PlayerInRoom
    {
        get { return playerInRoom; }
        set { playerInRoom = value; }
    }

    public float Timer
    {
        get { return timer; }
    }

    protected void PlaySoundAndSubtitlesLvl0(AudioClip audioClip, string subtitleName)
    {
        float subtitleLength = audioClip.length + 1;
        speakerManager.PlaySpeakerSoundOnce(audioClip);

        Debug.Log(subtitleLength);
        subtitleManager.StartSub(subtitleName, "Tutorial_0", subtitleLength);

        timeUntilLineEnds = subtitleLength;
        timer = 0;
    }
}
