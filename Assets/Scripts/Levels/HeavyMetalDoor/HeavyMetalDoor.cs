using UnityEngine;
using System.Collections;

public class HeavyMetalDoor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The time in seconds this door waits before starting the main loop.")]
    private float offset = 0.0f;

    [SerializeField]
    [Tooltip("The time in seconds from the door begins opening till it begins closeing.")]
    private float openTime = 3.0f;

    [SerializeField]
    [Tooltip("The time in seconds from the door begins closeing till it begins opening.")]
    private float closeTime = 1.0f;

    [SerializeField]
    private Animator doorAnimator;

    private float cooldown = 0;

    private bool doorOpen = true;

    private AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("AudioClips. 0 = Close, 1 = Open")]
    private AudioClip[] audioClips;

    // Use this for initialization
    void Start()
    {
        audioPlayer = GetComponentInChildren<AudioSource>();
        cooldown -= offset;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if (doorOpen)
        {
            if (cooldown > openTime)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = false;
                audioPlayer.PlayOneShot(audioClips[0]);
            }
        }
        else
        {
            if (cooldown > closeTime)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = true;
                audioPlayer.PlayOneShot(audioClips[1]);
            }
        }
    }
}
