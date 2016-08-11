using UnityEngine;
using System.Collections;
using System.IO;


[RequireComponent(typeof(AudioSource))]
public class HostAudioScript : MonoBehaviour {

    private static AudioSource audioPLayer;
    private AudioClip sip;

    private string audiod;
    

	// Use this for initialization
	void Start () {
        audioPLayer = GetComponent<AudioSource>();
        
        sip = (AudioClip)Resources.Load("Assets/Resources/TV_Theme160");
        sip = Resources.Load<AudioClip>("Assets/Resources/TV_Theme160");
        sip = Resources.Load("Resources/TV_Theme160") as AudioClip;
        Debug.Log(sip);
        audioPLayer.clip = sip;
        //PlayChosenClip(sip);
        audioPLayer.PlayOneShot(sip);
    }
	
	// Update is called once per frame
	void Update () {

	
	}

    public static void PlayChosenClip(AudioClip clip)
    {
              audioPLayer.PlayOneShot(clip);
    }
}
