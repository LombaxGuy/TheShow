using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    [SerializeField]
    private int minimumResolutionWidth = 800;

    [SerializeField]
    private int minimumResolutionHeight = 600;

    [SerializeField]
    private int defaultResolutionWidth = 800;

    [SerializeField]
    private int defaultResoltionHeight = 600;

    [SerializeField]
    private Dropdown resolutionDD;

    [SerializeField]
    private Toggle windowedToggle;

    private SettingsMenu settingsMenu;

    private bool windowedChanged = false;
    private bool resolutionChanged = false;

    /// <summary>
    /// Subscribes to events on awake
    /// </summary>
    private void Awake()
    {
        EventManager.OnCheckForSettingChanges += OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges += OnApplySettingChanges;
        EventManager.OnResetSettings += OnResetSettings;
        EventManager.OnResetToDefaultSettings += OnResetToDefaultSettings;
        //EventManager.OnSavePref += OnSavePref;
        //EventManager.OnLoadPref += OnLoadPref;

    }

    /// <summary>
    /// Unsubscribes from event on destroy
    /// </summary>
    private void OnDestroy()
    {
        EventManager.OnCheckForSettingChanges -= OnCheckForSettingChanges;
        EventManager.OnApplySettingChanges -= OnApplySettingChanges;
        EventManager.OnResetSettings -= OnResetSettings;
        EventManager.OnResetToDefaultSettings -= OnResetToDefaultSettings;
        //EventManager.OnSavePref -= OnSavePref;
        //EventManager.OnLoadPref -= OnLoadPref;
    }

    /// <summary>
    /// Sets the values of the resolution and the windowed checkbox on awake
    /// </summary>
    private void Start()
    {
        settingsMenu = GetComponentInParent<SettingsMenu>();
#if (!DEBUG)
        // Sets the abailable resolutions based on what the monitor supports.
        SetAvailableResolutions();
#endif
        // Gets the current resolution of the game window.
        int[] currentResolution = new int[] { Screen.width, Screen.height, Screen.currentResolution.refreshRate };

        // Finds the current resolution and set the value of the dropdown menu accordingly 
        resolutionDD.value = FindCurrentResolutionValue();
        
        // If the game is running in windowed mode the windowedToggle.isOn is set to true otherwise it is set to false
        windowedToggle.isOn = !Screen.fullScreen ? (true) : (false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    EventManager.RaiseOnCheckForSettingChanges();
        //    EventManager.RaiseOnApplySettingChanges();
        //}
    }

    /// <summary>
    /// Sets the available resolutions based on what the current monitor supports.
    /// </summary>
    private void SetAvailableResolutions()
    {
        resolutionDD.ClearOptions();

        List<string> resolutions = new List<string>();
        Resolution[] screenResolitions = Screen.resolutions;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (!(screenResolitions[i].width < minimumResolutionWidth) && !(screenResolitions[i].height < minimumResolutionHeight))
            {
                resolutions.Add(screenResolitions[i].width + "x" + screenResolitions[i].height + " " + screenResolitions[i].refreshRate + "Hz");
            }
        }

        resolutionDD.AddOptions(resolutions);

        resolutionDD.RefreshShownValue();
    }

    /// <summary>
    /// Runs when the OnCheckForSettingChanges event is raised.
    /// </summary>
    private void OnCheckForSettingChanges()
    {
        int[] values = GetSelectedResolution();

        if (Screen.width != values[0] || 
            Screen.height != values[1] || 
            Screen.currentResolution.refreshRate != values[2])
        {
            // Settings has been changed.
            EventManager.RaiseOnSettingsChanged();
            //settingsMenu.SettingsChanged = true;
        }

        if (Screen.fullScreen == windowedToggle.isOn)
        {
            // Settings has been changed.
            EventManager.RaiseOnSettingsChanged();
            //settingsMenu.SettingsChanged = true;
        }
    }

    /// <summary>
    /// Runs when the OnApplySettingChanges event is raised.
    /// </summary>
    private void OnApplySettingChanges()
    {
        int[] values = GetSelectedResolution();

        Screen.SetResolution(values[0], values[1], !windowedToggle.isOn, values[2]);
    }

    /// <summary>
    /// Runs when the OnResetSettings event is raised.
    /// </summary>
    private void OnResetSettings()
    {
        resolutionDD.value = FindCurrentResolutionValue();
    }

    /// <summary>
    /// Runs when the OnResetToDefaultSettings is raised.
    /// </summary>
    private void OnResetToDefaultSettings()
    {
        Screen.SetResolution(defaultResolutionWidth, defaultResoltionHeight, true);
        resolutionDD.value = FindCurrentResolutionValue();
        windowedToggle.isOn = false;
    }
    
    /// <summary>
    /// Returns an array containing the width, height and refresh rate of the selected resolution. (In that order.)
    /// </summary>
    /// <returns>An array containing a width, height and refresh rate.</returns>
    private int[] GetSelectedResolution()
    {
        int[] selectedValues = new int[3];
        int selectedIndex = resolutionDD.value;
        string resText = resolutionDD.options[selectedIndex].text;
        resText = resText.Replace('x', ' ');
        resText = resText.Replace("Hz", "");

        string[] stringArray = resText.Split(' ');

        int.TryParse(stringArray[0], out selectedValues[0]);
        int.TryParse(stringArray[1], out selectedValues[1]);
        int.TryParse(stringArray[2], out selectedValues[2]);

        return selectedValues;
    }

    /// <summary>
    /// Gets the resolution displayed on the specified value.
    /// </summary>
    /// <param name="value">The value where the resolution is located.</param>
    /// <returns>Returns an array containing the width, height and refresh rate of the resolution. (In that order)</returns>
    private int[] GetResolutionOnValue(int value)
    {
        int[] resolutionOnValue = new int[3];

        // The text that is displayed
        string resText = resolutionDD.options[value].text;

        // Replaces the 'x' with ' ' and the 'Hz' with nothing.
        resText = resText.Replace('x', ' ');
        resText = resText.Replace("Hz", "");

        // Splits the resText at the ' '
        string[] stringArray = resText.Split(' ');

        // Converts the 3 values to integers
        int.TryParse(stringArray[0], out resolutionOnValue[0]);
        int.TryParse(stringArray[1], out resolutionOnValue[1]);
        int.TryParse(stringArray[2], out resolutionOnValue[2]);

        return resolutionOnValue;
    }

    /// <summary>
    /// Returns the dropdown value of the current resolution setting.
    /// </summary>
    /// <returns>The value of the current resolution setting. If the current resolution cannot be found 0 is returned.</returns>
    private int FindCurrentResolutionValue()
    {
        int value = 0;

        for (int i = 0; i < resolutionDD.options.Count; i++)
        {
            if (GetResolutionOnValue(i)[0] == Screen.width &&
                GetResolutionOnValue(i)[1] == Screen.height &&
                GetResolutionOnValue(i)[2] == Screen.currentResolution.refreshRate)
            {
                value = i;
                break;
            }
        }

        return value;
    }
}
