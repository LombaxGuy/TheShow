using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Array med bools, om spilleren har klaret en bane eller ej.
    //Host mind skal gemmes. 

    //Method to save data
    public void Save()
    {
        //PlayerPrefsX.SetStringArray("Names", names)
        // PlayerPrefs.SetInt("LEVELREACHED", InsertNumber);
    }


    //Method to load data
    public void Load()
    {
        // LevelReached = PlayerPrefs.GetInt("LEVELREACHED");
        // something = PlayerPrefsX.GetStringArray("Names");
    }
}
