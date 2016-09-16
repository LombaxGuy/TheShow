﻿using UnityEngine;
using System.Collections;

public class ToolTipTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject manager;

    [SerializeField]
    private string messege;

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("WorldManager");
	
	}

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if(player.parent != null)
        if(player.parent.tag == "Player")
        {
            
            IntroSequence.lightEntered = true;
            IntroSequence.inLight = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

            if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                IntroSequence.inLight = false;
            }
    }
}
