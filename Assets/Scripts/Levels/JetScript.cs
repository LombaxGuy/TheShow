using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class JetScript : MonoBehaviour
{

    // The script containing the delegate
    InteractableObjectComponent interactableObjectComponent;

    private float timeToTakeOff;
    private float thrust;
    private bool go = false;

    [SerializeField]
    [Tooltip("Countdown timer")]
    private float timeBeforeTakeOff;

    [SerializeField]
    [Tooltip("Start Thrust when takeoff")]
    private float startThrust;

    [SerializeField]
    [Tooltip("Upper thrust limit")]
    private float maxThrust;

    [SerializeField]
    [Tooltip("Thrust accelaration")]
    private float thrustPerUpdate;

    [SerializeField]
    private Rigidbody rb;
    

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

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToTakeOff -= Time.deltaTime;
        if (timeToTakeOff < 0)
        {
            thrust = Mathf.MoveTowards(thrust, maxThrust, thrustPerUpdate);
            if (thrust < maxThrust && go)
            {
                rb.AddForce(transform.up * thrust);
            }
            if (thrust >= maxThrust)
            {
                go = false;
                thrust = 0;
            } 
        }
    }
    /// <summary>
    /// Defines the behaviour of the object when it is interacted with.
    /// </summary>
    private void ThisSpecificBehaviour()
    {
        timeToTakeOff = timeBeforeTakeOff;
        thrust = startThrust;
        go = true;
    }
}

