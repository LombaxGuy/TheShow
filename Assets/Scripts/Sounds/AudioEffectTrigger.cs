using UnityEngine;
using System.Collections;

public class AudioEffectTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The GameObject that should play the sound. The GameObject must have an audiosource-component.")]
    private GameObject target;

    [SerializeField]
    [Tooltip("The audioclip that should be played.")]
    private AudioClip setClip;

    [SerializeField]
    [Tooltip("Set to true if you want to disable the collider after one usage.")]
    private bool oneTime = false;

    [SerializeField]
    [Tooltip("Set to true if you want to enable the collider when the player respawns.")]
    private bool resetOnRespawn = false;

    void OnEnable()
    {
        // Subscribes to the OnRespawnReset event if the player should reset on respawn
        if (resetOnRespawn)
        {
            EventManager.OnPlayerRespawn += OnPlayerRespawn;
        }
    }

    void OnDisable()
    {
        // Unsubscribes from the OnRewpawnReset event
        if (resetOnRespawn)
        {
            EventManager.OnPlayerRespawn -= OnPlayerRespawn; 
        }
    }

    /// <summary>
    /// Code is called when the OnRespawnReset event is called
    /// </summary>
    void OnPlayerRespawn()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    /// <summary>
    /// Used to trigger a onetime soundeffect on an object. Other object needs to have an audiosource-component.
    /// </summary>
    /// <param name="other">The object that collides with the triggerbox.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && target.GetComponent<AudioSource>())
        {
            target.GetComponent<AudioSource>().PlayOneShot(setClip);

            if (oneTime)
            {
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
