using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour {

    

    public AudioMixer[] mixer;

    /// <summary>
    /// This method is to change one of the AudioMixers. 
    /// </summary>
    /// <param name="value"></param>
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
    /// <summary>
    /// If the value is at -40 or under, it will automaticly change it to -80. Probably need some changes after QA or future testing
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
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
