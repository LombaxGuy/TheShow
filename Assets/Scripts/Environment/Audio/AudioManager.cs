using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    
    [SerializeField]
    private GameObject[] speakers;
    // Use this for initialization
    void Start () {

        //Tag the speakers "Speaker"
        speakers = GameObject.FindGameObjectsWithTag("Speaker");
        if(speakers.Length == 0)
        {
            Debug.Log("No speakers set in scene: AudioManager Script");
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Used to call all the objects tagged with "Speaker"
    /// </summary>
    public void PlayRadioSoundInstance()
    {
        if(speakers.Length > 0)
        {
            for (int i = 0; i < speakers.Length; i++)
            {
                speakers[i].GetComponent<AudioSpeakerScript>().SoundInitiate();
            }
        }
    }

    //Might be good splitting
    public void PlayEffectSoundInstance()
    {
    
    }
}
