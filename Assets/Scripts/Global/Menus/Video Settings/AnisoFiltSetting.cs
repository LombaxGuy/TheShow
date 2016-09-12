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

    private SettingsMenu settingsMenu;

    private SaveGame saveGame = new SaveGame();

    /// <summary>
    /// Subscribes to events on awake
    /// </summary>
    private void Awake()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        EventManager.OnSavePref += SaveAnisoSettings;
        EventManager.OnLoadPref += LoadAnisoSettings;
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
        EventManager.OnSavePref -= SaveAnisoSettings;
        EventManager.OnLoadPref -= LoadAnisoSettings;
    }

    void Start()
    {
        settingsMenu = GetComponentInParent<SettingsMenu>();
        //currentSetting = defaultAnisotropicFiltering;

        SetToCurrentSetting();
        //Texture.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Texture.anisotropicFiltering = AnisotropicFiltering.Enable;
        //    Texture.SetGlobalAnisotropicFilteringLimits(16, 16);
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Texture.anisotropicFiltering = AnisotropicFiltering.Disable;
        //}
    }

    private void SetToCurrentSetting()
    {
        Debug.Log(currentSetting);
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

    private bool CompareAnisotropicFilteringLevel()
    {
        bool hasSettingsChanged = false;

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

    private void OnCheckForSettingChanges()
    {
        if (CompareAnisotropicFilteringLevel())
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnApplySettingChanges()
    {
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

    private void OnResetSettings()
    {
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

    private void OnResetToDefaultSettings()
    {
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

    private void SaveAnisoSettings()
    {
        SaveLoad.SaveAnisoSetting(currentSetting.ToString());
    }

    private void LoadAnisoSettings()
    {
        string aniso = SaveLoad.LoadAnisoSetting();
        if (aniso != null)
        {
        Debug.Log(aniso);
            switch (aniso)
            {
                case "Disabled":
                    currentSetting = AnisotropicFilteringSettings.Disabled;
                    break;

                case "2x":
                    currentSetting = AnisotropicFilteringSettings.x2;
                    break;

                case "4x":
                    currentSetting = AnisotropicFilteringSettings.x4;
                    break;

                case "8x":
                    currentSetting = AnisotropicFilteringSettings.x8;
                    break;

                case "16x":;
                    currentSetting = AnisotropicFilteringSettings.x16;
                    break;
            }

            Debug.Log(currentSetting);
        }
    }

    #endregion
}
