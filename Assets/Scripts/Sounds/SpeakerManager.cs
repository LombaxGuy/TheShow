using UnityEngine;
using System.Collections;

public class SpeakerManager : MonoBehaviour
{
    //Remember SpeakerTriggerScript and SpeakerScript
    //Set on gameobject

    private GameObject[] speakers;
    private static int audioIndex = 0;

    IEnumerator playTotalClips;

    private bool playTotalClipsRunning = false;

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

    private void StopSound()
    {
        if (speakers.Length > 0)
        {
            for (int i = 0; i < speakers.Length; i++)
            {
                AudioSource currentSpeaker = speakers[i].GetComponent<AudioSource>();
                if(currentSpeaker.isPlaying)
                {
                   currentSpeaker.Stop();
                    Debug.Log("stopped sound");
                }
            }
        }
    }

    public void PlaySpeakerSoundOnce(AudioClip audioClip)
    {
        StopSound();
        if (speakers.Length > 0)
        {
            for (int i = 0; i < speakers.Length; i++)
            {

                speakers[i].GetComponent<AudioSource>().PlayOneShot(audioClip);
                Debug.Log("Activated speaker" + i);
            }
        }
    }

    /// <summary>
    /// Used to call all the objects tagged with "Speaker"
    /// </summary>
    /// <param name="audioClips">Array of audioclips</param>
    public void PlaySpeakerSoundArray(AudioClip[] audioClips)
    {
        StopSound();
        if (playTotalClipsRunning == true)
        {
            StopCoroutine(playTotalClips);
        }
        playTotalClips = CoroutinePlayArray(audioClips);
        StartCoroutine(playTotalClips);
    }

    IEnumerator CoroutinePlayArray(AudioClip[] audioClips)
    {
        playTotalClipsRunning = true;
        while (audioClips.Length > audioIndex)
        {
            if (speakers.Length > 0)
            {
                for (int i = 0; i < speakers.Length; i++)
                {

                    speakers[i].GetComponent<AudioSource>().PlayOneShot(audioClips[audioIndex]);

                    Debug.Log("coroutien sound" + i);
                }
            }
            yield return new WaitForSeconds(audioClips[audioIndex].length);
            audioIndex++;

        }
        playTotalClipsRunning = false;
    }


}
