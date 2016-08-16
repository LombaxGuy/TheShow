using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer[] mixer;

    /// <summary>
    /// Changes the volume of the master channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMaster(float value)
    {
        mixer[0].SetFloat("masterVol", LinearToDecibel(value));
    }

    /// <summary>
    /// Changes the volume of the music channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMusic(float value)
    {
        mixer[1].SetFloat("musicVol", LinearToDecibel(value));
    }

    /// <summary>
    /// Changes the volume of the effects channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeFX(float value)
    {
        mixer[2].SetFloat("fxVol", LinearToDecibel(value));
    }

    /// <summary>
    /// Changes the volume of the voice channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeVoice(float value)
    {
        mixer[3].SetFloat("voiceVol", LinearToDecibel(value));  
    }

    /// <summary>
    /// Converts a linear value to a decibel value.
    /// </summary>
    /// <param name="linear">The linear value to convert to decible. Supports values from 0 to 1.</param>
    /// <returns></returns>
    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
        {
            dB = 20.0f * Mathf.Log10(linear);
        }
        else
        {
            dB = -144.0f;
        }

        return dB;
    }
}
