using UnityEngine;
using System.Collections;

public class Level0_Room2 : MonoBehaviour
{
    private GameObject worldManager;

    private SpeakerManager speakerManager;
    private SubtitleControl subtitleManager;
    private Tooltip tooltipManager;

    [SerializeField]
    private AudioClip[] room2Voicelines;

    private float timer = 0;
    private float timeUntilLineEnds = 0;
    private bool entered = false;

    private int stage = 0;

    public float Timer
    {
        get { return timer; }
    }

    public bool Entered
    {
        get { return entered; }
        set { entered = value; }
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

                if (entered)
                {
                    stage = 10;
                }

                break;

            case 10:
                PlaySoundAndSubtitlesLvl0(room2Voicelines[0], "T0Sub4");
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
