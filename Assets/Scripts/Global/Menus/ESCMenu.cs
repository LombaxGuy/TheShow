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
    // Use this for initialization
    void Start()
    {

    }

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
    }

    private void OnEnable()
    {
        headsUpDisplay.SetActive(false);
    }

    public void Resume()
    {
        headsUpDisplay.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Settings()
    {
        menuSettings.SetActive(true);
    }

    public void Exit()
    {
        popupExit.SetActive(true);
    }

    public void ConfirmExitMainMenu()
    {
        StatTracker.TimeSpendOnAllLevels += StatTracker.TimeSpendOnCurrentLevel;
        popupExit.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void ConfirmExitDesktop()
    {
        popupExit.SetActive(false);
        Application.Quit();
    }

    public void Cancel()
    {
        popupExit.SetActive(false);
    }
}
