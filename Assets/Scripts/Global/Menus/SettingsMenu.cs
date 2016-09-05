using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject popUpApply;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            if(popUpApply.activeInHierarchy)
            {
                Cancel();
            }
            else
            {
                Back();
            }
        }
    }

    public void Apply()
    {
        popUpApply.SetActive(false);
        //SaveLoad.SavePrefs();
    }

    public void Back()
    {
        //if (changes)
        //{
               //popUpApply.SetActive(true);

        //}
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        popUpApply.SetActive(false);
    }
}
