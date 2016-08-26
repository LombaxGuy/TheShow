using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    [SerializeField]
    GameObject head;

    [SerializeField]
    Collider bottomCollider;

    [SerializeField]
    CapsuleCollider topCollider;

    #region Move fields
    [Header("- Movement Settings -")]
    [SerializeField]
    float speedMultiplier = 80f;
    [SerializeField]
    float sprintSpeedModifier = 2.0f;
    [SerializeField]
    float crouchSpeedModifier = 0.5f;
    [SerializeField]
    float inAirSpeedModifier = 0.2f;
    [SerializeField]
    float crouchHeight = 1.0f;

    [SerializeField]
    float maxMagnitude = 7f;

    Vector3 moveVector;
    Vector3 moveVelocity;
    #endregion

    #region Jump fields
    [SerializeField]
    float initialJumpVelocity = 2.0f;
    [SerializeField]
    float maxJumpAccelleration = .6f;
    [SerializeField]
    float maxJumpTime = 1.5f;

    float jumpStartTime;
    float timeSinceJump;
    float jumpAccelleration;

    Vector3 curJumpVelocity;
    #endregion

    #region Headbob Fields
    [Header("- Headbob Settings -")]
    [SerializeField]
    [Range(0.001f, 1.0f)]
    private float headbobDegree = 1.0f;

    [SerializeField]
    [Range(0.5f, 50f)]
    float transitionSpeed = 20f;

    [SerializeField]
    [Range(0.001f, 10.0f)]
    float bobSpeed = 3.5f;

    [SerializeField]
    [Range(0.001f, 10.0f)]
    float bobSpeedSprinting = 6.5f;

    [SerializeField]
    [Range(0.001f, 2f)]
    float bobAmount = 0.05f;

    [SerializeField]
    [Range(0.001f, 2f)]
    float bobAmountSprinting = 0.15f;

    Vector3 restPosition;
    Vector3 headPosition;

    float curBobAmount = 0f;
    float curBobSpeed = 0f;

    float bobTimer = Mathf.PI / 2;
    #endregion

    #region KeyPressed bools
    bool jumpKey;
    bool sprintKey;
    bool crouchKey;
    #endregion

    bool jumping = false;
    bool crouching = false;

    [SerializeField]
    bool grounded = false;

    bool locked = false;

    private float rayCastLength = 1.01f;
    private int numberOfRaycasts = 10;

    [SerializeField]
    private float maximumSlopeAngle = 35;

    #region Events and EventHandlers

    void OnEnable()
    {
        // Subscribes to events
        EventManager.OnPlayerDeath += OnPlayerDeath;
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        // Unsubscribes from events
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    private void OnPlayerDeath()
    {
        locked = true;

        animator.SetBool("animateHead", false);
    }

    private void OnPlayerRespawn()
    {
        locked = false;
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        //Sets the reference to the head position and the rest position.
        headPosition = head.transform.localPosition;
        restPosition = headPosition;

        animator.SetFloat("headbobDegree", headbobDegree);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded();

        if (!locked)
        {
            moveVector = Vector3.zero;
            jumpKey = false;
            crouchKey = false;
            sprintKey = false;

            if (Input.anyKey)
            {
                HandleInput();
            }

            if (IsGrounded())
                HeadBob();
        }
    }

    void FixedUpdate()
    {
        //Slows the velocity each frame if the player is grounded.
        //Counteracts the sliding resulting from AddForce.
        if (IsGrounded())
        {
            rigid.velocity *= 0.8f;
        }
        else
        {
            rigid.velocity = new Vector3(rigid.velocity.x * 0.98f, rigid.velocity.y, rigid.velocity.z * 0.98f);
        }

        StopSlideOnSlopes();

        if (!locked)
        {
            HandleMovement();
        }
    }

    /// <summary>
    /// Updates movement vector, and current keypresses ( Crouch, Sprint, Jump )
    /// </summary>
    void HandleInput()
    {
        if (Input.GetKey(KeyBindings.KeyMoveForward))
        {
            moveVector.z = 1.0f;
        }

        if (Input.GetKey(KeyBindings.KeyMoveBackward))
        {
            moveVector.z = -1.0f;
        }

        if (Input.GetKey(KeyBindings.KeyMoveRight))
        {
            moveVector.x = 1.0f;
        }

        if (Input.GetKey(KeyBindings.KeyMoveLeft))
        {
            moveVector.x = -1.0f;
        }

        //Normalizes the vector to avoid diagonal movement being faster than Vertical and Horizontal.
        moveVector.Normalize();

        if (Input.GetKey(KeyBindings.KeyMoveJump))
        {
            jumpKey = true;
        }

        if (Input.GetKey(KeyBindings.KeyMoveCrouch))
        {
            crouchKey = true;

            //Adds the crouchSpeedModifier to the move vector.
            moveVector *= crouchSpeedModifier;
        }

        if (Input.GetKey(KeyBindings.KeyMoveSprint) && !crouching && IsGrounded())
        {
            sprintKey = true;

            //Adds the sprintSpeedModifier to the move vector.
            moveVector *= sprintSpeedModifier;
        }
    }

    /// <summary>
    /// Moves the player based on the move vector.
    /// Contains jumping and crouching based on bools from HandleInput.
    /// </summary>
    void HandleMovement()
    {
        //If either W, A, S or D is held.
        if (moveVector != Vector3.zero)
        {
            moveVelocity = Vector3.zero;

            //Creates the new desired velocity based on the moveVector.
            moveVelocity.x = moveVector.x * speedMultiplier;
            moveVelocity.y = 0f;
            moveVelocity.z = moveVector.z * speedMultiplier;

            if (IsGrounded() && rigid.velocity.magnitude <= maxMagnitude)
                //If grounded, add the velocity.
                rigid.AddRelativeForce((moveVelocity * 500) * Time.deltaTime);

            else if (!IsGrounded() && rigid.velocity.magnitude <= maxMagnitude)
            {
                //If not grounded, add the velocity multiplied by the inAirSpeedModifier.
                rigid.AddRelativeForce(((moveVelocity * inAirSpeedModifier) * 500) * Time.deltaTime);
            }
        }

        if (!jumpKey && jumping && IsGrounded())
        {
            jumping = false;
        }

        //Called when first jumping, adds the initial jump velocity.
        if (jumpKey && IsGrounded() && !jumping)
        {
            curJumpVelocity = rigid.velocity;
            curJumpVelocity.y = initialJumpVelocity;
            rigid.velocity = curJumpVelocity;

            jumping = true;
            jumpStartTime = Time.time;
        }

        //Called when in the air, and the jumpkey is held.
        if (jumping && jumpKey)
        {
            //Calculates the time since the jump began.
            timeSinceJump = Time.time - jumpStartTime;

            //If we haven't yet "jumped" for the maximum amount of time.
            if (timeSinceJump < maxJumpTime)
            {
                //t is used to make the jump curve exponential instead of linear.
                float t = 1 - timeSinceJump / maxJumpTime;
                t = t * t;

                jumpAccelleration = maxJumpAccelleration * t;

                //Adds the jump Acceleration to the y velocity. 
                curJumpVelocity = rigid.velocity;
                curJumpVelocity.y += jumpAccelleration;

                rigid.velocity = curJumpVelocity;
            }
        }
        //If the crouch key is held, the player isn't already crouching.
        else if (crouchKey && !crouching)
        {
            StartCrouch();
        }
        //If the crouch key is no longer held, and the player is crouching.
        else if (!crouchKey && crouching && CanStandUp())
        {
            EndCrouch();
        }
    }

    /// <summary>
    /// Disables the upper capsule collider and moves the camera down.
    /// </summary>
    void StartCrouch()
    {
        crouching = true;

        animator.SetBool("crouched", true);

        // Disables the top collider so it won't collide with the environment.
        topCollider.enabled = false;
    }

    /// <summary>
    /// Enables the upper capsule collider and moves the camera back up.
    /// </summary>
    void EndCrouch()
    {
        crouching = false;

        animator.SetBool("crouched", false);

        // Enables the top collider so it won't collide with the environment.
        topCollider.enabled = true;
    }

    /// <summary>
    /// Moves an empty gameObject called the head, based on the moveVector.
    /// </summary>
    void HeadBob()
    {
        //If sprinting increase the amount of headbobbing.
        if (sprintKey)
        {
            animator.SetFloat("animationSpeed", 2.0f);
            //curBobAmount = bobAmountSprinting;
            //curBobSpeed = bobSpeedSprinting;
        }

        //If not sprinting, change the amount of headbobbing back to normal.
        if (!sprintKey)
        {
            animator.SetFloat("animationSpeed", 1.0f);
            //Used a lerp to make the transition from sprinting to walking more smooth.
            //curBobAmount = Mathf.Lerp(bobAmountSprinting, bobAmount, 0.2f);
            //curBobSpeed = Mathf.Lerp(bobSpeedSprinting, bobSpeed, 0.2f);
        }

        //If WASD is pressed.
        if (moveVector != Vector3.zero)
        {
            animator.SetBool("animateHead", true);
            ////Increased the progress of the current "bob" based on the bobSpeed.
            //bobTimer += curBobSpeed * Time.deltaTime;

            ////Calculates the new position of the camera based on the progress of the bob.
            //Vector3 newPosition = new Vector3(Mathf.Cos(bobTimer) * curBobAmount,
            //                                    Mathf.Lerp(headPosition.y, restPosition.y + Mathf.Abs((Mathf.Sin(bobTimer) * curBobAmount)), transitionSpeed * Time.deltaTime),
            //                                    restPosition.z);

            //headPosition = newPosition;
        }

        //If no movement keys are pressed.
        else
        {
            animator.SetBool("animateHead", false);
            //Reset the bob timer.
            //bobTimer = Mathf.PI / 2;

            ////Lerps the position back to the rest position.
            //Vector3 newPosition = new Vector3(Mathf.Lerp(headPosition.x, restPosition.x, transitionSpeed * Time.deltaTime),
            //                                  Mathf.Lerp(headPosition.y, restPosition.y, transitionSpeed * Time.deltaTime),
            //                                  Mathf.Lerp(headPosition.z, restPosition.z, transitionSpeed * Time.deltaTime));

            //headPosition = newPosition;
        }

        ////If the bobTimer is too large, reset it to restart the bob.
        //if (bobTimer > Mathf.PI * 2)
        //{
        //    bobTimer = 0;
        //}

        ////Set the localPosition of the head.
        //head.transform.localPosition = headPosition;
    }

    /// <summary>
    /// Returns a bool based on whether or not the player is currently on the ground.
    /// </summary>
    /// <returns>Returns true if the player is on the ground.</returns>
    private bool IsGrounded()
    {
        // For-loop used to create the specified number of raycasts
        for (int i = 0; i < numberOfRaycasts; i++)
        {
            Vector3 raycastPos;

            // Calculates the the values used to make the circle
            float step = (i * 1.0f) / numberOfRaycasts;
            float angle = step * Mathf.PI * 2;

            // Calculates the x and z values for the player
            float x = Mathf.Sin(angle) * (transform.lossyScale.y / 2 - 0.01f);
            float z = Mathf.Cos(angle) * (transform.lossyScale.y / 2 - 0.01f);

            // Creates the position of the raycast
            raycastPos = new Vector3(x, 0, z) + transform.position;

            Debug.DrawRay(raycastPos, -transform.up.normalized * rayCastLength, Color.red);

            RaycastHit hit;

            // Creates the raycast
            if (Physics.Raycast(raycastPos, -transform.up, out hit, rayCastLength))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) < maximumSlopeAngle)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Returns a bool based on whether or not the player is under something.
    /// </summary>
    /// <returns>Returns true if the player is not under something.</returns>
    private bool CanStandUp()
    {
        // For-loop used to create the specified number of raycasts
        for (int i = 0; i < numberOfRaycasts; i++)
        {
            Vector3 raycastPos;

            // Calculates the the values used to make the circle
            float step = (i * 1.0f) / numberOfRaycasts;
            float angle = step * Mathf.PI * 2;

            // Calculates the x and z values for the player
            float x = Mathf.Sin(angle) * (transform.lossyScale.y / 2 - 0.01f);
            float z = Mathf.Cos(angle) * (transform.lossyScale.y / 2 - 0.01f);

            // Creates the position of the raycast
            raycastPos = new Vector3(x, 0, z) + transform.position;

            Debug.DrawRay(raycastPos, transform.up * rayCastLength, Color.blue);

            RaycastHit hit;

            // Creates the raycast
            if (Physics.Raycast(raycastPos, transform.up, out hit, rayCastLength))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Disables gravity on the player when the player is not moving.
    /// </summary>
    private void StopSlideOnSlopes()
    {
        if (moveVector == Vector3.zero)
        {
            Debug.DrawRay(transform.position, -transform.up * transform.lossyScale.y * 2, Color.yellow);

            RaycastHit hit;

            // Creates the raycast
            if (Physics.Raycast(transform.position, -transform.up, out hit, transform.lossyScale.y * 2))
            {
                float currentSlope = Vector3.Angle(hit.normal, Vector3.up);

                if (currentSlope < maximumSlopeAngle && IsGrounded())
                {
                    rigid.useGravity = false;
                }
                else if (!IsGrounded())
                {
                    rigid.useGravity = true;
                }
            }
        }
        else if (!rigid.useGravity)
        {
            rigid.useGravity = true;
        }
    }
}