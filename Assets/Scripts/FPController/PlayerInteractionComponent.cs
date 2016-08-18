using UnityEngine;
using System.Collections;

public class PlayerInteractionComponent : MonoBehaviour
{
    [SerializeField]
    private float force = 1500;
    [SerializeField]
    private float throwForce = 15;
    [SerializeField]
    private float interactionDistance = 2;

    private string nameOfPickUpLayer = "PickUp";
    private string nameOfInteractableLayer = "Interactable";

    private float damping = 6;
    private Transform jointTransform;
    private bool isCurrentlyCarring = false;

    private Quaternion rotationLastFrame;
    private Camera playerCamera;
    private Ray viewRay;
    private RaycastHit oldHit;

    // Use this for initialization
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        viewRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
    }

    /// <summary>
    /// Initiates the drag of an object. Calls the AttachJoint function and sets isCurrentlyCarring to true.
    /// </summary>
    public void DragBegin(RaycastHit hit)
    {
        jointTransform = AttachJoint(hit.rigidbody, hit.transform.position);
        rotationLastFrame = transform.rotation;
        isCurrentlyCarring = true;
    }

    /// <summary>
    /// Should be called every frame an object is carried except the first and the last frame.
    /// </summary>
    public void Dragging()
    {
        // If there is no jointTransform return
        if (jointTransform == null)
        {
            return;
        }

        RaycastHit hit;

        // If vision gets blocked of the object or if the object gets pushed out of the center of the screen the object is dropped.
        if (Physics.Raycast(viewRay, out hit, interactionDistance))
        {
            if (oldHit.transform != hit.transform)
            {
                DragEnd();
            }
        }

        // Sets the position of the joint to the end of a vector. The vector goes through the camera and forward 
        jointTransform.position = playerCamera.transform.position + playerCamera.transform.forward * interactionDistance;

        // Rotate Object - Start
        float newAngle = Quaternion.Angle(transform.rotation, rotationLastFrame);

        if (rotationLastFrame.eulerAngles.y > transform.eulerAngles.y)
        {
            newAngle = -newAngle;
        }

        jointTransform.Rotate(Vector3.up, newAngle);

        rotationLastFrame = transform.rotation;
        // Rotate Object - End
    }

    /// <summary>
    /// Removes the joint and sets isCurrentlyCarring to false
    /// </summary>
    public void DragEnd()
    {
        if (jointTransform == null)
        {
            return;
        }

        Destroy(jointTransform.gameObject);
        isCurrentlyCarring = false;
    }

    /// <summary>
    /// Adds a force impulse to an object in a given direction
    /// </summary>
    /// <param name="throwForce">The force that should be applied.</param>
    /// <param name="rigidbody">The rigidbody the force should be applied to.</param>
    /// <param name="direction">The direction of the force.</param>
    public void ThrowObject(float throwForce, Rigidbody rigidbody, Vector3 direction)
    {
        DragEnd();
        rigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Creates a empty gameobject, adds configuarable joint to it and connects it to a rigidbody
    /// </summary>
    /// <param name="rigidbody">The rigidbody of the object that is picked up.</param>
    /// <param name="position">The position on the object where the joint is attached.</param>
    /// <returns>Returns the transform of the gameobject that was created.</returns>
    private Transform AttachJoint(Rigidbody rigidbody, Vector3 position)
    {
        GameObject obj = new GameObject("Attachment Point");
        obj.hideFlags = HideFlags.HideInHierarchy;
        obj.transform.position = position;

        Rigidbody newRigidbody = obj.AddComponent<Rigidbody>();
        newRigidbody.isKinematic = true;

        ConfigurableJoint joint = obj.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rigidbody;
        joint.configuredInWorldSpace = true;
        joint.xDrive = NewJointDrive(force, damping);
        joint.yDrive = NewJointDrive(force, damping);
        joint.zDrive = NewJointDrive(force, damping);
        joint.slerpDrive = NewJointDrive(force, damping);
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        return obj.transform;
    }

    /// <summary>
    /// Creates a joint drive with a specified spring force and damping
    /// </summary>
    /// <param name="force">The force of the "spring".</param>
    /// <param name="damping">Damping (does not seem to affect anything).</param>
    /// <returns>A joint drive with the specified values.</returns>
    private JointDrive NewJointDrive(float force, float damping)
    {
        JointDrive drive = new JointDrive();
        drive.mode = JointDriveMode.Position;
        drive.positionSpring = force;
        drive.positionDamper = damping;
        drive.maximumForce = Mathf.Infinity;
        return drive;
    }

    /// <summary>
    /// Handles input
    /// </summary>
    private void HandleInput()
    {
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.C))
        {
                   //delete plz
            SubtitleControl.Instance.StartSub("sub1", 5);
            //end of delete

        }

        if (Input.GetKeyDown(KeyBindings.KeyInteraction))
        {
            //delete plz
            SubtitleControl.Instance.StartSub("sub2", 5);
            //end of delete
            if (Physics.Raycast(viewRay, out hit, interactionDistance))
            {
                // If the hit object is an interactable object, interact with it
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(nameOfInteractableLayer))
                {
                    hit.transform.GetComponent<InteractableObjectComponent>().InteractWithObject();
                }

                // If the hit object is an object that can be picked up, pick it up
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer(nameOfPickUpLayer))
                {
                    // If no object is currently being carried pick up the object
                    if (!isCurrentlyCarring)
                    {
                        oldHit = hit;

                        DragBegin(hit);
                        Debug.Log("Picked up object.");
                    }
                    // If an object is currently being carried drop it
                    else
                    {
                        DragEnd();
                        Debug.Log("Dropped object.");
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyBindings.KeyPrimaryAction))
        {
            if (Physics.Raycast(viewRay, out hit, interactionDistance))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(nameOfPickUpLayer))
                {
                    ThrowObject(throwForce, hit.rigidbody, playerCamera.transform.forward);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        viewRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        HandleInput();

        if (isCurrentlyCarring)
        {
            Dragging();
            Debug.Log("Carring object...");
        }
    }
}