﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class ButtonPlatformEtender : MonoBehaviour {

    InteractableObjectComponent interactableObjectComponent;
    private bool isOut = false;
    private float extendTimer = 0;
    [SerializeField]
    private float duration = 30;

    [SerializeField]
    private GameObject targetObject;

    private void OnEnable()
    {
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }
    // Use this for initialization
    void Start () {
        interactableObjectComponent = GetComponent<InteractableObjectComponent>();

        // If the component exists on the object we assign a behaviour to the delegate in the script component.
        if (interactableObjectComponent != null)
        {
            interactableObjectComponent.behaviourDelegate = ThisSpecificBehaviour;
            //Debug.Log("Behaviour assigned to the BehaviourDelegate.");
        }
        else
        {
            Debug.Log("TemplateBehaviourComponent.cs: No 'InteractableObjectComponent' was found!");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        extendTimer += Time.deltaTime;
        if (extendTimer > duration)
        {
            targetObject.GetComponent<PlatformExtender>().Substract();
            isOut = false;
            EventManager.RaiseOnTimerExpired();
            extendTimer = 0;
        }
	}

    private void ThisSpecificBehaviour()
    {

        if(!isOut)
        {
            targetObject.GetComponent<PlatformExtender>().Extend();
            isOut = true;
        }

        EventManager.RaiseOnButtonPressed();

    }

    private void OnPlayerRespawn()
    {
        extendTimer = duration + 1;
    }
}
