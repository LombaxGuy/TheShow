using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public enum AAType { Disabled, SSAA, NFAA, FXAA, DLAA };

public class AntiAliasingSettings : MonoBehaviour
{
    private enum AAMultiSampling { x2, x4, x8 };

    [Header("AA Type")]

    [SerializeField]
    [Tooltip("The DropDown-menu used to specify the AA type.")]
    private Dropdown aATypeDD;

    [SerializeField]
    [Tooltip("The default AA type.")]
    private AAType defaultAAType = AAType.Disabled;

    private AAType currentType;

    public AAType CurrentType
    {
        get { return currentType; }
    }

    [Header("AA Multi-Sampling")]

    [SerializeField]
    [Tooltip("The DropDown-menu used to specify the AA Multi-Sampling.")]
    private Dropdown aAMultiSamplingDD;

    [SerializeField]
    [Tooltip("The default AA multi-sampling.")]
    private AAMultiSampling defaultAAMultiSampling = AAMultiSampling.x2;

    private AAMultiSampling currentMS;

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

    // Update is called once per frame
    void Update()
    {
        // If the AA type is set to disabled the multi-sampling option will not be interactable.
        if (aATypeDD.options[aATypeDD.value].text == "Disabled")
        {
            aAMultiSamplingDD.interactable = false;
        }
        else
        {
            aAMultiSamplingDD.interactable = true;
        }
    }

    /// <summary>
    /// Runs when the OnCheckForSettingsChanges event is called.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the AA type setting has changed the OnSettingsChanged event is raised.
        if (aATypeDD.options[aATypeDD.value].text != currentType.ToString())
        {
            EventManager.RaiseOnSettingsChanged();
        }

        // If the AA multi-sampling setting has changed the OnSettingsChanged event is raised.
        if (aAMultiSamplingDD.interactable && aAMultiSamplingDD.options[aAMultiSamplingDD.value].text != currentMS.ToString())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is called.
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Switches on the current value of the AA type dropdown menu
        switch (aATypeDD.value)
        {
            case 0:
                currentType = AAType.Disabled;
                break;

            case 1:
                currentType = AAType.SSAA;
                break;

            case 2:
                currentType = AAType.NFAA;
                break;

            case 3:
                currentType = AAType.FXAA;
                break;

            case 4:
                currentType = AAType.DLAA;
                break;
        }

        // Switches on the current value of the AA multi-sampling dropdown menu
        switch (aAMultiSamplingDD.value)
        {
            case 0:
                QualitySettings.antiAliasing = 2;
                currentMS = AAMultiSampling.x2;
                break;

            case 1:
                QualitySettings.antiAliasing = 4;
                currentMS = AAMultiSampling.x4;
                break;

            case 2:
                QualitySettings.antiAliasing = 8;
                currentMS = AAMultiSampling.x8;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is called.
    /// </summary>
    private void OnResetSettings()
    {
        // Switches on currentType
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

        // Switches on currentMS
        switch (currentMS)
        {
            case AAMultiSampling.x2:
                aAMultiSamplingDD.value = 0;
                break;

            case AAMultiSampling.x4:
                aAMultiSamplingDD.value = 1;
                break;

            case AAMultiSampling.x8:
                aAMultiSamplingDD.value = 2;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is called.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Switches on defaultAAType
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

        //Switches on defaultAAMultisampling
        switch (defaultAAMultiSampling)
        {
            case AAMultiSampling.x2:
                aAMultiSamplingDD.value = 0;
                QualitySettings.antiAliasing = 2;
                break;

            case AAMultiSampling.x4:
                aAMultiSamplingDD.value = 1;
                QualitySettings.antiAliasing = 4;
                break;

            case AAMultiSampling.x8:
                aAMultiSamplingDD.value = 2;
                QualitySettings.antiAliasing = 8;
                break;
        }
    }

    /// <summary>
    /// Runs when the OnSavePref event is called.
    /// </summary>
    private void OnSavePref()
    {
        // Saves the currentType and the currentMS
        SaveLoad.SaveSettings("aaType", currentType.ToString());
        SaveLoad.SaveSettings("aaMS", currentMS.ToString());
    }

    /// <summary>
    /// Runs when the OnLoadPref event is called.
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the AA type and the AA multi-sampling
        string aaType = SaveLoad.LoadSettingString("aaType");
        string aaMS = SaveLoad.LoadSettingString("aaMS");

        // If the load of aaType is succesful the variables are set
        if (aaType != null)
        {
            switch (aaType)
            {
                case "Disabled":
                    currentType = AAType.Disabled;
                    aATypeDD.value = 0;
                    QualitySettings.antiAliasing = 0;
                    break;

                case "SSAA":
                    currentType = AAType.SSAA;
                    aATypeDD.value = 1;
                    break;

                case "NFAA":
                    currentType = AAType.NFAA;
                    aATypeDD.value = 2;
                    break;

                case "FXAA":
                    currentType = AAType.FXAA;
                    aATypeDD.value = 3;
                    break;

                case "DLAA":
                    currentType = AAType.DLAA;
                    aATypeDD.value = 4;
                    break;
            }
        }

        // If the load of aaMS is succesful the variables are set
        if (aaMS != null)
        {
            switch (aaMS)
            {
                case "x2":
                    QualitySettings.antiAliasing = 2;
                    aAMultiSamplingDD.value = 0;
                    currentMS = AAMultiSampling.x2;
                    break;

                case "x4":
                    QualitySettings.antiAliasing = 4;
                    aAMultiSamplingDD.value = 1;
                    currentMS = AAMultiSampling.x4;
                    break;

                case "x8":
                    QualitySettings.antiAliasing = 8;
                    aAMultiSamplingDD.value = 2;
                    currentMS = AAMultiSampling.x8;
                    break;
            }
        }
    }

}
