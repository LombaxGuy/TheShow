﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StatTracker : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject textParent;
    [SerializeField]
    private Text timesDeadText;
    [SerializeField]
    private Text timeSpendText;
    [SerializeField]
    private Text levelsCompletedText;
    [SerializeField]
    private Text currentLevelText;
    [SerializeField]
    private Text timeSpendOnCurrentLevelText;
    [SerializeField]
    private Text timeSpendInOneSettingText;
    [SerializeField]
    private Text frameRateText;

    private bool displayDebug = false;

    private static int totalTimesDead = 0;
    private static int timesKilledBySpikes = 0;
    private static int timesKilledBySpinners = 0;
    private static int timesKilledByFalling = 0;
    private static int timesKilledByShocks = 0;
    private static int timesKilledByGas = 0;

    private static float savedTotalTimeSpend = 0;
    private static float totalTimeSpend = 0;
    private static float timeSpendInOneSetting = 0;
    private static float timeSpendOnCurrentLevel = 0;
    private static float timeSpendOnAllLevels = 0;

    private static int levelsCompleted = 0;
    private static string currentLevel = "";

    public static int TotalTimesDead
    {
        get { return totalTimesDead; }
        set { totalTimesDead = value; }
    }

    public static int TimesKilledBySpikes
    {
        get { return timesKilledBySpikes; }
        set { timesKilledBySpikes = value; }
    }

    public static int TimesKilledBySpinners
    {
        get { return timesKilledBySpinners; }
        set { timesKilledBySpinners = value; }
    }

    public static int TimesKilledByFalling
    {
        get { return timesKilledByFalling; }
        set { timesKilledByFalling = value; }
    }

    public static int TimesKilledByShocks
    {
        get { return timesKilledByShocks; }
        set { timesKilledByShocks = value; }
    }

    public static int TimesKilledByGas
    {
        get { return timesKilledByGas; }
        set { timesKilledByGas = value; }
    }

    public static float SavedTotalTimeSpend
    {
        get
        {
            return savedTotalTimeSpend;
        }

        set
        {
            savedTotalTimeSpend = value;
        }
    }

    public static float TotalTimeSpend
    {
        get { return totalTimeSpend; }
        set { totalTimeSpend = value; }
    }

    public static float TimeSpendInOneSetting
    {
        get { return timeSpendInOneSetting; }
        set { timeSpendInOneSetting = value; }
    }

    public static float TimeSpendOnCurrentLevel
    {
        get { return timeSpendOnCurrentLevel; }
        set { timeSpendOnCurrentLevel = value; }
    }

    public static float TimeSpendOnAllLevels
    {
        get
        {
            return timeSpendOnAllLevels;
        }

        set
        {
            timeSpendOnAllLevels = value;
        }
    }

    public static int LevelsCompleted
    {
        get { return levelsCompleted; }
        set { levelsCompleted = value; }
    }

    public static string CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }



    #endregion

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        LogTimeSpend();

        UpdateDebuggingWindow();

        if (Input.GetKeyDown(KeyBindings.KeyToggleDebug))
        {
            ToggleDebuggingWindow();
        }
    }

    /// <summary>
    /// Sets the time values
    /// </summary>
    private void LogTimeSpend()
    {
        totalTimeSpend = savedTotalTimeSpend + timeSpendOnAllLevels + Time.timeSinceLevelLoad; // This variable should be set to something else. Need the savesystem to work for it to function as intended.,

        timeSpendOnCurrentLevel = Time.timeSinceLevelLoad;
        timeSpendInOneSetting = Time.time;
    }

    /// <summary>
    /// Updates the text elements of the debugging window.
    /// </summary>
    private void UpdateDebuggingWindow()
    {
        timesDeadText.text = totalTimesDead.ToString();
        timeSpendText.text = HelperFunctions.ConvertToTimeFormat(totalTimeSpend);
        levelsCompletedText.text = levelsCompleted.ToString();
        currentLevelText.text = currentLevel.ToString();
        timeSpendOnCurrentLevelText.text = HelperFunctions.ConvertToTimeFormatDebug(timeSpendOnCurrentLevel);
        timeSpendInOneSettingText.text = HelperFunctions.ConvertToTimeFormatDebug(timeSpendInOneSetting);
        frameRateText.text = (Mathf.RoundToInt(1.0f / Time.deltaTime)).ToString();
    }

    /// <summary>
    /// Displays debug values to the screen or turns them off.
    /// </summary>
    private void ToggleDebuggingWindow()
    {
        // Toggles the active state of the parent GameObject inside the debugging canvas.
        textParent.SetActive(!textParent.activeInHierarchy);
    }
}
