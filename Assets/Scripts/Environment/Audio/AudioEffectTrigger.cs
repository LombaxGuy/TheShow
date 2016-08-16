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
    [Tooltip("Toggle if you want to disable the collider after one usage.")]
    private bool oneTime;
    /// <summary>
    /// Used to trigger a onetime soundeffect on an object. Other object needs to have an audiosource-component.
    /// </summary>
    /// <param name="other">The object that collides with the triggerbox.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && target.GetComponent<AudioSource>())
        {
            target.GetComponent<AudioSource>().PlayOneShot(setClip);

            if(oneTime)
            {
                gameObject.GetComponent<Collider>().enabled = false;
            }
            
        }
    }

}
