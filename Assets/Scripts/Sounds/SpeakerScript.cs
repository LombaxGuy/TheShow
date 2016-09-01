using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SpeakerScript : MonoBehaviour {

    //Remember SpeakerTriggerScript and SpeakerManeger
    //Set on speakers

    private static AudioSource audioSource;
    private static int audioIndex = 0;

    // Use this for initialization
    void Start () {
        // Getting the audiosource component
        audioSource = GetComponent<AudioSource>();
	}

    /// <summary>
    /// Called through the SpeakerManager script
    /// </summary>
    /// <param name="ac">Array of audioclips</param>
    public void PlayClips(AudioClip[] ac)  {
        if (!audioSource.isPlaying)
        {
            if (ac.Length > audioIndex)
            {
                Debug.Log(this.ToString());
                audioSource.PlayOneShot(ac[audioIndex]);
                audioIndex++;
            }
        }
    }
}
