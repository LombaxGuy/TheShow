using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class DoorButton : MonoBehaviour
{
    InteractableObjectComponent interactableObjectComponent;

    private bool isOpen = false;


    [SerializeField]
    private GameObject targetObject;
    // Use this for initialization
    void Start()
    {
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
    void Update()
    {

    }

    private void ThisSpecificBehaviour()
    {
        if (!isOpen)
        {
            targetObject.GetComponentInChildren<DoorBehaviourComponent>().LockDoor(false);
            isOpen = true;
        }
    }

}
