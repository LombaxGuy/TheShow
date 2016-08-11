using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private static bool isPaused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool getPauseState()
    {
        return isPaused;
    }

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
