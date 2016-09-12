using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    //The panel that pops up if changes has been made
    [SerializeField]
    private GameObject popUpApply;
    //The menu where SoundSettings.cs is on
    [SerializeField]
    private GameObject soundMenu;
    //The 4 volume sliders
    [SerializeField]
    private GameObject masterSlider;
    [SerializeField]
    private GameObject musicSlider;
    [SerializeField]
    private GameObject fxSlider;
    [SerializeField]
    private GameObject voiceSlider;

    //Float that stores savedata
    private float[] values = new float[4];

    private bool settingsChanged = false;

    /// <summary>
    /// Runs when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        EventManager.OnSavePref += SaveSoundSliders;
        EventManager.OnLoadPref += LoadSoundSliders;
        EventManager.OnSettingsChanged += OnSettingsChanged;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnSavePref -= SaveSoundSliders;
        EventManager.OnLoadPref -= LoadSoundSliders;
        EventManager.OnSettingsChanged -= OnSettingsChanged;
    }

    // Use this for initialization
    void Start()
    {
        EventManager.RaiseOnLoadPref();

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if(popUpApply.activeInHierarchy)
            {
                Cancel();
            }
            else
            {
                Back();
            }
        }
    }


    /// <summary>
    /// Saves the new slider values
    /// </summary>
    public void Apply()
    {
        EventManager.RaiseOnApplySettingChanges();
        EventManager.RaiseOnSavePref();

        settingsChanged = false;

        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// If there is unfinished changes a popup window appears asking if the user wants to save the changes
    /// </summary>
    public void Back()
    {
        EventManager.RaiseOnCheckForSettingChanges();

        if (settingsChanged)
        {
            popUpApply.SetActive(true);
        }
        //if (soundMenu.GetComponent<SoundSettings>().Changes(values))
        //{
        //       popUpApply.SetActive(true);
        //}
        else
        {
            gameObject.SetActive(false);
        }
    }

    //closes the popUp apply window
    public void Cancel()
    {
        EventManager.RaiseOnResetSettings();

        popUpApply.SetActive(false);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Loads the values of the volumesliders
    /// </summary>
    private void LoadSoundSliders()
    {
        //Sets the values variable to be equal to the array returned in the LoadPrefs method.
        values = SaveLoad.LoadSoundPrefs();

        //Sets the slider values to be equal to the value variable and deactivates the menu
        masterSlider.GetComponent<Slider>().value = values[0];
        musicSlider.GetComponent<Slider>().value = values[1];
        fxSlider.GetComponent<Slider>().value = values[2];
        voiceSlider.GetComponent<Slider>().value = values[3];
    }

    /// <summary>
    /// Saves the values of the volumesliders
    /// </summary>
    private void SaveSoundSliders()
    {
        //Saves our slider values in the value variable
        values[0] = masterSlider.GetComponent<Slider>().value;
        values[1] = musicSlider.GetComponent<Slider>().value;
        values[2] = fxSlider.GetComponent<Slider>().value;
        values[3] = voiceSlider.GetComponent<Slider>().value;

        popUpApply.SetActive(false);

        SaveLoad.SaveSoundPrefs(values[0], values[1], values[2], values[3]);
    }

    private void OnSettingsChanged()
    {
        settingsChanged = true;
    }

}
