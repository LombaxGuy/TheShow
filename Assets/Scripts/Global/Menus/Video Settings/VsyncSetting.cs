using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VsyncSetting : MonoBehaviour
{
    private Toggle vSyncToggle;

    [SerializeField]
    private bool defaultVSyncSetting = false;

    private bool currentSetting;

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

    private void OnCheckForSettingChanges()
    {
        if (vSyncToggle.isOn && QualitySettings.vSyncCount == 0)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnApplySettingChanges()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void OnResetSettings()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }
        
    }

    private void OnResetToDefaultSettings()
    {
        vSyncToggle.isOn = defaultVSyncSetting;
    }
}
