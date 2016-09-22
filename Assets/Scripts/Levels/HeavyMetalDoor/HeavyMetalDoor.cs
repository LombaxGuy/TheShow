using UnityEngine;
using System.Collections;

public class HeavyMetalDoor : MonoBehaviour
{
    [SerializeField]
    private float timeFromOpenToClose = 3.0f;

    [SerializeField]
    private float timeFromCloseToOpen = 1.0f;

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

    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if (doorOpen)
        {
            if (cooldown > timeFromOpenToClose)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = false;
                audioPlayer.PlayOneShot(audioClips[0]);
            }
        }
        else
        {
            if (cooldown > timeFromCloseToOpen)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = true;
                audioPlayer.PlayOneShot(audioClips[1]);
            }
        }
    }
}
