using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {


    //This is a save/load system. Using Unity PlayerPrefs to create or get data from a place on the local computer.
    //Settings not done
    //Host mind not done


    private bool[] completedLevels;
    
    public bool[] CompletedLevels
    { get{return completedLevels; }set {//Triggered
            completedLevels = value;
        }
    }

    private string[] settings;

    public string[] Settings
    {
        get { return settings; }
        set { settings = value; }
    }



    
    

	// Use this for initialization
	void Start ()
    {

        settings = new string[] { };
        completedLevels = new bool[5];// {true,false,false,false,false };
        
    }
	


 /// <summary>
 /// Save Method, Only for completed levels. Room for change in future.
 /// </summary>
    public void Save()
    {

        try
        {
            for (int i = 0; i < completedLevels.Length; i++)
            {
                PlayerPrefs.SetInt("Level" + i, completedLevels[i] ? 1 : 0);
                Debug.Log("Saved : Level" + i + " : " + completedLevels[i]);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("SAVE FAILED: DANGER");
        }

        

    }


    /// <summary>
    /// LOAd methoD, also only for completed atm. 
    /// </summary>
    public void Load()
    {

        try
        {
            for (int i = 0; i < completedLevels.Length; i++)
            {
                if (PlayerPrefs.HasKey("Level" + i))
                {
                    if (PlayerPrefs.GetInt("Level" + i) == 0)
                    {
                        completedLevels[i] = false;
                    }
                    else
                    {
                        completedLevels[i] = true;
                    }
                    Debug.Log("Loaded : Level" + i + " : " + completedLevels[i]);
                }

            }
        }
        catch (System.Exception)
        {
            Debug.Log("LOADED FAILED: DANGER");
        }

        
    }
}
