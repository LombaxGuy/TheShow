using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class Node2Behaviour : MonoBehaviour {

    // The script containing the delegate
    InteractableObjectComponent interactableObjectComponent;

    // Use this for initialization
    void Start()
    {
        // Get the script
        interactableObjectComponent = GetComponent<InteractableObjectComponent>();

        // If the component exists on the object we assign a behaviour to the delegate in the script component.
        if (interactableObjectComponent != null)
        {
            interactableObjectComponent.behaviourDelegate = ThisSpecificBehaviour;
            Debug.Log("Node assigned to the BehaviourDelegate.");
        }
        else
        {
            Debug.Log("No 'InteractableObjectComponent' was found!");
        }
    }

    /// <summary>
    /// Defines the behaviour of the object when it is interacted with.
    /// </summary>
    private void ThisSpecificBehaviour()
    {
        transform.parent.GetComponent<MusicRecord>().Add(1);
        Debug.Log("Node touched 2");
    }
}
