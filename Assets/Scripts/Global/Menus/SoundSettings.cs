using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer[] mixer;

    [SerializeField]
    private float defaultVolume = 1;

    private bool changePersists;
    private float[] currentValues = new float[4];
    private float[] oldValues = new float[4];

    private float[] values = new float[4];

    [SerializeField]
    private GameObject masterSlider;
    [SerializeField]
    private GameObject musicSlider;
    [SerializeField]
    private GameObject fxSlider;
    [SerializeField]
    private GameObject voiceSlider;

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
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnSavePref += SaveSoundSliders;
        EventManager.OnLoadPref += LoadOnStartUp;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        EventManager.OnApplySettingChanges -= OnApplySettingChanges;
        EventManager.OnSavePref -= SaveSoundSliders;
        EventManager.OnLoadPref -= LoadOnStartUp;
    }

    /// <summary>
    /// Changes the volume of the master channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMaster(float value)
    {
        Mixer[0].SetFloat("masterVol", LinearToDecibel(value));
        //EventManager.RaiseOnSettingsChanged();
        currentValues[0] = value;
    }

    /// <summary>
    /// Changes the volume of the music channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeMusic(float value)
    {
        Mixer[1].SetFloat("musicVol", LinearToDecibel(value));
        //EventManager.RaiseOnSettingsChanged();
        currentValues[1] = value;
    }

    /// <summary>
    /// Changes the volume of the effects channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeFX(float value)
    {
        Mixer[2].SetFloat("fxVol", LinearToDecibel(value));
        //EventManager.RaiseOnSettingsChanged();
        currentValues[2] = value;
    }

    /// <summary>
    /// Changes the volume of the voice channel.
    /// </summary>
    /// <param name="value">The value to change the volume to. Value between 0 and 1 where 1 is full volume.</param>
    public void ChangeVolumeVoice(float value)
    {
        Mixer[3].SetFloat("voiceVol", LinearToDecibel(value));
        //EventManager.RaiseOnSettingsChanged();
        currentValues[3] = value;
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
        for (int i = 0; i < oldValues.Length; i++)
        {
            Debug.Log("Old " + i + ": " + oldValues[i]);
            Debug.Log("New " + i + ": " + currentValues[i]);

            if (oldValues[i] != currentValues[i])
            {
                //changePersists = true;
                EventManager.RaiseOnSettingsChanged();
            }
        }
    }

    private void OnResetSettings()
    {
        masterSlider.GetComponent<Slider>().value = oldValues[0];
        musicSlider.GetComponent<Slider>().value = oldValues[1];
        fxSlider.GetComponent<Slider>().value = oldValues[2];
        voiceSlider.GetComponent<Slider>().value = oldValues[3];
    }

    private void OnResetToDefaultSettings()
    {
        masterSlider.GetComponent<Slider>().value = defaultVolume;
        musicSlider.GetComponent<Slider>().value = defaultVolume;
        fxSlider.GetComponent<Slider>().value = defaultVolume;
        voiceSlider.GetComponent<Slider>().value = defaultVolume;
    }

    private void OnApplySettingChanges()
    {
        oldValues = currentValues;
    }

    private void LoadOnStartUp()
    {
        //Sets the values variable to be equal to the array returned in the LoadPrefs method.
        oldValues = SaveLoad.LoadSoundPrefs();
        Debug.Log(oldValues[0]);
        Debug.Log(oldValues[1]);
        Debug.Log(oldValues[2]);
        Debug.Log(oldValues[3]);

        //Sets the slider values to be equal to the value variable and deactivates the menu
        masterSlider.GetComponent<Slider>().value = oldValues[0];
        musicSlider.GetComponent<Slider>().value = oldValues[1];
        fxSlider.GetComponent<Slider>().value = oldValues[2];
        voiceSlider.GetComponent<Slider>().value = oldValues[3];
    }

    /// <summary>
    /// Saves the values of the volumesliders
    /// </summary>
    private void SaveSoundSliders()
    {
        //Saves our slider values in the value variable
        SaveLoad.SaveSoundPrefs(currentValues[0], currentValues[1], currentValues[2], currentValues[3]);
    }
}
