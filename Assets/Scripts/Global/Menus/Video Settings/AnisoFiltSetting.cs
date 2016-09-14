using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnisoFiltSetting : MonoBehaviour
{
    private enum AnisotropicFilteringSettings { Disabled, x2, x4, x8, x16 };

    [SerializeField]
    private Dropdown anisotropicFilteringDD;

    [SerializeField]
    private AnisotropicFilteringSettings defaultAnisotropicFiltering = AnisotropicFilteringSettings.Disabled;

    private AnisotropicFilteringSettings currentSetting;

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

    /// <summary>
    /// Sets the current setting.
    /// </summary>
    void Start()
    {
        SetToCurrentSetting();
    }

    /// <summary>
    /// Sets the dropdown menus option to the current setting. Sets the global AnisotropicFiltering level too.
    /// </summary>
    private void SetToCurrentSetting()
    {
        // Switches on 'currentSetting'
        switch (currentSetting)
        {
            case AnisotropicFilteringSettings.Disabled:
                Texture.anisotropicFiltering = AnisotropicFiltering.Disable;
                anisotropicFilteringDD.value = 0;
                break;

            case AnisotropicFilteringSettings.x2:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(2, 16);
                anisotropicFilteringDD.value = 1;
                break;

            case AnisotropicFilteringSettings.x4:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(4, 16);
                anisotropicFilteringDD.value = 2;
                break;

            case AnisotropicFilteringSettings.x8:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(8, 16);
                anisotropicFilteringDD.value = 3;
                break;

            case AnisotropicFilteringSettings.x16:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(16, 16);
                anisotropicFilteringDD.value = 4;
                break;
        }
    }

    /// <summary>
    /// Compares the current AF level with the selected AF level. If the setting has changed true is returned.
    /// </summary>
    /// <returns>Returns true if the settings has changed.</returns>
    private bool CompareAnisotropicFilteringLevel()
    {
        bool hasSettingsChanged = false;

        // Switches on the selected option's text in the dropdown menu
        switch (anisotropicFilteringDD.options[anisotropicFilteringDD.value].text)
        {
            case "Disabled":
                hasSettingsChanged = currentSetting == AnisotropicFilteringSettings.Disabled ? (false) : (true);
                break;

            case "2x":
                hasSettingsChanged = currentSetting == AnisotropicFilteringSettings.x2 ? (false) : (true);
                break;

            case "4x":
                hasSettingsChanged = currentSetting == AnisotropicFilteringSettings.x4 ? (false) : (true);
                break;

            case "8x":
                hasSettingsChanged = currentSetting == AnisotropicFilteringSettings.x8 ? (false) : (true);
                break;

            case "16x":
                hasSettingsChanged = currentSetting == AnisotropicFilteringSettings.x16 ? (false) : (true);
                break;
        }

        return hasSettingsChanged;
    }

    #region Event handlers

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the settings has been changed...
        if (CompareAnisotropicFilteringLevel())
        {
            //... raise the following event.
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised.
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Switches on the selected option's text and sets the settings accordingly
        switch (anisotropicFilteringDD.options[anisotropicFilteringDD.value].text)
        {
            case "Disabled":
                Texture.anisotropicFiltering = AnisotropicFiltering.Disable;
                currentSetting = AnisotropicFilteringSettings.Disabled;
                break;

            case "2x":
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(2, 16);
                currentSetting = AnisotropicFilteringSettings.x2;
                break;

            case "4x":
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(4, 16);
                currentSetting = AnisotropicFilteringSettings.x4;
                break;

            case "8x":
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(8, 16);
                currentSetting = AnisotropicFilteringSettings.x8;
                break;

            case "16x":
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(16, 16);
                currentSetting = AnisotropicFilteringSettings.x16;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is raised.
    /// </summary>
    private void OnResetSettings()
    {
        // Switches on currentSetting and sets the value of the dropdown menu accordingly
        switch (currentSetting)
        {
            case AnisotropicFilteringSettings.Disabled:
                anisotropicFilteringDD.value = 0;
                break;

            case AnisotropicFilteringSettings.x2:
                anisotropicFilteringDD.value = 1;
                break;

            case AnisotropicFilteringSettings.x4:
                anisotropicFilteringDD.value = 2;
                break;

            case AnisotropicFilteringSettings.x8:
                anisotropicFilteringDD.value = 3;
                break;

            case AnisotropicFilteringSettings.x16:
                anisotropicFilteringDD.value = 4;
                break;
        }

    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is raised.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Switches on defaultAnisotropicFiltering and sets the settings accordingly.
        switch (defaultAnisotropicFiltering)
        {
            case AnisotropicFilteringSettings.Disabled:
                Texture.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;

            case AnisotropicFilteringSettings.x2:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(2, 16);
                break;

            case AnisotropicFilteringSettings.x4:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(4, 16);
                break;

            case AnisotropicFilteringSettings.x8:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(8, 16);
                break;

            case AnisotropicFilteringSettings.x16:
                Texture.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                Texture.SetGlobalAnisotropicFilteringLimits(16, 16);
                break;
        }
    }

    /// <summary>
    /// Runs when the OnSavePref event is raised.
    /// </summary>
    private void OnSavePref()
    {
        // Saves the currentSetting to PlayerPrefs
        SaveLoad.SaveSettings("anisoSettings" ,currentSetting.ToString());
    }

    /// <summary>
    /// Runs when the OnLoadPref event is raised.
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the setting from prefs
        string aFSetting = SaveLoad.LoadSettingString("anisoSettings");

        // If the setting exist in prefs it is set to the loaded value
        if (aFSetting != null)
        {
            switch (aFSetting)
            {
                case "Disabled":
                    currentSetting = AnisotropicFilteringSettings.Disabled;
                    break;

                case "x2":
                    currentSetting = AnisotropicFilteringSettings.x2;
                    break;
                    
                case "x4":
                    currentSetting = AnisotropicFilteringSettings.x4;
                    break;

                case "x8":
                    currentSetting = AnisotropicFilteringSettings.x8;
                    break;

                case "x16":
                    ;
                    currentSetting = AnisotropicFilteringSettings.x16;
                    break;
            }
        }
    }

    #endregion
}
