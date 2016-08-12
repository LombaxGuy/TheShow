using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour {


    public AudioMixer[] mixer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeVolumeMaster(float value)
    {
        mixer[0].SetFloat("MasterVol", ValueFixer(value));
    }

    public void ChangeVolumeMusic(float value)
    {
        mixer[1].SetFloat("musicVol", ValueFixer(value));

    }

    public void ChangeVolumeFX(float value)
    {
        mixer[2].SetFloat("FxVol", ValueFixer(value));
    }

    public void ChangeVolumeVoice(float value)
    {
        mixer[3].SetFloat("VoiceVol", ValueFixer(value));  
    }

    private float ValueFixer(float value)
    {

        if (value <= -40)
        {
            value = -80;
        }
        Debug.Log(value);
        return value;
    }
}
