using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class Menu : MonoBehaviour
{

    //Used by the UI Manager to find the gameobjects that are used.
    private GameObject player;
    private GameObject newGameMenu;
    private GameObject menu;
    private GameObject button;
    private GameObject settingsMenu;
    private SaveGame saveGame;

    //Used to change cursor
    private CursorLockMode cMode;


    /// <summary>
    /// All Menus are set for not being shown by default try is made for the ingame menu that is not supposed to show up in the mainmenu
    /// </summary>
    void Start()
    {
        //Find all the objects using name
        newGameMenu = GameObject.Find("NewGameUI");
        button = GameObject.Find("Continue");
        menu = GameObject.Find("Menu");
        settingsMenu = GameObject.Find("SettingsMenu");
        player = GameObject.Find("Player");

        SaveLoad.LoadPrefs();
        //Turning off all menus
        settingsMenu.SetActive(false);

        try
        {
            menu.SetActive(false);
        }
        catch (Exception)
        {
            Debug.Log("Null Exception caused by no found 'Menu'");
        }

        //Sends a Debug.log if the NewGameUI canvas does not exist
        try
        {
            newGameMenu.SetActive(false);
        }
        catch (Exception)
        {

            Debug.Log("Menu.cs: Null Exception caused by no found 'NewGameUI'");
        }
    }

    /// <summary>
    /// Q is used for now 
    /// </summary>
    void Update()
    {
        //If the Main Menu is the active scene the Continue button will/will not be greyed out depending on if a save file exists
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
            {
                button.GetComponent<Button>().interactable = true;
            }
            else
            {
                button.GetComponent<Button>().interactable = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (menu != null && menu.gameObject.activeInHierarchy == false && settingsMenu.gameObject.activeInHierarchy == false)
            {
                MenuToggle(true);
            }
            else if (menu != null && menu.gameObject.activeInHierarchy == true)
            {
                MenuToggle(false);
            }
            else if (menu != null && menu.gameObject.activeInHierarchy == false && settingsMenu.gameObject.activeInHierarchy == true)
            {
                SettingsToggle(false);
            }
        }
    }

    /// <summary>
    /// MenuToggle is used to show and hide the menu ingame. This also gets the cursor and hides/shows/locks/unlocks it aswell as pause the game with the pause manager.
    /// </summary>
    /// <param name="state">Bool to control the toggled state of the menus</param>
    public void MenuToggle(bool state)
    {
        Pause.SetPauseState(state);
        menu.SetActive(state);
        switch (state)
        {
            case true:
                cMode = CursorLockMode.None;
                break;

            case false:
                cMode = CursorLockMode.Locked;
                SettingsToggle(false);
                break;
        }
        SetMouseState();
    }

    /// <summary>
    /// Used by the main menu settings button to open the settings menu
    /// </summary>
    /// <param name="state">used to change the state</param>
    public void SettingsToggle(bool state)
    {
        if (menu != null)
        {
            menu.SetActive(false);
        }
        settingsMenu.SetActive(state);
    }

    /// <summary>
    /// Used on the Menu screen, new game button will make the NewGameUI active
    /// </summary>
    public void NewGame()
    {
        newGameMenu.SetActive(true);
    }

    /// <summary>
    /// Used on the Menu Screen. 
    /// If pressed the game will load the save file and all values will be set to the saved values.
    /// Loads the last level the player was on.
    /// </summary>
    public void Continue()
    {
        saveGame = SaveLoad.Load();


        if (StatTracker.TotalTimeSpend == 0)
        {
            saveGame.GetStatTrackerValues();
        }

        SceneManager.LoadScene(saveGame.currentLevel);
    }

    /// <summary>
    /// Used in the ingame menu when pressing on the main menu button
    /// </summary>
    public void MainMenu()
    {
        StatTracker.TimeSpendOnAllLevels += StatTracker.TimeSpendOnCurrentLevel;
        Pause.SetPauseState(false);
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Used for the back button in settings menu useful when real settings are implimented.
    /// </summary>
    public void SettingsBack(bool state)
    {
        SaveLoad.SavePrefs();
        if (menu != null)
        {
            menu.SetActive(true);
        }
        settingsMenu.SetActive(state);
    }

    /// <summary>
    /// Used to exit the game ingame
    /// </summary>
    public void ExitInGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Deletes the savedata and loads a level
    /// </summary>
    public void YesButton()
    {
        StatTracker.TotalTimesDead = 0;
        StatTracker.TimesKilledBySpikes = 0;
        StatTracker.TimesKilledBySpinners = 0;
        StatTracker.TimesKilledByFalling = 0;
        StatTracker.TimesKilledByShocks = 0;
        StatTracker.TimesKilledByGas = 0;
        StatTracker.SavedTotalTimeSpend = 0;
        StatTracker.TotalTimeSpend = 0;
        StatTracker.TimeSpendOnAllLevels = 0;
        StatTracker.LevelsCompleted = 0;
        StatTracker.CurrentLevel = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("TestMap");
        SaveLoad.DeleteSaveData();
    }

    /// <summary>
    /// Hides the NewGameUI
    /// </summary>
    public void NoButton()
    {
        newGameMenu.SetActive(false);
    }

    /// <summary>
    /// Used to exit the game in main menu
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }

    /// <summary>
    /// 
    /// </summary>
    void SetMouseState()
    {
        Cursor.lockState = cMode;
        Cursor.visible = (CursorLockMode.Locked != cMode);
    }

}
