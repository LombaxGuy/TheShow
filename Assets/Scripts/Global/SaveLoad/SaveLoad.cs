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
    public static void SaveSoundPrefs(float master, float music, float fx, float voice)
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
    /// Called in settings methods when apply is pressed. This is for saving strings
    /// </summary>
    /// <param name="name">The name of the variable that needs to be saved. This is the same name that should be used in the load function</param>
    /// <param name="value">The value of the variable which needs to be saved</param>
    public static void SaveSettings(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
        //If the value is not stored in our playerpref list it is added to the list
        if (!prefKeys.Contains(name))
        {
            prefKeys.Add(name);
        }
    }

    /// <summary>
    /// Called in settings methods when apply is pressed. This is for saving ints
    /// </summary>
    /// <param name="name">The name of the variable that needs to be saved. This is the same name that should be used in the load function</param>
    /// <param name="intValue">The value of the variable which needs to be saved</param>
    public static void SaveSettings(string name, int intValue)
    {
        PlayerPrefs.SetInt(name, intValue);
        //If the value is not stored in our playerpref list it is added to the list
        if (!prefKeys.Contains(name))
        {
            prefKeys.Add(name);
        }
    }

    /// <summary>
    /// Called in settings methods when apply is pressed. This is for saving floats
    /// </summary>
    /// <param name="name">The name of the variable that needs to be saved. This is the same name that should be used in the load function</param>
    /// <param name="floatValue">The value of the variable which needs to be saved</param>
    public static void SaveSettings(string name, float floatValue)
    {
        PlayerPrefs.SetFloat(name, floatValue);
        //If the value is not stored in our playerpref list it is added to the list
        if (!prefKeys.Contains(name))
        {
            prefKeys.Add(name);
        }
    }

    public static void SaveSettings(string name, bool boolValue)
    {
        if(boolValue == false)
        {
            PlayerPrefs.SetInt(name, 0);
        }
        else
        {
            PlayerPrefs.SetInt(name, 1);
        }


        //If the value is not stored in our playerpref list it is added to the list
        if (!prefKeys.Contains(name))
        {
            prefKeys.Add(name);
        }
    }


    /// <summary>
    /// /Loads the saved preferences and returns it in a float array.
    /// </summary>
    /// <returns>Saves all PlayerPref key values in a float array</returns>
    public static float[] LoadSoundPrefs()
    {
        //Goes through our Playerpref Key array and gets the values if we have the keys stored
        try
        {
            for (int i = 0; i < prefKeys.Count; i++)
            {
                if (PlayerPrefs.HasKey("masterVol"))
                {
                    value[0] = PlayerPrefs.GetFloat("masterVol");
                }
                if (PlayerPrefs.HasKey("musicVol"))
                {
                    value[1] = PlayerPrefs.GetFloat("musicVol");
                }
                if (PlayerPrefs.HasKey("fxVol"))
                {
                    value[2] = PlayerPrefs.GetFloat("fxVol");
                }
                if (PlayerPrefs.HasKey("voiceVol"))
                {
                    value[3] = PlayerPrefs.GetFloat("voiceVol");
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
    /// This is for loading string values. It's called in methods when the menu is loaded
    /// </summary>
    /// <param name="name">The name which the variable was saved with</param>
    /// <returns>Retuns the saved setting if it exists. Else the method returns null</returns>
    public static string LoadSettingString(string name)
    {
        string setting = "";

        if (PlayerPrefs.HasKey(name))
        {
            setting = PlayerPrefs.GetString(name);
            return setting;
        }
        else
        {
            return null;
        }


    }

    /// <summary>
    /// This is for loading int values. It's called in methods when the menu is loaded
    /// </summary>
    /// <param name="name">The name which the variable was saved with</param>
    /// <returns>Retuns the saved setting if it exists. Else the method returns null</returns>
    public static int LoadSettingInt(string name)
    {
        int setting = 0;
        if (PlayerPrefs.HasKey(name))
        {
            setting = PlayerPrefs.GetInt(name);
            return setting;
        }
        else
        {
            return -1;

        }
    }

    /// <summary>
    /// This is for loading float values. It's called in methods when the menu is loaded
    /// </summary>
    /// <param name="name">The name which the variable was saved with</param>
    /// <returns>Retuns the saved setting if it exists. Else the method returns null</returns>
    public static float LoadSettingFloat(string name)
    {
        float setting = 0;
        if (PlayerPrefs.HasKey(name))
        {
            setting = PlayerPrefs.GetFloat(name);
            return setting;
        }
        else
        {
            return -1;

        }
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
