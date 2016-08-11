using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    //The Global pause variable
    private static bool isPaused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //This is used to get the paused state of the game
    public static bool getPauseState()
    {
        return isPaused;
    }

    /// <summary>
    /// This is used to set the pause to a state, Like when pausing using the menu.
    /// Put this in update.
    /// Use if(!Pause.setPauseState())
    /// {
    /// 
    /// {
    /// </summary>
    /// <param name="state">The state of pause, true is paused false is not paused</param>
   public static void setPauseState(bool state)
    {
        isPaused = state;
        if(state == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
}
