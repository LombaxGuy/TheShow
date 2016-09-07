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

    //Fields. Used for the Settings Canvas slider names
    private static string masterSliderName = "MasterSlider";
    private static string musicSliderName = "MusicSlider";
    private static string fxSliderName = "FXSlider";
    private static string voiceSliderName = "VoiceSlider";

    //The variable that stores the slider values
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
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }

        save.SetStatTrackerValues();


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

            save = (SaveGame)formatter.Deserialize(fileStream);

            fileStream.Close();

            return save;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Saves all player settings in PlayerPrefs
    /// </summary>
    /// <param name="master">The value of the Mastervolume slider</param>
    /// <param name="music">The value of the Musicvolume slider</param>
    /// <param name="fx">The value of the Fxvolume slider</param>
    /// <param name="voice">The value of the Voicevolume slider</param>
    public static void SavePrefs(float master, float music, float fx, float voice)
    {
        //Saves the slider values in playerprefs
        PlayerPrefs.SetFloat("masterVol", master);
        if (!prefKeys.Contains("masterVol"))
        {
            prefKeys.Add("masterVol");
        }
        PlayerPrefs.SetFloat("musicVol", music);
        if (!prefKeys.Contains("musicVol"))
        {
            prefKeys.Add("musicVol");
        }
        PlayerPrefs.SetFloat("fxVol", fx);
        if (!prefKeys.Contains("fxVol"))
        {
            prefKeys.Add("fxVol");
        }
        PlayerPrefs.SetFloat("voiceVol", voice);
        if (!prefKeys.Contains("voiceVol"))
        {
            prefKeys.Add("voiceVol");
        }
    }


    /// <summary>
    /// /Loads the saved preferences and returns it in a float array.
    /// </summary>
    /// <returns>Saves all PlayerPref key values in a float array</returns>
    public static float[] LoadPrefs()
    {
        //Goes through our Playerpref Key array and gets the values if we have the keys stored
        try
        {
            for (int i = 0; i < prefKeys.Count; i++)
            {
                if (PlayerPrefs.HasKey(prefKeys[i]))
                {
                    value[i] = PlayerPrefs.GetFloat(prefKeys[i]);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.Log("LOADED FAILED: DANGER");
        }


        //returns the playerpref values
        return value;
    }

    /// <summary>
    /// Deletes our save file if it exists.
    /// </summary>
    public static void DeleteSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData/SaveGame.blargh");
        }
    }
}
