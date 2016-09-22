using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadbobSetting : MonoBehaviour
{
    private Slider headbobSlider;

    [SerializeField]
    [Tooltip("The default amount of headbob. Displayed in %.")]
    private float defaultValue = 100;

    private float currentValue;

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
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

    private void Awake()
    {
        // Gets the slider component from the child object
        headbobSlider = GetComponentInChildren<Slider>();
    }

    /// <summary>
    /// Runs when the OnCheckForSettingsChanges event is called.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        // If the current value is not equals to the value of the slider the OnSettingsChanged event is raised.
        if (currentValue != headbobSlider.value)
        {
            EventManager.RaiseOnSettingsChanged();
        }
    }

    /// <summary>
    /// Runs when the OnResetSettings event is called.
    /// </summary>
    private void OnResetSettings()
    {
        // The value of the slider is set to the current value
        headbobSlider.value = currentValue;
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings event is called.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        // The value of the slider is set to the default value and the currentValue is also set to the default value.
        headbobSlider.value = defaultValue;
        currentValue = defaultValue;
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is called.
    /// </summary>
    private void OnApplySettingChanges()
    {
        // The currentValue is set to the value of the slider.
        currentValue = headbobSlider.value;
    }

    /// <summary>
    /// Runs when the OnSavePref event is called.
    /// </summary>
    private void OnSavePref()
    {
        // The vaiable 'currentValue' is saved
        SaveLoad.SaveSettings("Headbob", currentValue);
    }
    /// <summary>
    /// Runs when the OnLoadPref event is called.
    /// </summary>
    private void OnLoadPref()
    {
        // Loads the setting called 'Headbob'
        float savedBob = SaveLoad.LoadSettingFloat("Headbob");

        // If the load is succesful the variables are set
        if(savedBob != -1)
        {
            currentValue = savedBob;
            headbobSlider.value = currentValue;
        }
        // Else the current value is set to the default value
        else
        {
            currentValue = defaultValue;
        }
    }
}
