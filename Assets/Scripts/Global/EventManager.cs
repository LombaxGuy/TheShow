using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerDeathDelegate();
    public delegate void PlayerRespawnDelegate();
    public delegate void SaveGameDelegate();
    public delegate void LoadGameDelegate();
    public delegate void SavePrefDelegate();
    public delegate void LoadPrefDelegate();
    public delegate void CheckForSettingChangesDelegate();
    public delegate void ApplySettingChangesDelegate();

    public static event PlayerDeathDelegate OnPlayerDeath;
    public static event PlayerRespawnDelegate OnPlayerRespawn;
    public static event SaveGameDelegate OnSaveGame;
    public static event LoadGameDelegate OnLoadGame;
    public static event SavePrefDelegate OnSavePref;
    public static event LoadPrefDelegate OnLoadPref;
    public static event CheckForSettingChangesDelegate OnCheckForSettingChanges;
    public static event ApplySettingChangesDelegate OnApplySettingChanges;

    public static void RaiseOnPlayerDeath()
    {
        Debug.Log("EventManager.cs: Event 'OnPlayerDeath' raised.");
        OnPlayerDeath();
    }

    public static void RaiseOnPlayerRespawn()
    {
        Debug.Log("EventManager.cs: Event 'OnPlayerRespawn' raised");
        OnPlayerRespawn();
    }

    public static void RaiseOnSaveGame()
    {
        Debug.Log("EventManager.cs: Event 'OnSaveGame' raised.");
        OnSaveGame();
    }

    public static void RaiseOnLoadGame()
    {
        Debug.Log("EventManager.cs: Event 'OnLoadGame' raised.");
        OnLoadGame();
    }

    public static void RaiseOnSavePref()
    {
        Debug.Log("EventManager.cs: Event 'OnSavePref' raised.");
        OnSavePref();
    }

    public static void RaiseOnLoadPref()
    {
        Debug.Log("EventManager.cs: Event 'OnLoadPref' raised.");
        OnLoadPref();
    }

    public static void RaiseOnCheckForSettingChanges()
    {
        Debug.Log("EventManager.cs: Event 'OnCheckForSettingChanges' raised.");
        OnCheckForSettingChanges();
    }

    public static void RaiseOnApplySettingChanges()
    {
        Debug.Log("EventManager.cs: Event 'OnApplySettingChanges' raised.");
        OnApplySettingChanges();
    }
}
