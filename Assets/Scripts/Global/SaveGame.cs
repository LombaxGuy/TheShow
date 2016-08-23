using System;
using System.Collections.Generic;

// If we need different types of save classes this class should be made abstract and the other classes needs to inheirit from this one
//These different classe could be HighScore, PlayerData, our StatTracker and more
[Serializable]
public class SaveGame
{
    //The variables we save
    public int totalTimesDead = StatTracker.TotalTimesDead;
    public int timesKilledBySpikes = StatTracker.TimesKilledBySpikes;
    public int timesKilledBySpinners = StatTracker.TimesKilledBySpinners;
    public int timesKilledByFalling = StatTracker.TimesKilledByFalling;
    public int timesKilledByShocks = StatTracker.TimesKilledByShocks;
    public int timesKilledByGas = StatTracker.TimesKilledByGas;
    public float totalTimeSpend = StatTracker.TotalTimeSpend;
    public int levelsCompleted = StatTracker.LevelsCompleted;
    public string currentLevel = StatTracker.CurrentLevel;
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public List<string> prefKeys = SaveLoad.PrefKeys;
}

