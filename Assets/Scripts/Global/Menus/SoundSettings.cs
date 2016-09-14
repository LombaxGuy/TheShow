using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer[] mixer;

    private bool hasChanged;
    private bool changePersists;
    private float[] saveValues = new float[4];
    private float[] values = new float[4];

    public AudioMixer[] Mixer
    {
        get { return mixer; }
        set { mixer = value; }
    }


    private void OnEnable()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        EventManager.OnSavePref += SaveSoundSliders;
        EventManager.OnLoadPref += LoadSoundSliders;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        EventManager.OnSavePref -= SaveSoundSliders;
        EventManager.OnLoadPref -= LoadSoundSliders;
    }
    /// <summary>
    /// Changes the volume of the master channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMaster(float value)
    {
        Mixer[0].SetFloat("masterVol", LinearToDecibel(value));
        hasChanged = true;
        saveValues[0] = value;
    }

    /// <summary>
    /// Changes the volume of the music channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMusic(float value)
    {
        Mixer[1].SetFloat("musicVol", LinearToDecibel(value));
        hasChanged = true;
        saveValues[1] = value;
    }

    /// <summary>
    /// Changes the volume of the effects channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeFX(float value)
    {
        Mixer[2].SetFloat("fxVol", LinearToDecibel(value));
        hasChanged = true;
        saveValues[2] = value;
    }

    /// <summary>
    /// Changes the volume of the voice channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeVoice(float value)
    {
        Mixer[3].SetFloat("voiceVol", LinearToDecibel(value));
        hasChanged = true;
        saveValues[3] = value;
        Debug.Log(saveValues[3]);
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

    public void OnCheckForSettingChanges()
    {
        float[] prefValues = SaveLoad.LoadSoundPrefs();
        changePersists = false;
        if (hasChanged)
        {
            for (int i = 0; i < prefValues.Length; i++)
            {
                Debug.Log(prefValues[i]);
                Debug.Log(saveValues[i]);
                Debug.Log(changePersists);
                if (prefValues[i] != saveValues[i])
                {
                    changePersists = true;
                }

            }
        }
        if (changePersists)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }


    private void OnResetSettings()
    {
        //Sets the values variable to be equal to the array returned in the LoadPrefs method.

        //Sets the slider values to be equal to the value variable and deactivates the menu

        transform.FindChild("MasterSlider").GetComponent<Slider>().value = values[0];
        transform.FindChild("MusicSlider").GetComponent<Slider>().value = values[1];
        transform.FindChild("FXSlider").GetComponent<Slider>().value = values[2];
        transform.FindChild("VoiceSlider").GetComponent<Slider>().value = values[3];
    }

    private void OnResetToDefaultSettings()
    {
        transform.FindChild("MasterSlider").GetComponent<Slider>().value = 1;
        transform.FindChild("MusicSlider").GetComponent<Slider>().value = 1;
        transform.FindChild("FXSlider").GetComponent<Slider>().value = 1;
        transform.FindChild("VoiceSlider").GetComponent<Slider>().value = 1;
    }

    private void LoadSoundSliders()
    {
        //Sets the values variable to be equal to the array returned in the LoadPrefs method.
        saveValues = SaveLoad.LoadSoundPrefs();

        //Sets the slider values to be equal to the value variable and deactivates the menu

        transform.FindChild("MasterSlider").GetComponent<Slider>().value = saveValues[0];
        transform.FindChild("MusicSlider").GetComponent<Slider>().value = saveValues[1];
        transform.FindChild("FXSlider").GetComponent<Slider>().value = saveValues[2];
        transform.FindChild("VoiceSlider").GetComponent<Slider>().value = saveValues[3];
    }

    /// <summary>
    /// Saves the values of the volumesliders
    /// </summary>
    private void SaveSoundSliders()
    {
        //Saves our slider values in the value variable
        saveValues[0] = transform.FindChild("MasterSlider").GetComponent<Slider>().value;
        saveValues[1] = transform.FindChild("MusicSlider").GetComponent<Slider>().value;
        saveValues[2] = transform.FindChild("FXSlider").GetComponent<Slider>().value;
        saveValues[3] = transform.FindChild("VoiceSlider").GetComponent<Slider>().value;


        SaveLoad.SaveSoundPrefs(saveValues[0], saveValues[1], saveValues[2], saveValues[3]);

        values = saveValues;
    }
}
