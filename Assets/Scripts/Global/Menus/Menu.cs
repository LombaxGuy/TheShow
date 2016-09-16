using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class Menu : MonoBehaviour
{

    //Used by the UI Manager to find the gameobjects that are used.
    [SerializeField]
    private GameObject menuESC;
    [SerializeField]
    private GameObject menuSettings;
    [SerializeField]
    private GameObject menuMain;

    //Used to change cursor
    private CursorLockMode cMode;


    /// <summary>
    /// Q is used for now 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if (!menuMain.activeInHierarchy && !menuSettings.activeInHierarchy && SceneManager.GetActiveScene().name != "Menu")
            {
                menuESC.SetActive(true);

                CursorManager.UnlockCursor();
                Pause.SetPauseState(true);
            }
        }
    }
}
