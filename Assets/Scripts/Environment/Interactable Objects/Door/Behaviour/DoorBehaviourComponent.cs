using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class DoorBehaviourComponent : MonoBehaviour
{

    // The script containing the delegate
    InteractableObjectComponent interactableObjectComponent;

    [SerializeField]
    [Range(0f, 50000f)]
    // The force applied to the door when opened or closed
    private float doorOpenForce = 10000;

    [SerializeField]
    // The angle at which the force directions are changed
    private float openOrCloseDoorAngle = 95;

    [SerializeField]
    // Interaction cooldown in seconds
    private float interactionCooldown = 0.5f;
    
    private float yRotation;
    [SerializeField]
    private bool onCooldown = false;

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

        // If interaction is not on cooldown
        if (!onCooldown)
        {
            // Opens the door
            if (yRotation < openOrCloseDoorAngle)
            {
                GetComponent<Rigidbody>().AddForce(-transform.forward * doorOpenForce);
                StartCoroutine(StartDoorCooldown());
                //Debug.Log("Open door");
            }
            // Closes the door
            else
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * doorOpenForce);
                StartCoroutine(StartDoorCooldown());
                //Debug.Log("Close door");
            }
        }
    }

    private IEnumerator StartDoorCooldown()
    {
        onCooldown = true;

        yield return new WaitForSeconds(interactionCooldown);

        onCooldown = false;
    }
}
