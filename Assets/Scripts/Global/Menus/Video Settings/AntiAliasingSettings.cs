using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public enum AAType { Disabled, SSAA, NFAA, FXAA, DLAA };

public class AntiAliasingSettings : MonoBehaviour
{
    private enum AAMultiSampling { x2, x4, x8 };

    [SerializeField]
    private Dropdown aAQualityDD;

    [SerializeField]
    private Dropdown aATypeDD;

    private AAType currentType;
    private AAMultiSampling currentMS;

    [SerializeField]
    private AAType defaultAAType = AAType.Disabled;

    [SerializeField]
    private AAMultiSampling defaultAAMultiSampling = AAMultiSampling.x2;

    public AAType CurrentType
    {
        get { return currentType; }
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

    // Update is called once per frame
    void Update()
    {
        if (aATypeDD.options[aATypeDD.value].text == "Disabled")
        {
            aAQualityDD.interactable = false;
        }
        else
        {
            aAQualityDD.interactable = true;
        }
    }

    private void OnCheckForSettingChanges()
    {
        if (aATypeDD.options[aATypeDD.value].text != currentType.ToString())
        {
            EventManager.RaiseOnSettingsChanged();
        }

        if (aAQualityDD.interactable && aAQualityDD.options[aAQualityDD.value].text != currentMS.ToString())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnApplySettingChanges()
    {
        switch (aATypeDD.options[aATypeDD.value].text)
        {
            case "Disabled":
                currentType = AAType.Disabled;
                QualitySettings.antiAliasing = 0;
                break;

            case "SSAA":
                currentType = AAType.SSAA;
                break;

            case "NFAA":
                currentType = AAType.NFAA;
                break;

            case "FXAA":
                currentType = AAType.FXAA;
                break;

            case "DLAA":
                currentType = AAType.DLAA;
                break;
        }

        switch (aAQualityDD.options[aAQualityDD.value].text)
        {
            case "2x multi-sampling":
                QualitySettings.antiAliasing = 2;
                currentMS = AAMultiSampling.x2;
                break;

            case "4x multi-sampling":
                QualitySettings.antiAliasing = 4;
                currentMS = AAMultiSampling.x4;
                break;

            case "8x multi-sampling":
                QualitySettings.antiAliasing = 8;
                currentMS = AAMultiSampling.x8;
                break;
        }
    }

    private void OnResetSettings()
    {
        switch (currentType)
        {
            case AAType.Disabled:
                aATypeDD.value = 0;
                break;

            case AAType.SSAA:
                aATypeDD.value = 1;
                break;

            case AAType.NFAA:
                aATypeDD.value = 2;
                break;

            case AAType.FXAA:
                aATypeDD.value = 3;
                break;

            case AAType.DLAA:
                aATypeDD.value = 4;
                break;
        }

        switch (currentMS)
        {
            case AAMultiSampling.x2:
                aAQualityDD.value = 0;
                break;

            case AAMultiSampling.x4:
                aAQualityDD.value = 1;
                break;

            case AAMultiSampling.x8:
                aAQualityDD.value = 2;
                break;
        }
    }

    private void OnResetToDefaultSettings()
    {
        switch (defaultAAType)
        {
            case AAType.Disabled:
                aATypeDD.value = 0;
                QualitySettings.antiAliasing = 0;
                break;

            case AAType.SSAA:
                aATypeDD.value = 1;
                currentType = AAType.SSAA;
                break;

            case AAType.NFAA:
                aATypeDD.value = 2;
                currentType = AAType.NFAA;
                break;

            case AAType.FXAA:
                aATypeDD.value = 3;
                currentType = AAType.FXAA;
                break;

            case AAType.DLAA:
                aATypeDD.value = 4;
                currentType = AAType.DLAA;
                break;
        }

        switch (defaultAAMultiSampling)
        {
            case AAMultiSampling.x2:
                aAQualityDD.value = 0;
                QualitySettings.antiAliasing = 2;
                break;

            case AAMultiSampling.x4:
                aAQualityDD.value = 1;
                QualitySettings.antiAliasing = 4;
                break;

            case AAMultiSampling.x8:
                aAQualityDD.value = 2;
                QualitySettings.antiAliasing = 8;
                break;
        }
    }
}
