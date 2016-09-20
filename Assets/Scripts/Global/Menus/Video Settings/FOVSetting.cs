using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FOVSetting : MonoBehaviour
{
    private Slider fovSlider;

    [SerializeField]
    private float defaultFov = 90;

    private float currentFov;

    public float CurrentFov
    {
        get { return currentFov; }
        set { currentFov = value; }
    }

    private void Awake()
    {
        fovSlider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnSavePref += OnSavePref;
        EventManager.OnLoadPref += OnLoadPref;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        EventManager.OnApplySettingChanges -= OnApplySettingChanges;
        EventManager.OnSavePref -= OnSavePref;
        EventManager.OnLoadPref -= OnLoadPref;
    }

    private void OnCheckForSettingChanges()
    {
        if (currentFov != fovSlider.value)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    private void OnResetSettings()
    {
        fovSlider.value = currentFov;
    }

    private void OnResetToDefaultSettings()
    {
        fovSlider.value = defaultFov;
        currentFov = defaultFov;
    }

    private void OnApplySettingChanges()
    {
        currentFov = fovSlider.value;
    }

    private void OnSavePref()
    {
        SaveLoad.SaveSettings("FOVsetting", currentFov);
        Debug.Log(currentFov);
    }

    private void OnLoadPref()
    {
        float savedFOV = SaveLoad.LoadSettingFloat("FOVsetting");

        Debug.Log(savedFOV);

        if(savedFOV != -1)
        {
            currentFov = savedFOV;
            fovSlider.value = currentFov;
        }
        else
        {
            currentFov = defaultFov;
        }
    }
}
