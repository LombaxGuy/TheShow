using UnityEngine;
using System.Collections;

public class SpeakerManeger : MonoBehaviour {

    //Remember SpeakerTriggerScript and SpeakerScript
    //Set on gameobject

    private GameObject[] speakers;

    // Use this for initialization
    void Start() {
        //Tag the speakers "Speaker"
        speakers = GameObject.FindGameObjectsWithTag("Speaker");
        if (speakers.Length == 0)
        {
            Debug.Log("No speakers set in scene: AudioManager Script");
        }
    }

    /// <summary>
    /// Used to call all the objects tagged with "Speaker"
    /// </summary>
    /// <param name="ac">Array of audioclips</param>
    public void PlayRadioSoundInstance(AudioClip[] ac) {

        Debug.Log(speakers.Length + " speakers");

        if (speakers.Length > 0)
        {
            for (int i = 0; i < speakers.Length; i++)
            {
                speakers[i].GetComponent<SpeakerScript>().PlayClips(ac);
                Debug.Log(i + "Speakers done");
            }
        }
    }
}
