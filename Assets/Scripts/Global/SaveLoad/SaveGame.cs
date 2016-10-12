using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// If we need different types of save classes this class should be made abstract and the other classes needs to inheirit from this one
//These different classe could be HighScore, PlayerData, our StatTracker and more
[Serializable]
public class SaveGame
{
    //The variables we save
    public int totalTimesDead;
    public int timesKilledBySpikes;
    public int timesKilledBySpinners;
    public int timesKilledByFalling;
    public int timesKilledByShocks;
    public int timesKilledByGas;
    public float totalTimeSpend;
    public int levelsCompleted ;
    public string currentLevel ;
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public List<string> prefKeys = SaveLoad.PrefKeys;
    public List<AudioClip> savedClips;
    public List<float> savedTimeBetweenClips;


    /// <summary>
    /// Sets all the values in StatTracker.cs that needs to be saved
    /// </summary>
    public void SetStatTrackerValues()
    {
        totalTimesDead = StatTracker.TotalTimesDead;
        timesKilledBySpikes = StatTracker.TimesKilledBySpikes;
        timesKilledBySpinners = StatTracker.TimesKilledBySpinners;
        timesKilledByFalling = StatTracker.TimesKilledByFalling;
        timesKilledByShocks = StatTracker.TimesKilledByShocks;
        timesKilledByGas = StatTracker.TimesKilledByGas;
        totalTimeSpend = StatTracker.TotalTimeSpend;
        levelsCompleted = StatTracker.LevelsCompleted;
        currentLevel = StatTracker.CurrentLevel;
    }
    
    public void SetMusicClip(List<AudioClip> audio, List<float> timeBetweenClips)
    {
        savedClips = audio;
        savedTimeBetweenClips = timeBetweenClips;

    }

    public List<AudioClip> GetMusicClip()
    {
        return savedClips;
    }

    public List<float> GetTimeBetweenClips()
    {
        return savedTimeBetweenClips;
    }

    /// <summary>
    /// Sets all PlayerValues that needs to be saved
    /// </summary>
    /// <param name="posX">The players X position</param>
    /// <param name="posY">The players Y position</param>
    /// <param name="posZ">The Players Z position</param>
    public void SetPlayerValues(float posX, float posY, float posZ)
    {
        playerPosX = posX;
        playerPosY = posY;
        playerPosZ = posZ;
    }

    /// <summary>
    /// Gets the stored StatTracker values and sets the StatTracker values to be equal to the saved ones
    /// </summary>
    public void GetStatTrackerValues()
    {
        StatTracker.TotalTimesDead = totalTimesDead;
        StatTracker.TimesKilledBySpikes = timesKilledBySpikes;
        StatTracker.TimesKilledBySpinners = timesKilledBySpinners;
        StatTracker.TimesKilledByFalling = timesKilledByFalling;
        StatTracker.TimesKilledByShocks = timesKilledByShocks;
        StatTracker.TimesKilledByGas = timesKilledByGas;
        StatTracker.SavedTotalTimeSpend = totalTimeSpend;
        StatTracker.LevelsCompleted = levelsCompleted;
        StatTracker.CurrentLevel = currentLevel;
    }

    /// <summary>
    /// Resets the current StatTracker values and deletes the savedata
    /// </summary>
    public void DeleteSaveData()
    {
        StatTracker.TotalTimesDead = 0;
        StatTracker.TimesKilledBySpikes = 0;
        StatTracker.TimesKilledBySpinners = 0;
        StatTracker.TimesKilledByFalling = 0;
        StatTracker.TimesKilledByShocks = 0;
        StatTracker.TimesKilledByGas = 0;
        StatTracker.SavedTotalTimeSpend = 0;
        StatTracker.TotalTimeSpend = 0;
        StatTracker.TimeSpendOnAllLevels = 0;
        StatTracker.LevelsCompleted = 0;
        StatTracker.CurrentLevel = SceneManager.GetActiveScene().name;
        SaveLoad.DeleteSaveData();
    }


}

