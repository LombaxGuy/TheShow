using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class TemplateBehaviourComponent : MonoBehaviour
{
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
            //Debug.Log("Behaviour assigned to the BehaviourDelegate.");
        }
        else
        {
            Debug.Log("TemplateBehaviourComponent.cs: No 'InteractableObjectComponent' was found!");
        }
    }

    /// <summary>
    /// Defines the behaviour of the object when it is interacted with.
    /// </summary>
    private void ThisSpecificBehaviour()
    {
        Debug.Log("This is a behaviour specific to the object the script is attached to.");
    }
}