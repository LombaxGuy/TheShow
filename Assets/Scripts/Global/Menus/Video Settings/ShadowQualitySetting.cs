using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShadowQualitySetting : MonoBehaviour
{
    private enum Quality { Disabled, Low, Medium, High, VeryHigh };

    [SerializeField]
    [Tooltip("The DropDown-menu used to specify shadow quality.")]
    private Dropdown shadowQualityDD;

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

    private void Start()
    {
        // Sets the value of the dropdown menu to the currently selected value
        shadowQualityDD.value = GetCurrentSettingValue();
    }

    /// <summary>
    /// Compares the current settings and the selected settings and return a bool based on whether or not the settings has changed.
    /// </summary>
    /// <returns>Returns true if the settings has changed.</returns>
    private bool CompareShadowSettings()
    {
        bool hasSettingsChanged = false;

        // Switches on the value of the dropdown menu
        switch (shadowQualityDD.value)
        {
            case 0:
                if (QualitySettings.shadowDistance == 0 && QualitySettings.shadowResolution == ShadowResolution.Low)
                {
                    hasSettingsChanged = false;
                }
                else
                {
                    hasSettingsChanged = true;
                }
                break;

            case 1:
                if (QualitySettings.shadowDistance != 0 && QualitySettings.shadowResolution == ShadowResolution.Low)
                {
                    hasSettingsChanged = false;
                }
                else
                {
                    hasSettingsChanged = true;
                }
                break;

            case 2:
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.Medium ? (false) : (true);
                break;

            case 3:
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.High ? (false) : (true);
                break;

            case 4:
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.VeryHigh ? (false) : (true);
                break;
        }

        return hasSettingsChanged;
    }

    /// <summary>
    /// Returns the value of the current setting in the dropdown menu.
    /// </summary>
    /// <returns>The value of the currnet setting.</returns>
    private int GetCurrentSettingValue()
    {
        return (int)GetCurrentShadowQualitySetting();
    }

    /// <summary>
    /// Returns the current Quality of the shadows.
    /// </summary>
    /// <returns>The curren Quality of the shadows.</returns>
    private Quality GetCurrentShadowQualitySetting()
    {
        Quality currentSetting = Quality.Disabled;

        // Switches on the QualitySettings.shadowResolution
        switch (QualitySettings.shadowResolution)
        {
            case ShadowResolution.Low:
                // If the shadowDistance is 0 and the ShadowResolution is Low.
                if (QualitySettings.shadowDistance == 0)
                {
                    currentSetting = Quality.Disabled;
                }
                else
                {
                    currentSetting = Quality.Low;
                }
                break;

            case ShadowResolution.Medium:
                currentSetting = Quality.Medium;
                break;

            case ShadowResolution.High:
                currentSetting = Quality.High;
                break;

            case ShadowResolution.VeryHigh:
                currentSetting = Quality.VeryHigh;
                break;
        }
        return currentSetting;
    }

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the settings has changed the event OnSettingsChanged is raised.
        if (CompareShadowSettings())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Switches on the value of the dropdown menu and sets the settings accordingly
        switch (shadowQualityDD.value)
        {
            case 0:
                QualitySettings.shadowDistance = 0;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;

            case 1:
                QualitySettings.shadowDistance = 50;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;

            case 2:
                QualitySettings.shadowDistance = 75;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;

            case 3:
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;

            case 4:
                QualitySettings.shadowDistance = 150;
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is raised
    /// </summary>
    private void OnResetSettings()
    {
        // Sets the value of the dropdown menu to the current value
        shadowQualityDD.value = GetCurrentSettingValue();
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is raised
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Sets the settings to their defalult values.
        QualitySettings.shadowDistance = 75;
        QualitySettings.shadowResolution = ShadowResolution.Medium;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowProjection = ShadowProjection.StableFit;
        shadowQualityDD.value = 2;
    }

    /// <summary>
    /// Runs when the OnSavePref event is raised
    /// </summary>
    private void OnSavePref()
    {
        // Saves the current setting
        SaveLoad.SaveSettings("shadow", shadowQualityDD.options[shadowQualityDD.value].text);
    }

    /// <summary>
    /// Runs when the OnLoadPref event is raised
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the shadow setting
        string ddSetting = SaveLoad.LoadSettingString("shadow");

        // If the load was sucssfull the settings is set
        if(ddSetting != null)
        {
            switch(ddSetting)
            {
                case "Disabled":
                    QualitySettings.shadowDistance = 0;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.shadowCascades = 0;
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;
                    break;

                case "Low":
                    QualitySettings.shadowDistance = 50;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.shadowCascades = 0;
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;
                    break;

                case "Medium":
                    QualitySettings.shadowDistance = 75;
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    QualitySettings.shadowCascades = 2;
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;
                    break;

                case "High":
                    QualitySettings.shadowDistance = 100;
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    QualitySettings.shadowCascades = 4;
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;
                    break;

                case "Very High":
                    QualitySettings.shadowDistance = 150;
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    QualitySettings.shadowCascades = 4;
                    QualitySettings.shadowProjection = ShadowProjection.StableFit;
                    break;

            }
        }
    }
}
