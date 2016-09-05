using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour {

    private SaveGame saveGame;
    private bool loadPrefs = true;
    private Canvas cObject;

	// Use this for initialization
	void Start ()
    {
        cObject = GameObject.Find("MasterCanvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
            {
                //continueButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                // continueButton.GetComponent<Button>().interactable = false;
            }

            if (loadPrefs == true)
            {
                SaveLoad.LoadPrefs();
                loadPrefs = false;
            }
        }
    }

    private void Continue()
    {
        saveGame = SaveLoad.Load();
        SceneManager.LoadScene(saveGame.currentLevel);
        loadPrefs = true;
    }

    private void NewGame()
    {
        GameObject.Find("Yes").GetComponent<Button>().enabled = true;
        GameObject.Find("No").GetComponent<Button>().enabled = true;
    }

    private void Settings()
    {

    }

    private void Quit()
    {
        Application.Quit();
    }

    private void ConfirmNewGame()
    {
        saveGame.DeleteSaveData();   
        SceneManager.LoadScene("TestMap");
        loadPrefs = true;
    }

    private void DenyNewGame()
    {
        GameObject.Find("Yes").GetComponent<Button>().enabled = false;
        GameObject.Find("No").GetComponent<Button>().enabled = false;
    }
}
