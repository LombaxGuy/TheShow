using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSpeakerScript : MonoBehaviour {

    private static AudioSource audioPlayer;
    private static int audioIndex = 0;

    [SerializeField]
    private AudioClip[] chosenClip;

    // Use this for initialization
    /// <summary>
    /// Getting the audiosource component
    /// </summary>
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

    }

    /// <summary>
    /// Called through audiomanager chosenclip is the editor list remember to drag and drop your sounds
    /// </summary>
    public void SoundInitiate()
    {

        if (!audioPlayer.isPlaying)
        {
            if (chosenClip.Length > audioIndex)
            {
                audioPlayer.PlayOneShot(chosenClip[audioIndex]);
                audioIndex++;
            }
        }
    }
}
