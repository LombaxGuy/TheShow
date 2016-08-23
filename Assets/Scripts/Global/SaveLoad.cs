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
    private static GameObject[] gObject;
    private static GameObject soundObject;
    private static Canvas[] cObject;
    private static Canvas soundCanvas;
    private static float[] value = new float[8];

    private static SaveGame save = new SaveGame();

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
        //Gets the saved PlayerPrefKeys from the regestry
        for (int i = 0; i < save.prefKeys.Capacity; i++)
        {
            if (PlayerPrefs.HasKey(save.prefKeys[i]))
            {
                PlayerPrefs.GetFloat(save.prefKeys[i]);
            }
        }
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
        SavePrefs();


        //try
        //{
        //    for (int i = 0; i < completedLevels.Length; i++)
        //    {
        //        PlayerPrefs.SetInt("Level" + i, completedLevels[i] ? 1 : 0);
        //        Debug.Log("Saved : Level" + i + " : " + completedLevels[i]);
        //    }
        //}
        //catch (System.Exception)
        //{
        //    Debug.Log("SAVE FAILED: DANGER");
        //}


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
        gObject = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gObject.Length; i++)
        {
            if (gObject[i].name == "SettingsObject")
            {
                soundObject = gObject[i];
                Debug.Log(soundObject.name);
            }
            Debug.Log(gObject[i].name);
        }

        soundObject.GetComponent<SoundSettings>().Mixer[0].GetFloat("masterVol", out value[0]);
        soundObject.GetComponent<SoundSettings>().Mixer[1].GetFloat("musicVol", out value[1]);
        soundObject.GetComponent<SoundSettings>().Mixer[2].GetFloat("fxVol", out value[2]);
        soundObject.GetComponent<SoundSettings>().Mixer[3].GetFloat("voiceVol", out value[3]);

        PlayerPrefs.SetFloat("masterVol", value[0]);
        PrefKeys.Add("masterVol");
        PlayerPrefs.SetFloat("musicVol", value[1]);
        PrefKeys.Add("musicVol");
        PlayerPrefs.SetFloat("fxVol", value[2]);
        PrefKeys.Add("fxVol");
        PlayerPrefs.SetFloat("voiceVol", value[3]);
        PrefKeys.Add("voiceVol");
        //Debug.Log(value1);
        //Debug.Log(value2);
        //Debug.Log(value3);
        //Debug.Log(value4);

    }

    public static void LoadPrefs()
    {
        cObject = Canvas.FindObjectsOfType<Canvas>();
        for (int i = 0; i < cObject.Length; i++)
        {
            if (cObject[i].name == "SettingsMenu")
            {
                soundCanvas = cObject[i];
                Debug.Log(soundCanvas.name);
            }
            Debug.Log(cObject[i].name);
        }

        //soundCanvas.transform.GetChild(0).GetComponent<Slider>().value = value[0];
        //soundCanvas.transform.GetChild(1).GetComponent<Slider>().value = value[1];
        //soundCanvas.transform.GetChild(2).GetComponent<Slider>().value = value[2];
        //soundCanvas.transform.GetChild(3).GetComponent<Slider>().value = value[3];
    }

    public static void DeleteSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData/SaveGame.blargh");
        }
    }
}
