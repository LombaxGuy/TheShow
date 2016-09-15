﻿using UnityEngine;
using System.Collections;

public class ButtonRoomTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                IntroSequence.buttonPressed = true;
                gameObject.SetActive(false);

            }

    }
}
