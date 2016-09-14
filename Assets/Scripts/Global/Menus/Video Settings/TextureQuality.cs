using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureQuality : MonoBehaviour
{
    private enum TextureQualityLevel { Low, Medium, High};

    [SerializeField]
    private Dropdown textureQualityDD;

    [SerializeField]
    private TextureQualityLevel defaultTextureQuality = TextureQualityLevel.Medium;

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
    /// Compares the name of the setting in the dropdown menu with the currently active setting and returns a bool based on whether the values are the same or not. 
    /// </summary>
    /// <returns>Returns true if the settings has ben changed.</returns>
    private bool CompareTextureQuality()
    {
        bool hasSettingsChanged = false;

        for (int i = 0; i < textureQualityDD.options.Count; i++)
        {
            // Compares the name of the setting in the dropdown menu with the currently active setting.
            switch(textureQualityDD.options[textureQualityDD.value].text)
            {
                case "Low":
                    hasSettingsChanged = QualitySettings.masterTextureLimit == 2 ? (false) : (true);
                    break;

                case "Medium":
                    hasSettingsChanged = QualitySettings.masterTextureLimit == 1 ? (false) : (true);
                    break;

                case "High":
                    hasSettingsChanged = QualitySettings.masterTextureLimit == 0 ? (false) : (true);
                    break;
            }

        }

        return hasSettingsChanged;
    }

    #region Event handlers

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // Raises the event OnSettingsChanged if the setting has changed.
        if (CompareTextureQuality())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised.
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Sets the texture quality according to the name of the option in the dropdown menu.
        switch (textureQualityDD.options[textureQualityDD.value].text)
        {
            case "Low":
                QualitySettings.masterTextureLimit = 2;
                break;

            case "Medium":
                QualitySettings.masterTextureLimit = 1;
                break;

            case "High":
                QualitySettings.masterTextureLimit = 0;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is raised.
    /// </summary>
    private void OnResetSettings()
    {
        // Sets the value of the dropdown menu to match the currently active quality setting.
        switch (QualitySettings.masterTextureLimit)
        {
            case 0:
                textureQualityDD.value = 2;
                break;

            case 1:
                textureQualityDD.value = 1;
                break;

            case 2:
                textureQualityDD.value = 0;
                break;

            default:
                textureQualityDD.value = 0;
                break;
        }
        
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is raised.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Sets the texture quality to its default. Also changes the dropdown menu's value to match the new quality setting.
        switch (defaultTextureQuality)
        {
            case TextureQualityLevel.Low:
                QualitySettings.masterTextureLimit = 2;
                textureQualityDD.value = 0;
                break;

            case TextureQualityLevel.Medium:
                QualitySettings.masterTextureLimit = 1;
                textureQualityDD.value = 1;
                break;

            case TextureQualityLevel.High:
                QualitySettings.masterTextureLimit = 0;
                textureQualityDD.value = 2;
                break;
        }
    }

    private void OnSavePref()
    {
        SaveLoad.SaveSettings("textureQuality", QualitySettings.masterTextureLimit);
        Debug.Log(QualitySettings.masterTextureLimit);
    }

    private void OnLoadPref()
    {
        int textureSetting = SaveLoad.LoadSettingInt("textureQuality");
        Debug.Log(textureSetting);
        if(textureSetting != 0)
        {
            switch(textureSetting)
            {
                case 0:
                    QualitySettings.masterTextureLimit = 0;
                    break;
                case 1:
                    QualitySettings.masterTextureLimit = 1;
                    break;
                case 2:
                    QualitySettings.masterTextureLimit = 2;
                    break;
            }
        }
        Debug.Log(QualitySettings.masterTextureLimit);
    }

    #endregion
}