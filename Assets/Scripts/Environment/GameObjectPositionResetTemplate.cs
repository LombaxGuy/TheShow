using UnityEngine;
using System.Collections;

public class GameObjectPositionResetTemplate : MonoBehaviour {


    /// <summary>
    /// This is a template script for GameObjectPositionReset.
    /// When the Player dies, Name will be called.
    /// </summary>

    void OnEnable()
    {
        EventManager.OnPlayerRespawn += Name;
    }

    void OnDisable()
    {
        EventManager.OnPlayerRespawn -= Name;
    }

    void Name()
    {
        Debug.Log("Called from GameObjectPositionReset");
    }
}
