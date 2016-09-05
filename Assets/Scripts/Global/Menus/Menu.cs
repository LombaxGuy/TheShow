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
    /// All Menus are set for not being shown by default try is made for the ingame menu that is not supposed to show up in the mainmenu
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Q is used for now 
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if (!menuMain.activeInHierarchy && !menuSettings.activeInHierarchy)
            {
                menuESC.SetActive(true);
            }
        }
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
