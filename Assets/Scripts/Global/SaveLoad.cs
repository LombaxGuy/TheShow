using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{


    //This is a save/load system. Using Unity PlayerPrefs to create or get data from a place on the local computer.
    //Settings not done
    //Host mind not done
    private static Canvas[] canvas;
    private static Canvas soundCanvas;
    private static float[] value;

    private static List<string> prefKeys = new List<string>();

    // Use this for initialization
    static void Start()
    {
        for (int i = 0; i < prefKeys.Capacity; i++)
        {
            if(PlayerPrefs.HasKey(prefKeys[i]))
            {
                PlayerPrefs.GetFloat(prefKeys[i]);
            }
        }
    }



    /// <summary>
    /// Save Method, Only for completed levels. Room for change in future.
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
    /// LOAd methoD, also only for completed atm. 
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

        //try
        //{
        //    for (int i = 0; i < completedLevels.Length; i++)
        //    {
        //        if (PlayerPrefs.HasKey("Level" + i))
        //        {
        //            if (PlayerPrefs.GetInt("Level" + i) == 0)
        //            {
        //                completedLevels[i] = false;
        //            }
        //            else
        //            {
        //                completedLevels[i] = true;
        //            }
        //            Debug.Log("Loaded : Level" + i + " : " + completedLevels[i]);
        //        }

        //    }
        //}
        //catch (System.Exception)
        //{
        //    Debug.Log("LOADED FAILED: DANGER");
        //}


    }

    public static void SavePrefs()
    {
        canvas = Canvas.FindObjectsOfType<Canvas>();

        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].name == "SoundSettings")
            {
                soundCanvas = canvas[i];
            }
        }

        soundCanvas.GetComponent<SoundSettings>().Mixer[0].GetFloat("masterVol", out value[0]);
        soundCanvas.GetComponent<SoundSettings>().Mixer[1].GetFloat("musicVol", out value[1]);
        soundCanvas.GetComponent<SoundSettings>().Mixer[2].GetFloat("fxVol", out value[2]);
        soundCanvas.GetComponent<SoundSettings>().Mixer[3].GetFloat("voiceVol", out value[3]);
        PlayerPrefs.SetFloat("masterVol", value[0]);
        prefKeys.Add("masterVol");
        PlayerPrefs.SetFloat("musicVol", value[1]);
        prefKeys.Add("musicVol");
        PlayerPrefs.SetFloat("fxVol", value[2]);
        prefKeys.Add("fxVol");
        PlayerPrefs.SetFloat("voiceVol", value[3]);
        prefKeys.Add("voiceVol");
    }

    public static void LoadPrefs()
    {
    }

    public static void DeleteSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData/SaveGame.blargh");
        }
    }
}
