using UnityEngine;
using System.Collections;

public class DoorManagerComponent : MonoBehaviour
{
    [SerializeField]
    // The angle at which the door automatically closes
    private float doorAutoCloseAngle = 10.0f;

    [SerializeField]
    // The amount of force the door tries to close with
    private float doorSpringValue = 200f;

    [SerializeField]
    // The bouncines of the door
    private float doorBouncinessValue = 0.3f;
    
    private float yRotation;
    private HingeJoint hinge;

    // Use this for initialization
    void Start ()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        yRotation = transform.localEulerAngles.y;
        //yRotation = transform.rotation.eulerAngles.y;
        //Debug.Log(yRotation);

        AutoCloseDoor();
    }

    /// <summary>
    /// Method that checks on the doors rotation and closes the door if the open angle is less than the 'doorAutoCloseAngle'. Also makes sure to set the spring and bounciness of the door.
    /// </summary>
    private void AutoCloseDoor()
    {
        // Creating instances of the joints we want to change values in
        JointSpring jointSpring = hinge.spring;
        JointLimits jointLimit = hinge.limits;

        // If the door is not open (the open angle is less than 'doorAutoCloseAngle')
        if (!IsDoorOpen())
        {   
            // Set the spring value. Makes the door automatically close.
            jointSpring.spring = doorSpringValue;

            // Set the bounciness to 0 so the door does not bounce open after being closed
            jointLimit.bounciness = 0;

            // Assign the above values to our HingeJoint
            hinge.limits = jointLimit;
            hinge.spring = jointSpring;
        }
        else
        {
            // Set the spring value to 0 so the door does not close automatically
            jointSpring.spring = 0;

            // Set the bounciness so the door bounces off the wall
            jointLimit.bounciness = doorBouncinessValue;

            // Assign the above values to our HingeJoint
            hinge.limits = jointLimit;
            hinge.spring = jointSpring;
        }
    }

    /// <summary>
    /// Determins if the door is open or closed.
    /// </summary>
    /// <returns>Retuns true if the door is open and false if it's closed</returns>
    private bool IsDoorOpen()
    {
        bool isDoorOpen = false;

        // Checks if the angle of the door is larger than 'doorAutoCloseAngle'. If it is the door is considered open.
        if (yRotation > doorAutoCloseAngle)
        {
            isDoorOpen = true;
        }

        return isDoorOpen;
    }
}
