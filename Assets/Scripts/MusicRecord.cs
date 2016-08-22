using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicRecord : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private List<AudioClip> clipsQueue;
    [SerializeField]
    private List<float> timeBetweenClip;

    private AudioSource audio;

    private float timeBetween;

    private float timeBetweenPlay;

    private int whatToPlay;

    private bool firstTimePress = false;

    private bool test = false;

    private float cd;

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
        audio = GetComponent<AudioSource>();
        audio.loop = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerClicked == true)
        {


            //cd -= Time.deltaTime;
            timeBetween += Time.deltaTime;
            timeBetweenPlay -= Time.deltaTime;

            HandleKeys();

            if (test == true)
            {

                if (timeBetweenPlay <= 0)
                {
                    timeBetweenPlay = timeBetweenClip[whatToPlay];
                    audio.PlayOneShot(clipsQueue[whatToPlay]);
                    whatToPlay++;
                    if (whatToPlay >= clipsQueue.Count - 1)
                    {
                        whatToPlay = 0;
                    }

                }


            }
        }

    }

    private void HandleKeys()
    {
        //if (cd <= 0)
        //{
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

        //  cd = 0.2f;
        //}

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            test = true;
            timeBetweenPlay = 0;
            whatToPlay = 0;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<MusicBehaviour>().GetOut();
        }
    }

    private void Add(int number)
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

        audio.PlayOneShot(clips[number]);
    }


}
