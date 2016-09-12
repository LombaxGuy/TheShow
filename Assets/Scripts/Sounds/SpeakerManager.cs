using UnityEngine;
using System.Collections;

public class SpeakerManager : MonoBehaviour
{
    //Remember SpeakerTriggerScript and SpeakerScript
    //Set on gameobject

    private GameObject[] speakers;
    private static int audioIndex = 0;

    // Use this for initialization
    void Start()
    {
        //Tag the speakers "Speaker"
        speakers = GameObject.FindGameObjectsWithTag("Speaker");

        if (speakers.Length == 0)
        {
            Debug.Log("No speakers set in scene: SpeakerManager Script");
        }
    }

    /// <summary>
    /// Used to call all the objects tagged with "Speaker"
    /// </summary>
    /// <param name="audioClips">Array of audioclips</param>
    public void PlaySpeakerSoundInstance(AudioClip[] audioClips)
    {
        StartCoroutine(SendNext(audioClips));
    }

    IEnumerator SendNext(AudioClip[] audioClips)
    {
        while (audioClips.Length > audioIndex)
        {
            if (speakers.Length > 0)
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    //if (!speakers[i].GetComponent<AudioSource>().isPlaying)
                    //{
                    speakers[i].GetComponent<AudioSource>().PlayOneShot(audioClips[audioIndex]);
                    // }
                }
            }
            yield return new WaitForSeconds(audioClips[audioIndex].length);
            audioIndex++;
        }
    }
}
