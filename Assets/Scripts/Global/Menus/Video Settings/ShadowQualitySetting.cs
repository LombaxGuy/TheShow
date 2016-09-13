using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShadowQualitySetting : MonoBehaviour
{
    private enum Quality { Disabled, Low, Medium, High, VeryHigh };

    [SerializeField]
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
        shadowQualityDD.value = GetCurrentSettingValue();
    }

    private bool CompareShadowSettings()
    {
        bool hasSettingsChanged = false;

        switch (shadowQualityDD.options[shadowQualityDD.value].text)
        {
            case "Disabled":
                if (QualitySettings.shadowDistance == 0 && QualitySettings.shadowResolution == ShadowResolution.Low)
                {
                    hasSettingsChanged = false;
                }
                else
                {
                    hasSettingsChanged = true;
                }
                break;

            case "Low":
                if (QualitySettings.shadowDistance != 0 && QualitySettings.shadowResolution == ShadowResolution.Low)
                {
                    hasSettingsChanged = false;
                }
                else
                {
                    hasSettingsChanged = true;
                }
                break;

            case "Medium":
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.Medium ? (false) : (true);
                break;

            case "High":
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.High ? (false) : (true);
                break;

            case "Very High":
                hasSettingsChanged = QualitySettings.shadowResolution == ShadowResolution.VeryHigh ? (false) : (true);
                break;
        }

        return hasSettingsChanged;
    }

    private int GetCurrentSettingValue()
    {
        return (int)GetCurrentShadowQualitySetting();
    }

    private Quality GetCurrentShadowQualitySetting()
    {
        Quality currentSetting = Quality.Disabled;

        switch (QualitySettings.shadowResolution)
        {
            case ShadowResolution.Low:
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

    private void OnCheckForSettingChanges()
    {
        if (CompareShadowSettings())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnApplySettingChanges()
    {
        // Sets the texture quality according to the name of the option in the dropdown menu.
        switch (shadowQualityDD.options[shadowQualityDD.value].text)
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

    private void OnResetSettings()
    {
        shadowQualityDD.value = GetCurrentSettingValue();
    }

    private void OnResetToDefaultSettings()
    {
        QualitySettings.shadowDistance = 75;
        QualitySettings.shadowResolution = ShadowResolution.Medium;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowProjection = ShadowProjection.StableFit;
        shadowQualityDD.value = 2;
    }

    private void OnSavePref()
    {
        SaveLoad.SaveSettings("shadow", shadowQualityDD.options[shadowQualityDD.value].text);
    }

    private void OnLoadPref()
    {
        string ddSetting = SaveLoad.LoadSettingString("shadow");

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
