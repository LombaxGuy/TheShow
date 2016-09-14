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

    [SerializeField]
    private GameObject videoMenu;

    [SerializeField]
    private GameObject gameplayMenu;

    [SerializeField]
    private GameObject keybindingMenu;


    //Float that stores savedata
    private float[] values = new float[4];

    private bool settingsChanged = false;

    /// <summary>
    /// Runs when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        EventManager.OnSettingsChanged += OnSettingsChanged;
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnSettingsChanged -= OnSettingsChanged;
    }

    // Use this for initialization
    void Start()
    {
        EventManager.RaiseOnLoadPref();

        gameObject.SetActive(false);
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


    private void OnSettingsChanged()
    {
        settingsChanged = true;
    }

    private void OpenGameplaySettings()
    {
        
    }

    private void OpenVideoSettings()
    {
        
    }

    private void OpenAudioSettings()
    {
        
    }

    private void OpeKeybindingSettings()
    {
        
    }
}
