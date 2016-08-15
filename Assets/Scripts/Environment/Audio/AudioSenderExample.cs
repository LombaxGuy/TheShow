using UnityEngine;
using System.Collections;

public class AudioSenderExample : MonoBehaviour {

    //Set this in the editor, has to be the gameobject with AudioManager
    [SerializeField]
    private GameObject audioManager;

    /// <summary>
    /// Test for sending playcall
    /// </summary>
    /// <param name="other">the player</param>
    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            audioManager.GetComponent<AudioManager>().PlayRadioSoundInstance();
        }

    }
}
