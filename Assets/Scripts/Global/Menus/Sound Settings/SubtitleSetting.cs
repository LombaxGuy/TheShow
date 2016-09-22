using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitleSetting : MonoBehaviour
{
    private Toggle subtitleToggle;

    [SerializeField]
    [Tooltip("The default setting for subtitles. If true subtitles are enabled by default.")]
    private bool defaultSubtitleSetting = false;

    private bool subtitlesEnabled;

    public bool SubtitlesEnabled
    {
        get { return subtitlesEnabled; }
        set { subtitlesEnabled = value; }
    }

    /// <summary>
    /// Subscribes to events on awake
    /// </summary>
    private void Awake()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        EventManager.OnSavePref += OnSavePref;
        EventManager.OnLoadPref += OnLoadPref;

        // Gets the toggle component
        subtitleToggle = GetComponent<Toggle>();
    }

    /// <summary>
    /// Unsubscribes from events on destroy.
    /// </summary>
    private void OnDestroy()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges -= OnApplySettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        EventManager.OnSavePref -= OnSavePref;
        EventManager.OnLoadPref -= OnLoadPref;
    }

    /// <summary>
    /// Runs when the OnCheckForSettingsChanges event is called.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the current setting is not equals the meun toggle
        if (subtitlesEnabled != subtitleToggle.isOn)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is called.
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Set the current setting to whatever the toggle is set to
        subtitlesEnabled = subtitleToggle.isOn;
    }

    /// <summary>
    /// Runs when the OnResetSettings event is called.
    /// </summary>
    private void OnResetSettings()
    {
        // Set the toggle to whatever the current setting is set to
        subtitleToggle.isOn = subtitlesEnabled;
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is called.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Set the toggle to the default setting and set the current setting to the default setting
        subtitleToggle.isOn = defaultSubtitleSetting;
        subtitlesEnabled = defaultSubtitleSetting;
    }

    /// <summary>
    /// Runs when the OnSavePref event is called.
    /// </summary>
    private void OnSavePref()
    {
        // Saves the variable 'subtitlesEnabled'
        SaveLoad.SaveSettings("Subtitles", subtitlesEnabled);
    }

    /// <summary>
    /// Runs when the OnLoadPref event is called.
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the variable saves as 'Subtitles'
        int savedSubtitle = SaveLoad.LoadSettingInt("Subtitles");

        // If the load is succesful the variables are set
        if (savedSubtitle != -1)
        {
            subtitlesEnabled = Convert.ToBoolean(savedSubtitle);
            subtitleToggle.isOn = Convert.ToBoolean(savedSubtitle);
        }
        // Else the variables are set to the default value
        else
        {
            subtitlesEnabled = defaultSubtitleSetting;
            subtitleToggle.isOn = defaultSubtitleSetting;
        }
    }
}
