using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ESCMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject popupExit;
    [SerializeField]
    private GameObject menuSettings;
    [SerializeField]
    private GameObject headsUpDisplay;
    [SerializeField]
    private GameObject menuMain;

    private bool closeThisShit = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if (popupExit.activeInHierarchy)
            {
                Cancel();
            }
            else if (!menuSettings.activeInHierarchy)
            {
                Resume();
            }
        }
        if (gameObject.activeInHierarchy && closeThisShit)
        {
            closeThisShit = false;
        }
    }

    /// <summary>
    /// When the object is enabled this will run
    /// </summary>
    private void OnEnable()
    {
        headsUpDisplay.SetActive(false);
        CursorManager.UnlockCursor();
        Pause.SetPauseState(true);
    }

    /// <summary>
    /// Unpauses the game and closes the menu
    /// </summary>
    public void Resume()
    {
        headsUpDisplay.SetActive(true);
        gameObject.SetActive(false);
        CursorManager.LockCursor();
        Pause.SetPauseState(false);
    }

    /// <summary>
    /// Enables the SettingsMenu
    /// </summary>
    public void Settings()
    {
        menuSettings.SetActive(true);
    }

    /// <summary>
    /// Enables the exit window
    /// </summary>
    public void Exit()
    {
        popupExit.SetActive(true);
    }

    /// <summary>
    /// Exits to the main menu
    /// </summary>
    public void ConfirmExitMainMenu()
    {
        Debug.Log(gameObject.activeInHierarchy);
        StatTracker.TimeSpendOnAllLevels += StatTracker.TimeSpendOnCurrentLevel;
        popupExit.SetActive(false);
        gameObject.SetActive(false);
        Debug.Log(gameObject.activeInHierarchy);
        menuMain.SetActive(true);
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ConfirmExitDesktop()
    {
        popupExit.SetActive(false);
        Application.Quit();
    }

    /// <summary>
    /// Disables the exit window
    /// </summary>
    public void Cancel()
    {
        popupExit.SetActive(false);
    }
}
