using UnityEngine;
using System.Collections;

public class AudioSenderExample : MonoBehaviour
{
    //Set this in the editor, has to be the gameobject with AudioManager
    [SerializeField]
    [Tooltip("The GameObject that has the AudioManager-component.")]
    private GameObject audioManager;

    /// <summary>
    /// Test for sending playcall
    /// </summary>
    /// <param name="other">The object that collides with the trigger box.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioManager.GetComponent<AudioManager>().PlayRadioSoundInstance();
        }
    }
}
