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

    // Use this for initialization
    void Start()
    {
        SaveLoad.LoadPrefs();
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

    private void OnEnable()
    {
        GreyOutContinue();
    }

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

    public void Continue()
    {
        saveGame = SaveLoad.Load();

        if (StatTracker.TotalTimeSpend == 0)
        {
            saveGame.GetStatTrackerValues();
        }

        SceneManager.LoadScene(saveGame.currentLevel);

    }

    public void NewGame()
    {
        popupNewGame.SetActive(true);
    }

    public void Settings()
    {
        menuSettings.SetActive(true);
    }

    public void Exit()
    {
        popupExit.SetActive(true);
    }

    public void ConfirmExit()
    {
        popupExit.SetActive(false);
        Application.Quit();
    }

    public void ConfirmNewGame()
    {
        saveGame.DeleteSaveData();
        SceneManager.LoadScene("TestMap");
        popupNewGame.SetActive(false);
    }

    public void Cancel()
    {
        popupExit.SetActive(false);
        popupNewGame.SetActive(false);
    }
}
