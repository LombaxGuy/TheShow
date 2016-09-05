using UnityEngine;
using System.Collections;

public class SpeakerManeger : MonoBehaviour
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
    /// <param name="acs">Array of audioclips</param>
    public void PlaySpeakerSoundInstance(AudioClip[] acs)
    {
        StartCoroutine(SendNext(acs));
    }

    IEnumerator SendNext(AudioClip[] acs)
    {
        while (acs.Length > audioIndex)
        {
            if (speakers.Length > 0)
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    //if (!speakers[i].GetComponent<AudioSource>().isPlaying)
                    //{
                        speakers[i].GetComponent<AudioSource>().PlayOneShot(acs[audioIndex]); 
                   // }
                }
            }
            yield return new WaitForSeconds(acs[audioIndex].length);
            audioIndex++;
        }
    }
}
