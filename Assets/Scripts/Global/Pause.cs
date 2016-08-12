using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    //The Global pause variable
    private static bool isPaused;

    /// <summary>
    /// This is used to get the pause state of the game.
    /// </summary>
    /// <returns>Returns true if the game is paused. Returned false if the game is not paused.</returns>
    public static bool GetPauseState()
    {
        return isPaused;
    }

    /// <summary>
    /// This is used to set the pause to a state, Like when pausing using the menu.
    /// </summary>
    /// <param name="state">The state of pause, true is paused false is not paused</param>
   public static void SetPauseState(bool state)
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
