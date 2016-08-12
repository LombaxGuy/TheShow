using UnityEngine;
using System.Collections;

public enum GroundType { None, Dirt, Grass, Wood, Cloth, Concrete, Water, Metal }

public class FirstPersonController : MonoBehaviour
{
    [Header("- Camera Settings -")]
    // Serialized Fields
    [SerializeField]
    private float maxVerticalRotation = 0.6f;
    [SerializeField]
    private float minVerticalRotation = -0.6f;
    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float mouseSensitivity = 0;
    [SerializeField]
    private bool invertedMouseControls = false;

    // Non-Serialized Fields
    private Camera cam;

    [Header("- Movement Settings -")]
    // Serialized Fields
    [SerializeField]
    private float maxWalkSpeed = 3;
    [SerializeField]
    private float maxSprintSpeed = 6;
    [SerializeField]
    private float maxBackSpeed = 1.5f;
    [SerializeField]
    private float maxCrouchSpeed = 1.5f;
    [SerializeField]
    private float maxInAirSpeed = 1.5f;
    [SerializeField]
    private float inAirAcceleration = 5;
    [SerializeField]
    private float jumpHeight = 7;
    [SerializeField]
    private float crouchHeight = 0.6f;
    [SerializeField]
    [Range(0.001f, 2.0f)]
    private float headbobAmount = 1.0f;
    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private bool isLockedCamera;

    // Non-Serialized Fields
    private bool isCrouched = false;
    private Rigidbody body;
    private float defaultYScale = 1.0f;
    private float rayCastLength = 1.001f;
    private Animator headAnimator;

