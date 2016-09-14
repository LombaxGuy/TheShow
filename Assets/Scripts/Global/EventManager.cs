using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerDeathDelegate();
    public delegate void PlayerRespawnDelegate();
    public delegate void PlayerJumpDelegate();
    public delegate void PlayerPickupDelegate();
    public delegate void PlayerCrouchDelegate();
    public delegate void SaveGameDelegate();
    public delegate void LoadGameDelegate();
    public delegate void SavePrefDelegate();
    public delegate void LoadPrefDelegate();
    public delegate void CheckForSettingChangesDelegate();
    public delegate void ApplySettingChangesDelegate();
    public delegate void SettingsChangedDelegate();
    public delegate void ResetSettingsDelegate();
    public delegate void ResetToDefaultSettingsDelegate();

    public static event PlayerDeathDelegate OnPlayerDeath;
    public static event PlayerRespawnDelegate OnPlayerRespawn;
    public static event PlayerJumpDelegate OnPlayerJump;
    public static event PlayerPickupDelegate OnPlayerPickup;
    public static event PlayerCrouchDelegate OnPlayerCrouch;
    public static event SaveGameDelegate OnSaveGame;
    public static event LoadGameDelegate OnLoadGame;
    public static event SavePrefDelegate OnSavePref;
    public static event LoadPrefDelegate OnLoadPref;
    public static event CheckForSettingChangesDelegate OnCheckForSettingChanges;
    public static event ApplySettingChangesDelegate OnApplySettingChanges;
    public static event SettingsChangedDelegate OnSettingsChanged;
    public static event ResetSettingsDelegate OnResetSettings;
    public static event ResetToDefaultSettingsDelegate OnResetToDefaultSettings;

    public static void RaiseOnPlayerDeath()
    {
        if (OnPlayerDeath != null)
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerDeath' raised.");
            OnPlayerDeath.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerDeath' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnPlayerRespawn()
    {
        if (OnPlayerRespawn != null)
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerRespawn' raised");
            OnPlayerRespawn.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerRespawn' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnPlayerJump()
    {
        if (OnPlayerJump != null)
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerJump' raised");
            OnPlayerJump.Invoke();
        }
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerJump' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnPlayerPickup()
    {
        if (OnPlayerPickup != null)
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerPickup' raised.");
            OnPlayerPickup.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerPickup' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnPlayerCrouch()
    {
        if (OnPlayerCrouch != null)
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerCrouch' raised.");
            OnPlayerCrouch.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnPlayerCrouch' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnSaveGame()
    {
        if (OnSaveGame != null)
        {
            Debug.Log("EventManager.cs: Event 'OnSaveGame' raised.");
            OnSaveGame.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnSaveGame' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnLoadGame()
    {
        if (OnLoadGame != null)
        {
            Debug.Log("EventManager.cs: Event 'OnLoadGame' raised.");
            OnLoadGame.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnLoadGame' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnSavePref()
    {
        if (OnSavePref != null)
        {
            Debug.Log("EventManager.cs: Event 'OnSavePref' raised.");
            OnSavePref.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnSavePref' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnLoadPref()
    {
        if (OnLoadPref != null)
        {
            Debug.Log("EventManager.cs: Event 'OnLoadPref' raised.");
            OnLoadPref.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnLoadPref' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnCheckForSettingChanges()
    {
        if (OnCheckForSettingChanges != null)
        {
            Debug.Log("EventManager.cs: Event 'OnCheckForSettingChanges' raised.");
            OnCheckForSettingChanges.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnCheckForSettingChanges' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnApplySettingChanges()
    {
        if (OnApplySettingChanges != null)
        {
            Debug.Log("EventManager.cs: Event 'OnApplySettingChanges' raised.");
            OnApplySettingChanges.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnApplySettingChanges' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnSettingsChanged()
    {
        if (OnSettingsChanged != null)
        {
            Debug.Log("EventManager.cs: Event 'OnSettingsChanged' raised.");
            OnSettingsChanged.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnSettingsChanged' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnResetSettings()
    {
        if (OnResetSettings != null)
        {
            Debug.Log("EventManager.cs: Event 'OnResetSettings' raised.");
            OnResetSettings.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnResetSettings' not raised because nothing subscibes to it.");
        }
    }

    public static void RaiseOnResetToDefaultSettings()
    {
        if (OnResetToDefaultSettings != null)
        {
            Debug.Log("EventManager.cs: Event 'OnResetToDefaultSettings' raised.");
            OnResetToDefaultSettings.Invoke();
        }
        else
        {
            Debug.Log("EventManager.cs: Event 'OnResetToDefaultSettings' not raised because nothing subscibes to it.");
        }
    }
}
