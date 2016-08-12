using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Audio;


//[RequireComponent(typeof(AudioSource))]
public class HostAudioScript : MonoBehaviour {


    public AudioMixer masterMixer;


	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {

	
	}
    
    public void SetVolume(float dinMor)
    {
        masterMixer.SetFloat("musicVol", dinMor);
    }

 
}
