using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class Menu : MonoBehaviour {

    //Used by the UI Manager to find the gameobjects that are used.
    private GameObject player;
    private GameObject menu;
    private GameObject settingsMenu;

    //Used to change cursor
    private CursorLockMode cMode;
    

    /// <summary>
    /// All Menus are set for not being shown by default try is made for the ingame menu that is not supposed to show up in the mainmenu
    /// </summary>
	// Use this for initialization
	void Start () {

        //Find all the objects using name
        menu = GameObject.Find("Menu");
        settingsMenu = GameObject.Find("SettingsMenu");
        player = GameObject.Find("Player");

        //Turning off all menus
        settingsMenu.SetActive(false);
        try
        { 
            menu.SetActive(false);            
        }
        catch(Exception)
        {
            Debug.Log("Null Exception caused by no found 'Menu'");
            
        }
    }
	
	// Update is called once per frame
        /// <summary>
        /// Q is used for now, 
        /// </summary>
	void Update () {           
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (menu != null && menu.gameObject.activeInHierarchy == false)
            {
                MenuToggle(true);
            }
            else if (menu != null && menu.gameObject.activeInHierarchy == true)
            {
                MenuToggle(false);
                
            }                       
        }
	}

    /// <summary>
    /// MEnu toggle is used by the keyboard button for opening the menu in game
    /// </summary>
    /// <param name="state">Bool to control the toggled state of the menus</param>
    public void MenuToggle(bool state)
    {        
        player.GetComponent<FirstPersonController>().LockCamera(state);
        menu.SetActive(state);
        switch(state)
        {
            case true:               
                cMode = CursorLockMode.None;
                Time.timeScale = 0;
                break;

            case false:
                cMode = CursorLockMode.Locked;
                Time.timeScale = 1;
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
        settingsMenu.SetActive(state);
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
        SceneManager.LoadScene("Menu");       
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

    void SetMouseState()
    {
        Cursor.lockState = cMode;
        Cursor.visible = (CursorLockMode.Locked != cMode);
    }

}
