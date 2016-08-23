using UnityEngine;
using System.Collections;

public class GameObjectPositionResetTemplate : MonoBehaviour {


    /// <summary>
    /// This is a template script for GameObjectPositionReset.
    /// When the Player dies, Name will be called.
    /// </summary>

    void OnEnable()
    {
        GameObjectPositionReset.OnResetObjects += Name;
    }

    void OnDisable()
    {
        GameObjectPositionReset.OnResetObjects -= Name;
    }

    void Name()
    {
        Debug.Log("Called from GameObjectPositionReset");
    }
}
