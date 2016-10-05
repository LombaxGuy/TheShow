using UnityEngine;
using System.Collections;

public class AudioSequencePlayingScript : MonoBehaviour
{
    private AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("AudioClips")]
    private AudioClip[] audioClips;

    [SerializeField]
    [Tooltip("Should it loop the last AudioClip?")]
    private bool loopLast;

    private float timer;

    private float currentAudioClipLength;

    private int audioIndex = 0;

    private bool endOfArray = false;

    [SerializeField]
    private bool test = false;

    // Use this for initialization
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        if (audioClips.Length > 0)
        {
            PlayClip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioClips.Length > 0 && !endOfArray)
        {
            timer += Time.deltaTime;
            //- 0.1f for no break in between audioclips
            if (timer > currentAudioClipLength - 0.1f)
            {
                if (audioIndex + 1 == audioClips.Length)
                {
                    if (loopLast)
                    {
                        audioPlayer.loop = true;
                        return;
                    }
                    else
                    {
                        audioPlayer.Stop();

                        endOfArray = true;

                        return;
                    }
                }
                else
                {
                    audioIndex++;
                }
                PlayClip();
            }
        }
    }
    /// <summary>
    /// Plays the current audioclip
    /// </summary>
    void PlayClip()
    {
        audioPlayer.Stop();

        audioPlayer.clip = audioClips[audioIndex];

        currentAudioClipLength = audioClips[audioIndex].length;

        timer = 0;

        audioPlayer.Play();

    }
}
