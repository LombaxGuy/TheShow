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
        // Gets the slider form the child object
        fovSlider = GetComponentInChildren<Slider>();
    }

    /// <summary>
    /// Runs when the object is enabled
    /// </summary>
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

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the FOV setting has changed the event OnSettingsChanged is raised.
        if (currentFov != fovSlider.value)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is raised
    /// </summary>
    private void OnResetSettings()
    {
        // Sets the slider value to the current FOV
        fovSlider.value = currentFov;
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is raised
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // Set the slider value and the current FOV to the default value
        fovSlider.value = defaultFov;
        currentFov = defaultFov;
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised
    /// </summary>
    private void OnApplySettingChanges()
    {
        // Sets the currentFov to the slider value
        currentFov = fovSlider.value;
    }

    /// <summary>
    /// Runs when the OnSavePref event is raised
    /// </summary>
    private void OnSavePref()
    {
        // Saves the current FOV
        SaveLoad.SaveSettings("FOVsetting", currentFov);
    }

    /// <summary>
    /// Runs when the OnLoadPref event is raised
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the 'FOVsetting'
        float savedFOV = SaveLoad.LoadSettingFloat("FOVsetting");

        // If the load is succesful the variables are set
        if (savedFOV != -1)
        {
            currentFov = savedFOV;
            fovSlider.value = currentFov;
        }
        // Otherwise the currentFOV is set to the default value
        else
        {
            currentFov = defaultFov;
        }
    }
}
