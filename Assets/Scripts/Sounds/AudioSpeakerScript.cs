using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSpeakerScript : MonoBehaviour
{
    private static AudioSource audioPlayer;
    private static int audioIndex = 0;

    [SerializeField]
    // Editor list. Remember to drag and drop your sounds
    private AudioClip[] chosenClip;

    // Use this for initialization
    void Start()
    {
        // Getting the audiosource component
        audioPlayer = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called through the AudioManager script
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
