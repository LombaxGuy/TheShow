using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour
{

    private SaveGame saveGame;

    [SerializeField]
    private GameObject popupNewGame;
    [SerializeField]
    private GameObject popupExit;
    [SerializeField]
    private GameObject menuSettings;
    [SerializeField]
    private GameObject continueButton;
    /// <summary>
    /// Called when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        EventManager.OnLoadGame += LoadGame;
        GreyOutContinue();
    }

    /// <summary>
    /// Called when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnLoadGame -= LoadGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if (popupNewGame.activeInHierarchy || popupExit.activeInHierarchy)
            {
                Cancel();
            }
            else if (!menuSettings.activeInHierarchy)
            {
                Exit();
            }
        }
    }


    /// <summary>
    /// Greys out the continue button
    /// </summary>
    private void GreyOutContinue()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            continueButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = false;
        }

    }

    /// <summary>
    /// Loads the users savegame
    /// </summary>
    public void Continue()
    {
        EventManager.RaiseOnLoadGame();
        Pause.SetPauseState(false);
    }

    /// <summary>
    /// Enables the newgame window
    /// </summary>
    public void NewGame()
    {
        popupNewGame.SetActive(true);
    }

    /// <summary>
    /// Enables the settings window
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
    /// Exits the game
    /// </summary>
    public void ConfirmExit()
    {
        popupExit.SetActive(false);
        Application.Quit();
    }

    /// <summary>
    /// Starts a new game and deletes old savedata
    /// </summary>
    public void ConfirmNewGame()
    {
        saveGame = new SaveGame();
        saveGame.DeleteSaveData();
        popupNewGame.SetActive(false);
        gameObject.SetActive(false);
        Pause.SetPauseState(false);
        SceneManager.LoadScene("TestMap");
    }

    /// <summary>
    /// Closes the exit and newgame windows
    /// </summary>
    public void Cancel()
    {
        popupExit.SetActive(false);
        popupNewGame.SetActive(false);
    }

    /// <summary>
    /// Loads the savedata
    /// </summary>
    private void LoadGame()
    {
        saveGame = SaveLoad.Load();

        if (StatTracker.TotalTimeSpend == 0)
        {
            saveGame.GetStatTrackerValues();
        }

        SceneManager.LoadScene(saveGame.currentLevel);

        gameObject.SetActive(false);
    }
}
