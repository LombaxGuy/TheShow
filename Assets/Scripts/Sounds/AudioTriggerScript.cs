using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioTriggerScript : MonoBehaviour {

    private static AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("AudioClip Whoosh")]
    private AudioClip audioClip;

    void Start() {
        audioPlayer = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioPlayer.PlayOneShot(audioClip, 0.7f);
        }
    }
}
