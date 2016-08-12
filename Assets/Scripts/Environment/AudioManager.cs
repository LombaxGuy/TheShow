using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


    private static AudioSource audioPLayer;
    private AudioClip sip;
    private string audioPath;
    private string audiod;



    ArrayList<>


    public string AudioPath
    {
        get
        {
            return audioPath;
        }
        set
        {
            audioPath = value;
        }
    }
    // Use this for initialization
    void Start () {

        //audioPLayer = GetComponent<AudioSource>();

        sip = Resources.Load<AudioClip>(audioPath);

        Debug.Log(sip);
        audioPLayer.clip = sip;
        PlayChosenClip(sip);

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public static void PlayChosenClip(AudioClip clip)
    {
        audioPLayer.PlayOneShot(clip);
    }

}
