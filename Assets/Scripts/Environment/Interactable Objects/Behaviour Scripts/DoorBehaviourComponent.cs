using UnityEngine;
using System.Collections;

public class DoorBehaviourComponent : MonoBehaviour
{

    // The script containing the delegate
    InteractableObjectComponent interactableObjectComponent;

    [SerializeField]
    [Range(0f, 50000f)]
    private float doorOpenForce = 10000;

    [SerializeField]
    private float openOrCloseDoorAngle = 95;
    
    private float yRotation;

    // Use this for initialization
    void Start()
    {
        // Get the script
        interactableObjectComponent = GetComponent<InteractableObjectComponent>();

        // If the component exists on the object we assign a behaviour to the delegate in the script component.
        if (interactableObjectComponent != null)
        {
            interactableObjectComponent.behaviourDelegate = ThisSpecificBehaviour;
            Debug.Log("Behaviour assigned to the BehaviourDelegate.");
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
        // Set the yRotation to the rotation of the door
        yRotation = transform.localEulerAngles.y;

        // Opens the door
        if (yRotation < openOrCloseDoorAngle)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * doorOpenForce);
            Debug.Log("Open door");
        }
        else // Closes the door
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * doorOpenForce);
            Debug.Log("Close door");
        }

    }
}
