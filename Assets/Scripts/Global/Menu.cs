using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class Menu : MonoBehaviour {

    //Used for nothing
    public bool pausedGame;

    //Used by the UI Manager to find the gameobjects that are used.
    private GameObject player;
    private GameObject menu;
    private GameObject settingsMenu;

    /// <summary>
    /// All Menus are set for not being shown by default try is made for the ingame menu that is not supposed to show up in the mainmenu
    /// </summary>
	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu");
        settingsMenu = GameObject.Find("SettingsMenu");
        player = GameObject.Find("Player");
        settingsMenu.SetActive(false);
        try
        { 
            menu.SetActive(false);            
        }
        catch(Exception)
        {
            Debug.Log("Null Exception caused by no found 'Menu' or 'settingsMenu'");
            
        }

        

    }
	
	// Update is called once per frame
        /// <summary>
        /// Q is used for now
        /// </summary>
	void Update () {           
        if(Input.GetKeyUp(KeyCode.Q) && menu.gameObject.activeInHierarchy == false)
        {
            MenuToggle(true, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Q) && menu.gameObject.activeInHierarchy == true)
        {
            MenuToggle(false, 1);
            SettingsToggle(false);
        }
	}

    /// <summary>
    /// MEnu toggle is used by the keyboard button for opening the menu in game
    /// </summary>
    /// <param name="state">Bool to control the toggled state of the menus</param>
    /// <param name="scale">the scale value used for pausing the game, will be obselete when real pause is implimented</param>
    public void MenuToggle(bool state, float scale)
    {
        Time.timeScale = scale;
        player.GetComponent<FirstPersonController>().LockCamera(state);
        pausedGame = state;
        menu.SetActive(state);
    }

    /// <summary>
    /// Used by the main menu settings button to open the settings menu
    /// </summary>
    /// <param name="state">used to change the state</param>
    public void SettingsToggle(bool state)
    {

        settingsMenu.SetActive(state);
    }

    /// <summary>
    /// Used by the resume button in the ingame menu to hide menu and unpause
    /// </summary>
    /// <param name="state">used to change states</param>
    public void Resume(bool state)
    {
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().LockCamera(state);
        pausedGame = state;
        menu.SetActive(state);
    }

    /// <summary>
    /// Used on the Menu screen, new game button to load a scene
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("fp-ctlr");
        
    }

    /// <summary>
    /// USed in the ingame menu when pressing on the main menu button
    /// </summary>
   public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
        

    }

    /// <summary>
    /// used by the menu ingame to toggle the settings
    /// </summary>
    /// <param name="state"></param>
   public void SettingsInGame(bool state)
    {
        settingsMenu.SetActive(state);
    }

    /// <summary>
    /// USed for the back button in settings menu useful when real settings are implimented.
    /// </summary>
    public void SettingsBack(bool state)
    {
        settingsMenu.SetActive(state);
    }

    /// <summary>
    /// USed to exit the game ingame
    /// </summary>
   public void ExitInGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// USed to exit the game in main menu
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }

}
