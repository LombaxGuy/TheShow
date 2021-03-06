﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Pusher : MonoBehaviour {   

    //Used to check which way the pusher is moving
    private bool isOut = false;
    private bool isMoving = false;

    //Used to tweak behaviour on the pusher
    [SerializeField]
    [Tooltip("How far this extends")]
    private float distance = 5;
    [SerializeField]
    [Tooltip("How fast this extends")]
    private float pushOutTime = 0.2f;
    [SerializeField]
    [Tooltip("How fast this extends")]
    private float pushInTime = 1f;
    [SerializeField]
    [Tooltip("How long in seconds until the script starts running")]
    private float offsetSeconds = 0;
    [SerializeField]
    [Tooltip("How long this stays in")]
    private float idleInSeconds = 2.2f;
    [SerializeField]
    [Tooltip("How long this stays out")]
    private float idleOutSeconds = 1;

    //Timers that are used for delays
    private float offsetTimer = 0;
    private float idleTimer = 0;

    //This is the box collider child for the knockback effect
    private BoxCollider stampTrigger;

    //possition when script runs first
    private Vector3 startPos;
    //extended possition
    private Vector3 extPos;

    private AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("AudioClips. 0 = out, 1 = in")]
    private AudioClip[] audioClips;

    // Use this for initialization
    /// <summary>
    /// Used to set the vectors and our trigger collider.
    /// </summary>
    void Start () {

        stampTrigger = transform.GetChild(0).GetComponent<BoxCollider>();
        startPos = transform.position;
        extPos = transform.position + transform.forward * distance;
        audioPlayer = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
    /// <summary>
    /// Checking the state this object is in and handling the timers.
    /// Starting the coroutine.
    /// </summary>
	void Update () {
        if(!isMoving)
        {
            stampTrigger.enabled = false;
            if (offsetTimer < offsetSeconds)
            {
                
                offsetTimer += Time.deltaTime;
            }
            else
            {
                idleTimer += Time.deltaTime;
            }
        }   


        if(isOut)
        {
            if(idleTimer > idleOutSeconds)
            {
                Move(extPos, startPos, pushInTime);
                stampTrigger.enabled = false;
                isOut = false;
                idleTimer = 0;
                audioPlayer.PlayOneShot(audioClips[1]);
            }

        }
        else
        {
            if (idleTimer > idleInSeconds)
            {
                Move(startPos, extPos, pushOutTime);
                stampTrigger.enabled = true;
                isOut = true;
                idleTimer = 0;
                audioPlayer.PlayOneShot(audioClips[0]);
            }
        }

        
        
    }

    /// <summary>
    /// Coroutine for the movement of the object
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    /// <returns></returns>
    private IEnumerator CoroutineMove(Vector3 from, Vector3 to, float timeInSeconds)
    {
        isMoving = true;
        float t = 0;

        while(t < 1)
        {
            transform.position = Vector3.Lerp(from, to, t);

            t += Time.deltaTime / timeInSeconds;

            yield return null;
        }


        transform.position = Vector3.Lerp(from, to, t);
        isMoving = false;
    }

    /// <summary>
    /// This is simply used to call the coroutine.
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    private void Move( Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

    }
}