    // Use this for initialization
    private void Start()
    {
        //Used to lock player inputs, off by default
        isLocked = false;
        isLockedCamera = false;

        // Getting the Rigidbody component
        body = GetComponent<Rigidbody>();

        // Getting the Camera component
        cam = GetComponentInChildren<Camera>();

        // Getting the Animator component
        headAnimator = GetComponentInChildren<Animator>();

        // Locks the cursor and hides it
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Pause.GetPauseState())
        {
            if (isLocked)
            {
                headAnimator.SetBool("animateHead", false);
            }
            else
            {
                if (isLockedCamera)
                {

                }
                else
                    HandleMouseInput();
                HandleKeyboardInput();
            }
        }


    }

    /// <summary>
    /// The main input control function.
    /// </summary>
    private void HandleKeyboardInput()
    {
        #region Movement System
        bool currentlyOnGround = OnGround();

        if (!currentlyOnGround && isCrouched)
        {
            EndCrouch();
        }
        else if (Input.GetKeyUp(KeyBindings.KeyMoveCrouch) && !StandBlocked())
        {
            EndCrouch();
        }
        else if (isCrouched && !StandBlocked() && !Input.GetKey(KeyBindings.KeyMoveCrouch))
        {
            EndCrouch();
        }

        // If the player is on the ground
        if (currentlyOnGround)
        {
            if (Input.GetKeyDown(KeyBindings.KeyMoveCrouch))
            {
                StartCrouch();
            }

            // If the player is crouched
            if (isCrouched)
            {
                if (Input.GetKey(KeyBindings.KeyMoveForward))
                {
                    if (Input.GetKey(KeyBindings.KeyMoveLeft))
                    {
                        // Move forward + left
                        body.velocity = (transform.forward + (-transform.right)).normalized * maxCrouchSpeed;
                    }
                    else if (Input.GetKey(KeyBindings.KeyMoveRight))
                    {
                        // Move forward + right
                        body.velocity = (transform.forward + transform.right).normalized * maxCrouchSpeed;
                    }
                    else
                    {
                        // Move forward
                        body.velocity = transform.forward.normalized * maxCrouchSpeed;
                    }

                    headAnimator.SetBool("animateHead", true);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveBackward)) // Backward Movement
                {
                    if (Input.GetKey(KeyBindings.KeyMoveLeft))
                    {
                        // Move back + left
                        body.velocity = (-transform.forward + (-transform.right)).normalized * maxCrouchSpeed;
                    }
                    else if (Input.GetKey(KeyBindings.KeyMoveRight))
                    {
                        // Move back + right
                        body.velocity = (-transform.forward + transform.right).normalized * maxCrouchSpeed;
                    }
                    else
                    {
                        // Move back
                        body.velocity = -transform.forward.normalized * maxCrouchSpeed;
                    }

                    headAnimator.SetBool("animateHead", true);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveLeft)) // Left Movement
                {
                    // Move left
                    body.velocity = -transform.right.normalized * maxCrouchSpeed;

                    headAnimator.SetBool("animateHead", true);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveRight)) // Right Movement
                {
                    // Move right
                    body.velocity = transform.right.normalized * maxCrouchSpeed;

                    headAnimator.SetBool("animateHead", true);
                }
                else
                {
                    headAnimator.SetBool("animateHead", false);
                }

            }
            // If the player is standing up
            else
            {
                if (Input.GetKey(KeyBindings.KeyMoveForward)) // Forward Movement
                {
                    if (Input.GetKey(KeyBindings.KeyMoveSprint))
                    {
                        if (Input.GetKey(KeyBindings.KeyMoveLeft))
                        {
                            // Sprint left
                            body.velocity = (transform.forward + (-transform.right)).normalized * maxSprintSpeed;
                        }
                        else if (Input.GetKey(KeyBindings.KeyMoveRight))
                        {
                            // Sprint right
                            body.velocity = (transform.forward + transform.right).normalized * maxSprintSpeed;
                        }
                        else
                        {
                            // Sprint forward
                            body.velocity = transform.forward.normalized * maxSprintSpeed;
                        }

                        headAnimator.SetFloat("animationSpeed", maxSprintSpeed / maxWalkSpeed);
                        headAnimator.SetFloat("headbobDegree", headbobAmount);
                        headAnimator.SetBool("animateHead", true);
                    }
                    else
                    {
                        if (Input.GetKey(KeyBindings.KeyMoveLeft))
                        {
                            // Move forward + left
                            body.velocity = (transform.forward + (-transform.right)).normalized * maxWalkSpeed;
                        }
                        else if (Input.GetKey(KeyBindings.KeyMoveRight))
                        {
                            // Move forward + right
                            body.velocity = (transform.forward + transform.right).normalized * maxWalkSpeed;
                        }
                        else
                        {
                            // Move forward
                            body.velocity = transform.forward.normalized * maxWalkSpeed;                       
                        }

                        headAnimator.SetFloat("animationSpeed", 1);
                        headAnimator.SetFloat("headbobDegree", headbobAmount);
                        headAnimator.SetBool("animateHead", true);
                    }
                }
                else if (Input.GetKey(KeyBindings.KeyMoveBackward)) // Backward Movement
                {
                    if (Input.GetKey(KeyBindings.KeyMoveLeft))
                    {
                        // Move back + left
                        body.velocity = (-transform.forward + (-transform.right)).normalized * maxWalkSpeed;
                    }
                    else if (Input.GetKey(KeyBindings.KeyMoveRight))
                    {
                        // Move back + right
                        body.velocity = (-transform.forward + transform.right).normalized * maxWalkSpeed;
                    }
                    else
                    {
                        // Move back
                        body.velocity = -transform.forward.normalized * maxWalkSpeed;
                    }

                    headAnimator.SetFloat("animationSpeed", 1);
                    headAnimator.SetFloat("headbobDegree", headbobAmount);
                    headAnimator.SetBool("animateHead", true);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveLeft)) // Left Movement
                {
                    // Move left
                    body.velocity = -transform.right.normalized * maxWalkSpeed;

                    headAnimator.SetFloat("animationSpeed", 1);
                    headAnimator.SetFloat("headbobDegree", headbobAmount);
                    headAnimator.SetBool("animateHead", true);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveRight)) // Right Movement
                {
                    // Move right
                    body.velocity = transform.right.normalized * maxWalkSpeed;

                    headAnimator.SetFloat("animationSpeed", 1);
                    headAnimator.SetFloat("headbobDegree", headbobAmount);
                    headAnimator.SetBool("animateHead", true);
                }
                else
                {
                    headAnimator.SetBool("animateHead", false);
                }

                // Player is jumping
                if (Input.GetKeyDown(KeyBindings.KeyMoveJump))
                {
                    body.velocity += transform.up * jumpHeight;
                    headAnimator.SetBool("animateHead", false);
                }
            }
        }
        // If the player is in the air
        else
        {
            if (Input.GetKey(KeyBindings.KeyMoveForward))
            {
                if (Input.GetKey(KeyBindings.KeyMoveLeft))
                {
                    // Move forward + left
                    MoveInAir(transform.forward + -transform.right, maxInAirSpeed);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveRight))
                {
                    // Move forward + right
                    MoveInAir(transform.forward + transform.right, maxInAirSpeed);
                }
                else
                {
                    // Move forward
                    MoveInAir(transform.forward, maxInAirSpeed);
                }
            }
            else if (Input.GetKey(KeyBindings.KeyMoveBackward)) // Backward Movement
            {
                if (Input.GetKey(KeyBindings.KeyMoveLeft))
                {
                    // Move back + left
                    MoveInAir(-transform.forward + -transform.right, maxInAirSpeed);
                }
                else if (Input.GetKey(KeyBindings.KeyMoveRight))
                {
                    // Move back + right
                    MoveInAir(-transform.forward + transform.right, maxInAirSpeed);
                }
                else
                {
                    // Move back
                    MoveInAir(-transform.forward, maxInAirSpeed);
                }
            }
            else if (Input.GetKey(KeyBindings.KeyMoveLeft)) // Left Movement
            {
                // Move left
                MoveInAir(-transform.right, maxInAirSpeed);
            }
            else if (Input.GetKey(KeyBindings.KeyMoveRight)) // Right Movement
            {
                // Move right
                MoveInAir(transform.right, maxInAirSpeed);
            }
        }
        #endregion
    }

    /// <summary>
    /// Rotates the camera based on mouse input. Also rotates the players transform to match the rotation of the camera. 
    /// </summary>
    private void HandleMouseInput()
    {
        //Getting mouse axis. "horizontal" is for rotation the player transform. "vertical" is for rotating the camera.
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        // Horizontal rotation of the player transform. Checks if the controls are inverted or not
        if (invertedMouseControls)
        {
            this.transform.Rotate(0, mouseSensitivity * (-horizontal), 0);
        }
        else
        {
            this.transform.Rotate(0, mouseSensitivity * horizontal, 0);
        }

        // Checks if the camera is rotated down. Rotates the camera down if it is within two float variables. Checks if the controls are inverted or not
        if (vertical > 0)
        {
            if (invertedMouseControls)
            {
                if (cam.transform.localRotation.x < maxVerticalRotation)
                {
                    cam.transform.Rotate(mouseSensitivity * vertical, 0, 0);
                }
            }
            else
            {
                if (cam.transform.localRotation.x > minVerticalRotation)
                {
                    cam.transform.Rotate(mouseSensitivity * (-vertical), 0, 0);
                }
            }
        }
        // If the camera is not rotated down check if it is rotated up. Rotates the camera up if it is within two float variables. Checks if the controls are inverted or not
        else if (vertical < 0)
        {
            if (invertedMouseControls)
            {
                if (cam.transform.localRotation.x > minVerticalRotation)
                {
                    cam.transform.Rotate(mouseSensitivity * vertical, 0, 0);
                }
            }
            else
            {
                if (cam.transform.localRotation.x < maxVerticalRotation)
                {
                    cam.transform.Rotate(mouseSensitivity * (-vertical), 0, 0);
                }
            }
        }
    }

    /// <summary>
    /// Reduces the scale on the y axis of the player and sets isCrouched to true.
    /// </summary>
    private void StartCrouch()
    {
        if (!isCrouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - (defaultYScale - transform.localScale.y), transform.position.z);
            isCrouched = true;

            headAnimator.SetFloat("animationSpeed", 0.5f);
            headAnimator.SetFloat("headbobDegree", headbobAmount / 2);
        }
    }

    /// <summary>
    /// Sets the players scale on the y axis back to normal and sets isCrouched to false.
    /// </summary>
    private void EndCrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, defaultYScale, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + (defaultYScale - transform.localScale.y), transform.position.z);
        isCrouched = false;

        headAnimator.SetFloat("animationSpeed", 1);
        headAnimator.SetFloat("headbobDegree", headbobAmount);
    }

    /// <summary>
    /// Moves the player in a direction with a specified speed and acceleration. Used for movement while the player is airborn.
    /// </summary>
    /// <param name="direction">The direction the player is moved</param>
    /// <param name="maxSpeed">The maximum speed the player can move in the air</param>
    private void MoveInAir(Vector3 direction, float maxSpeed)
    {
        Vector3 horizontalVelocity = new Vector3(body.velocity.x, 0, body.velocity.z);

        if (horizontalVelocity.magnitude < maxSpeed)
        {
            body.velocity += direction.normalized * inAirAcceleration * Time.deltaTime;
        }
    }

    /// <summary>
    /// Locking input for the player. use this if you wat to shut off the players actions
    /// </summary>
    /// <param name="state">true or false false if you want to allow movement</param>
    public void LockControls(bool state)
    {
        isLocked = state;
    }
    /// <summary>
    /// For Locking the camera only, used together with timescale pausing
    /// </summary>
    /// <param name="state"></param>
    public void LockCamera(bool state)
    {
        isLockedCamera = state;
    }

    /// <summary>
    /// Casts 4 rays downwards from the player to check if the player is standing on something. Returns true if the player stands on something.
    /// </summary>
    /// <returns></returns>
    private bool OnGround()
    {
        Vector3[] startPositions = new Vector3[4]
        {
                new Vector3(transform.position.x - (transform.localScale.x / 3), transform.position.y + (defaultYScale - transform.localScale.y), transform.position.z - (transform.localScale.z / 3)),
                new Vector3(transform.position.x - (transform.localScale.x / 3), transform.position.y + (defaultYScale - transform.localScale.y), transform.position.z + (transform.localScale.z / 3)),
                new Vector3(transform.position.x + (transform.localScale.x / 3), transform.position.y + (defaultYScale - transform.localScale.y), transform.position.z - (transform.localScale.z / 3)),
                new Vector3(transform.position.x + (transform.localScale.x / 3), transform.position.y + (defaultYScale - transform.localScale.y), transform.position.z + (transform.localScale.z / 3))
        };

        for (int i = 0; i < startPositions.Length; i++)
        {
            Debug.DrawRay(startPositions[i], -transform.up.normalized * rayCastLength, Color.red);

            if (Physics.Raycast(startPositions[i], -transform.up, rayCastLength))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Casts 4 rays upwards from the player to check if the player can stand up. Returns true if the player cannot stand up.
    /// </summary>
    /// <returns></returns>
    private bool StandBlocked()
    {
        if (isCrouched)
        {
            Vector3[] startPositions = new Vector3[4]
            {
                new Vector3(transform.position.x - (transform.localScale.x / 3), transform.position.y + (defaultYScale - crouchHeight), transform.position.z - (transform.localScale.z / 3)),
                new Vector3(transform.position.x - (transform.localScale.x / 3), transform.position.y + (defaultYScale - crouchHeight), transform.position.z + (transform.localScale.z / 3)),
                new Vector3(transform.position.x + (transform.localScale.x / 3), transform.position.y + (defaultYScale - crouchHeight), transform.position.z - (transform.localScale.z / 3)),
                new Vector3(transform.position.x + (transform.localScale.x / 3), transform.position.y + (defaultYScale - crouchHeight), transform.position.z + (transform.localScale.z / 3))
            };

            for (int i = 0; i < startPositions.Length; i++)
            {
                Debug.DrawRay(startPositions[i], transform.up.normalized * rayCastLength, Color.yellow);

                if (Physics.Raycast(startPositions[i], transform.up, rayCastLength))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
