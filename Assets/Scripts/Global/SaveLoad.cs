using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {


    //Unity egen save system. PlayerPref. Eller xml eller JSON. || PlayerPref kræver mindst arbejde. xml eller JSON er nemmere at rette i bagefter?

    //private array med bools?
    //get og set for array med bools
    //private array med host mind bools?
    //get og set også for det
    //Settings skal også gemmes
    

	// Use this for initialization
	void Start ()
    {
        //Load();  eller når man klikker på "Continue" knappen i main menu. Så "Start" metoden kan fjernes.
	
	}
	

    //Array med bools, om spilleren har klaret en bane eller ej.
    //Host mind skal gemmes. 

    //Method to save data
    public void Save()
    {
        //En som checker om man har saved. Se om der er sket en fejl?
        //PlayerPrefsX.SetStringArray("Names", names)
        // PlayerPrefs.SetInt("LEVELREACHED", InsertNumber);
    }


    //Method to load data
    public void Load()
    {
        // Skal køres hvis der er noget i den. Ellers ligger den default values ind. Ved bare at køre save..
        // LevelReached = PlayerPrefs.GetInt("LEVELREACHED");
        // something = PlayerPrefsX.GetStringArray("Names");
    }
}
