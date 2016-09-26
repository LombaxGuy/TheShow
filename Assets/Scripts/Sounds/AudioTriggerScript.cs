using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioTriggerScript : MonoBehaviour {

    private AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("AudioClip Whoosh")]
    private AudioClip audioClip;

    private bool soundPlayed = false;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (soundPlayed != true)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.tag == "Player")
                {
                    soundPlayed = true;
                    audioPlayer.PlayOneShot(audioClip, 0.7f);
                    Debug.Log("Sound Played");
                }
            }
        }
        else
        {
            soundPlayed = false;
        }
    }
}
