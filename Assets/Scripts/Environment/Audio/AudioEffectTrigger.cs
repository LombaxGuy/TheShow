using UnityEngine;
using System.Collections;

public class AudioEffectTrigger : MonoBehaviour {
    //Target is the object you want to play sound.
    [SerializeField]
    private GameObject target;
    //setClip is the audio clip you want played.
    [SerializeField]
    private AudioClip setClip;

    /// <summary>
    /// Used to trigger a onetime soundeffect on an object. Other object needs to have audiosource component.
    /// </summary>
    /// <param name="other">player</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !target.GetComponent<AudioSource>() == false)
        {
            target.GetComponent<AudioSource>().PlayOneShot(setClip);
        }
    }

}
