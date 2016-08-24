using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerDeathDelegate();
    public delegate void PlayerRespawnDelegate();

    public static event PlayerDeathDelegate OnPlayerDeath;
    public static event PlayerRespawnDelegate OnPlayerRespawn;

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
}
