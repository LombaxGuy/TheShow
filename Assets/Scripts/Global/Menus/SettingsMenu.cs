using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    //The panel that pops up if changes has been made
    [SerializeField]
    private GameObject popUpApply;

    [SerializeField]
    private GameObject popUpDefault;

    [SerializeField]
    private Canvas gameplayMenu;

    [SerializeField]
    private Canvas videoMenu;

    [SerializeField]
    private Canvas soundMenu;

    [SerializeField]
    private Canvas keybindingMenu;

    private int menuFrontLayer = 90;
    private int menuBackLayer = 80;

    //Float that stores savedata
    private float[] values = new float[4];

    private bool settingsChanged = false;

    /// <summary>
    /// Runs when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        EventManager.OnSettingsChanged += OnSettingsChanged;

        soundMenu.gameObject.SetActive(true);
        videoMenu.gameObject.SetActive(true);
        gameplayMenu.gameObject.SetActive(true);
        keybindingMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Runs when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnSettingsChanged -= OnSettingsChanged;

        soundMenu.gameObject.SetActive(false);
        videoMenu.gameObject.SetActive(false);
        gameplayMenu.gameObject.SetActive(false);
        keybindingMenu.gameObject.SetActive(false);
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
            if (popUpApply.activeInHierarchy || popUpDefault.activeInHierarchy)
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

        popUpApply.SetActive(false);
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

        if (popUpApply.activeInHierarchy)
        {
            popUpApply.SetActive(false);

            gameObject.SetActive(false);
        }
        else if(popUpDefault.activeInHierarchy)
        {
            popUpDefault.SetActive(false);
        }
    }

    public void ResetToDefault()
    {
        popUpDefault.SetActive(true);
    }

    public void DefaultSettings()
    {
        EventManager.RaiseOnResetToDefaultSettings();
        EventManager.RaiseOnApplySettingChanges();
        settingsChanged = false;
        popUpDefault.SetActive(false);
    }

    /// <summary>
    /// Loads the values of the volumesliders
    /// </summary>


    private void OnSettingsChanged()
    {
        settingsChanged = true;
    }

    public void OpenGameplaySettings()
    {
        gameplayMenu.sortingOrder = menuFrontLayer;
        videoMenu.sortingOrder = menuBackLayer;
        soundMenu.sortingOrder = menuBackLayer;
        keybindingMenu.sortingOrder = menuBackLayer;
    }

    public void OpenVideoSettings()
    {
        gameplayMenu.sortingOrder = menuBackLayer;
        videoMenu.sortingOrder = menuFrontLayer;
        soundMenu.sortingOrder = menuBackLayer;
        keybindingMenu.sortingOrder = menuBackLayer;
    }

    public void OpenSoundSettings()
    {
        gameplayMenu.sortingOrder = menuBackLayer;
        videoMenu.sortingOrder = menuBackLayer;
        soundMenu.sortingOrder = menuFrontLayer;
        keybindingMenu.sortingOrder = menuBackLayer;
    }

    public void OpenKeybindingSettings()
    {
        gameplayMenu.sortingOrder = menuBackLayer;
        videoMenu.sortingOrder = menuBackLayer;
        soundMenu.sortingOrder = menuBackLayer;
        keybindingMenu.sortingOrder = menuFrontLayer;
    }
}
