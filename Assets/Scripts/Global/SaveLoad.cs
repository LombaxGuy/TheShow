using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{


    //This is a save/load system. Uses PlayerPref for settings and file streaming for values
    //Settings not done
    //Host mind not done

    //Serialized fields. Used for the Settings Canvas slider names
    [SerializeField]
    private static string masterSliderName = "MasterSlider";
    [SerializeField]
    private static string musicSliderName = "MusicSlider";
    [SerializeField]
    private static string fxSliderName = "FXSlider";
    [SerializeField]
    private static string voiceSliderName = "VoiceSlider";


    private static Canvas[] cObject;
    private static Canvas soundCanvas;

    //The variable that stores the slider values
    [SerializeField]
    private static float[] value = new float[4];

    private static SaveGame save = new SaveGame();

    //List that stores our PlayerPref keys
    private static List<string> prefKeys = new List<string>();

    public static List<string> PrefKeys
    {
        get
        {
            return prefKeys;
        }

        set
        {
            prefKeys = value;
        }
    }

    // Use this for initialization
    static void Start()
    {
 
    }

    /// <summary>
    /// Creates a save directory if the user dosen't have one.
    /// Creates a file which is saved in the directory
    /// </summary>
    public static void Save(SaveGame save)
    {
        if(!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }


        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/SaveData/SaveGame.blargh");

        Debug.Log(Application.persistentDataPath);

        formatter.Serialize(fileStream, save);

        fileStream.Close();


    }


    /// <summary>
    /// Loads the save file if it exists
    /// </summary>
    public static SaveGame Load()
    {
        SaveGame save;
        if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath + "/SaveData/SaveGame.blargh", FileMode.Open);

            save = (SaveGame) formatter.Deserialize(fileStream);

            fileStream.Close();

            return save;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Saves all settings in PlayerPrefs
    /// </summary>
    public static void SavePrefs()
    {
        //Finds the Settings canvas
        cObject = Canvas.FindObjectsOfType<Canvas>();
        for (int i = 0; i < cObject.Length; i++)
        {
            if (cObject[i].name == "SettingsObject")
            {
                soundCanvas = cObject[i];
            }
        }

        //Saves our slider values in the value variable

        value[0] = soundCanvas.transform.Find(masterSliderName).GetComponent<Slider>().value;
        value[1] = soundCanvas.transform.Find(musicSliderName).GetComponent<Slider>().value;
        value[2] = soundCanvas.transform.Find(fxSliderName).GetComponent<Slider>().value;
        value[3] = soundCanvas.transform.Find(voiceSliderName).GetComponent<Slider>().value;
        
        //Saves the slider values in playerprefs
        PlayerPrefs.SetFloat("masterVol", value[0]);
        PrefKeys.Add("masterVol");
        PlayerPrefs.SetFloat("musicVol", value[1]);
        PrefKeys.Add("musicVol");
        PlayerPrefs.SetFloat("fxVol", value[2]);
        PrefKeys.Add("fxVol");
        PlayerPrefs.SetFloat("voiceVol", value[3]);
        PrefKeys.Add("voiceVol");
    }


    /// <summary>
    /// Loads the saved preferrences 
    /// </summary>
    public static void LoadPrefs()
    {
        //Goes through our Playerpref Key array and gets the values if we have the keys stored
        try
        {
            for (int i = 0; i < prefKeys.Count; i++)
            {
                if (PlayerPrefs.HasKey(prefKeys[i]))
                {
                    value[i] = PlayerPrefs.GetFloat(prefKeys[i]);
                    Debug.Log(value[i]);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.Log("LOADED FAILED: DANGER");
        }

        //Finds our settings canvas
        cObject = Canvas.FindObjectsOfType<Canvas>();
        for (int i = 0; i < cObject.Length; i++)
        {
            if (cObject[i].name == "SettingsMenu")
            {
                soundCanvas = cObject[i];
            }
        }

        //Sets our sliders
        soundCanvas.transform.Find(masterSliderName).GetComponent<Slider>().value = value[0];
        soundCanvas.transform.Find(musicSliderName).GetComponent<Slider>().value = value[1];
        soundCanvas.transform.Find(fxSliderName).GetComponent<Slider>().value = value[2];
        soundCanvas.transform.Find(voiceSliderName).GetComponent<Slider>().value = value[3];
    }

    /// <summary>
    /// Deletes our save file if it exists.
    /// </summary>
    public static void DeleteSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData/SaveGame.blargh");
        }
    }
}
