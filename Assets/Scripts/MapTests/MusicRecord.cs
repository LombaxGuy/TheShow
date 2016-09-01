using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicRecord : MonoBehaviour {

    [Header("Place your sound clips here")]
    [SerializeField]
    private AudioClip[] clips;

    private List<AudioClip> clipsQueue;

    private List<float> timeBetweenClip;

    private AudioSource soundSource;

    private float timeBetween;

    private float timeBetweenPlay;

    private int whatToPlay;

    private bool firstTimePress = false;

    private bool soundsAllowedToPlay = false;

    private bool allowedLoop = false;

    [SerializeField]
    private bool useMouse = true;

    private bool playerClicked = false;

    public bool PlayerClicked
    {
        get { return playerClicked; }
        set { playerClicked = value; }
    }

	// Use this for initialization
	void Start ()
    {
        clipsQueue = new List<AudioClip>();
        timeBetweenClip = new List<float>();
        soundSource = GetComponent<AudioSource>();
        soundSource.loop = false;
        whatToPlay = 0;
	}

    // Update is called once per frame
    void Update()
    {

        timeBetween += Time.deltaTime;
        timeBetweenPlay -= Time.deltaTime;
        PlayWithOrWithoutLoop();

        if(useMouse == false)
        {
            HandleKeys();
        }

    }


    /// <summary>
    /// This method is to play the sounds in the list. For it can play the next sound, it needs to wait for the time between them. This can be played in loop or not.
    /// If it is not on loop it will reset it self.
    /// </summary>
    private void PlayWithOrWithoutLoop()
    {
        if (soundsAllowedToPlay == true)
        {
            if (timeBetweenPlay <= 0)
            {
                timeBetweenPlay = timeBetweenClip[whatToPlay];
                soundSource.PlayOneShot(clipsQueue[whatToPlay]);
                whatToPlay++;

                if (whatToPlay >= clipsQueue.Count - 1)
                {
                    if (allowedLoop == true)
                    {
                        whatToPlay = 0;
                    }
                    else
                    {
                        whatToPlay = 0;
                        soundsAllowedToPlay = false;
                    }
                }
            }
        }
    }


    /// <summary>
    /// This method is to play sounds, when there is pushed on keys
    /// </summary>
    private void HandleKeys()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Add(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Add(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Add(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Add(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Add(4);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Add(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Add(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            soundsAllowedToPlay = true;
        }

    }

    /// <summary>
    /// This method is called outside of this script, to make sounds play.
    /// </summary>
    public void PlaySounds()
    {
        soundsAllowedToPlay = true;
    }

    /// <summary>
    /// This method is to add sound clips to a list. If it is first time, there will not be a time saved. The param number is for what clip should actually be played. 
    /// This can be used with the keys only or the mouse only mode.
    /// </summary>
    /// <param name="number"></param>
    public void Add(int number)
    {

        if (firstTimePress == true)
        {
            timeBetweenClip.Add(timeBetween);
        }
        else
        {
            firstTimePress = true;
        }

        clipsQueue.Add(clips[number]);

        timeBetween = 0;

        soundSource.PlayOneShot(clips[number]);
    }



}
