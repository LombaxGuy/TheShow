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
        //cd -= Time.deltaTime;
        timeBetween += Time.deltaTime;
        timeBetweenPlay -= Time.deltaTime;

        //if (cd <= 0)
        //{
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[0]);

                timeBetween = 0;

                audio.PlayOneShot(clips[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[1]);

                timeBetween = 0;


                audio.PlayOneShot(clips[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[2]);

                timeBetween = 0;

                audio.PlayOneShot(clips[2]);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[3]);

                timeBetween = 0;


                audio.PlayOneShot(clips[3]);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[4]);

                timeBetween = 0;


                audio.PlayOneShot(clips[4]);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[5]);

                timeBetween = 0;


                audio.PlayOneShot(clips[5]);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {

                if (firstTimePress == true)
                {
                    timeBetweenClip.Add(timeBetween);
                }
                else
                {
                    firstTimePress = true;
                }

                clipsQueue.Add(clips[6]);

                timeBetween = 0;


                audio.PlayOneShot(clips[6]);
            }

          //  cd = 0.2f;
        //}

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            test = true;
            timeBetweenPlay = 0;
            whatToPlay = 0;
        }



        if(test == true)
        {

            if(timeBetweenPlay <= 0)
            {
                timeBetweenPlay = timeBetweenClip[whatToPlay];
                audio.PlayOneShot(clipsQueue[whatToPlay]);
                whatToPlay++;
                if(whatToPlay >= clipsQueue.Count - 1)
                {
                    whatToPlay = 0;
                }

            }


        }

    }
}
