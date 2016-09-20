using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitleSetting : MonoBehaviour
{
    private Toggle subtitleToggle;

    [SerializeField]
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

    private void OnCheckForSettingChanges()
    {
        if (subtitlesEnabled != subtitleToggle.isOn)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnApplySettingChanges()
    {
        subtitlesEnabled = subtitleToggle.isOn;
    }

    private void OnResetSettings()
    {
        subtitleToggle.isOn = subtitlesEnabled;
    }

    private void OnResetToDefaultSettings()
    {
        subtitleToggle.isOn = defaultSubtitleSetting;
        subtitlesEnabled = defaultSubtitleSetting;
    }

    private void OnSavePref()
    {
        // Save subtitlesEnabled
    }

    private void OnLoadPref()
    {
        // Load subtitlesEnabled
    }
}
