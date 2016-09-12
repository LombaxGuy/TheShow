using UnityEngine;
using System.Collections;

public class SpeakerTrigger : MonoBehaviour
{
    //Remember SpeakerManeger and SpeakerScript
    //Set on trigger

    //Set this in the editor, has to be the gameobject with AudioManager
    [SerializeField]
    [Tooltip("The GameObject that has the SpeakerManager-component.")]
    private GameObject speakerManager;

    //Editor list. Remember to drag and drop your sounds
    [SerializeField]
    [Tooltip("Set Audio to be played on speakers")]
    private AudioClip[] audioClips;

    /// <summary>
    /// Used to begin playing an array of audioclips over the speakers.
    /// </summary>
    /// <param name="other">The object that collides with the trigger box</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            speakerManager.GetComponent<SpeakerManager>().PlaySpeakerSoundInstance(audioClips);
        }

        Destroy(GetComponent<BoxCollider>());
    }
}
