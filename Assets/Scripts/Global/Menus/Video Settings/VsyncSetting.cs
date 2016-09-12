using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VsyncSetting : MonoBehaviour
{
    private Toggle vSyncToggle;

    [SerializeField]
    private bool defaultVSyncSetting = false;

    /// <summary>
    /// Subscribes to events on awake
    /// </summary>
    private void Awake()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
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
    }

    private void Start()
    {
        vSyncToggle = GetComponent<Toggle>();
    }

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the vSync setting is toggled to 'On' and the current setting is off...
        if (vSyncToggle.isOn && QualitySettings.vSyncCount == 0)
        {
            //... raise the following event.
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised
    /// </summary>
    private void OnApplySettingChanges()
    {
        // If the vSync setting is set to on...
        if (vSyncToggle.isOn)
        {
            //... set the vSyncCount to 1
            QualitySettings.vSyncCount = 1;
        }
        // Otherwise...
        else
        {
            //... set the vSyncCount to 0
            QualitySettings.vSyncCount = 0;
        }
    }

    /// <summary>
    /// Runs when the OnReset event is raised
    /// </summary>
    private void OnResetSettings()
    {
        // If vSync is currently turned off...
        if (QualitySettings.vSyncCount == 0)
        {
            //... set the menu toggle to 'Off'.
            vSyncToggle.isOn = false;
        }
        // Otherwise...
        else
        {
            //... set the menu toggle to 'On'.
            vSyncToggle.isOn = true;
        }
        
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is raised
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Resets the vSync setting to the default setting.
        vSyncToggle.isOn = defaultVSyncSetting;
    }
}
