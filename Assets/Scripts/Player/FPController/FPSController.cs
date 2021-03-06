﻿using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField]
    Animator animatorHead;

    [SerializeField]
    [Tooltip("The top collider in the player object.")]
    CapsuleCollider topCollider;

    #region Move fields
    [Header("- Movement Settings -")]
    [SerializeField]
    [Tooltip("Overall scaling of the movespeed, regardless of sprinting, crouching or in air.")]
    float speedMultiplier = 80f;
    [SerializeField]
    [Tooltip("The speed is multiplied by this amount when sprinting.")]
    float sprintSpeedModifier = 2.0f;
    [SerializeField]
    [Tooltip("The speed is multiplied by this amount when crouching.")]
    float crouchSpeedModifier = 0.5f;
    [SerializeField]
    [Tooltip("The speed is multiplied by this amount when in the air.")]
    float inAirSpeedModifier = 0.2f;

    [SerializeField]
    [Tooltip("The players velocity is not increased further if it's magnitude >= this amount.")]
    float maxMagnitude = 7f;

    [SerializeField]
    [Tooltip("The maximum angle of the slope the player can walk on without sliding.")]
    private float maximumSlopeAngle = 35;

    Vector3 moveVector;
    Vector3 moveVelocity;
    #endregion

    #region Jump fields
    [SerializeField]
    [Tooltip("The minimum velocity when jumping, regardless of how long the jump key is held.")]
    float initialJumpVelocity = 2.0f;
    [SerializeField]
    [Tooltip("The maximum acceleration of the players velocity when jumping.")]
    float maxJumpAccelleration = .6f;
    [SerializeField]
    [Tooltip("The maximum amount of time a jump can last.")]
    float maxJumpTime = 1.5f;

    float jumpStartTime;
    float timeSinceJump;
    float jumpAccelleration;


    Vector3 curJumpVelocity;
    #endregion

    #region Headbob Fields
    [Header("- Headbob Settings -")]
    [SerializeField]
    [Tooltip("The speed of bobbing when walking.")]
    [Range(0.001f, 1f)]
    float bobSpeed = 1f;

    [SerializeField]
    [Tooltip("The speed of bobbing when sprinting.")]
    [Range(0.001f, 1f)]
    float bobSpeedSprinting = 1.5f;

    [SerializeField]
    [Tooltip("The speed of bobbing when crouching.")]
    [Range(0.001f, 1f)]
    float bobSpeedCrouching = 0.5f;


    [SerializeField]
    [Tooltip("Amount used to scale the movements of the headbobbing animation.")]
    [Range(0.001f, 1f)]
    float headbobDegree = 1f;
    #endregion

    #region KeyPressed bools
    bool jumpKey;
    bool sprintKey;
    bool crouchKey;
    #endregion

    [SerializeField]
    private float maxFallDistance = 20;

    private float oldPlayerY = 0;
    public float fallHeigth = 0;

    private bool comparePos = false;

    bool jumping = false;
    bool crouching = false;

    bool stunned = false;
    bool locked = false;

    private float rayCastLength = 1.1f;
    private int numberOfRaycasts = 10;

    [SerializeField]
    private bool onGround = true;

    public bool OnGround
    {
        get { return onGround; }
    }

    public bool Stunned
    {
        get { return stunned; }
    }

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
        topCollider.enabled = false;

        animatorHead.SetBool("animateHead", false);
    }

    private void OnPlayerRespawn()
    {
        locked = false;
        topCollider.enabled = true;
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        animatorHead.SetFloat("headbobDegree", headbobDegree);

        oldPlayerY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stunned)
        {
            IsGrounded();
        }
        else
        {
            onGround = false;
        }

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

            HeadBob();

            if (onGround && comparePos)
            {
                ComparePlayerPos();
            }

            if (!onGround && !comparePos)
            {
                CalculateFallHeigth();
            }

        }
    }

    void FixedUpdate()
    {
        //Slows the velocity each frame if the player is grounded.
        //Counteracts the sliding resulting from AddForce.
        if (onGround)
        {
            rigid.velocity *= 0.8f;
        }
        else
        {
            rigid.velocity = new Vector3(rigid.velocity.x * 0.98f, rigid.velocity.y, rigid.velocity.z * 0.98f);
            rigid.useGravity = true;
        }

        StopSlideOnSlopes();

        if (!locked && !stunned)
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
        }

        if (crouching)
        {
            moveVector *= crouchSpeedModifier;
        }

        if (Input.GetKey(KeyBindings.KeyMoveSprint) && !crouching && onGround)
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

            if (onGround && rigid.velocity.magnitude <= maxMagnitude)
            {
                //If grounded, add the velocity.
                rigid.AddRelativeForce((moveVelocity * 500) * Time.deltaTime);
            }
            else if (!onGround && rigid.velocity.magnitude <= maxMagnitude)
            {
                //If not grounded, add the velocity multiplied by the inAirSpeedModifier.
                rigid.AddRelativeForce(((moveVelocity * inAirSpeedModifier) * 500) * Time.deltaTime);
            }
        }

        if (!jumpKey && jumping && onGround)
        {
            jumping = false;
        }

        //Called when first jumping, adds the initial jump velocity.
        if (jumpKey && onGround && !jumping)
        {
            rigid.velocity += new Vector3(0, initialJumpVelocity, 0);

            jumping = true;
            jumpStartTime = Time.time;

            EventManager.RaiseOnPlayerJump();
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
        if (crouchKey && !crouching)
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

        animatorHead.SetBool("crouched", true);

        // Disables the top collider so it won't collide with the environment.
        topCollider.enabled = false;

        EventManager.RaiseOnPlayerCrouch();
    }

    /// <summary>
    /// Enables the upper capsule collider and moves the camera back up.
    /// </summary>
    void EndCrouch()
    {
        crouching = false;

        animatorHead.SetBool("crouched", false);

        // Enables the top collider so it won't collide with the environment.
        topCollider.enabled = true;
    }

    /// <summary>
    /// Moves an empty gameObject called the head, based on the moveVector.
    /// </summary>
    void HeadBob()
    {
        //If sprinting increase the amount of headbobbing.
        if (sprintKey && !crouching)
        {
            animatorHead.SetFloat("animationSpeed", bobSpeedSprinting);
        }
        //If not sprinting, change the amount of headbobbing back to normal.
        else if (!sprintKey && !crouching)
        {
            animatorHead.SetFloat("animationSpeed", bobSpeed);
        }
        else if (!sprintKey && crouching)
        {
            animatorHead.SetFloat("animationSpeed", bobSpeedCrouching);
        }

        //If WASD is pressed.
        if (moveVector != Vector3.zero && onGround)
        {
            animatorHead.SetBool("animateHead", true);
        }
        //If no movement keys are pressed.
        else
        {
            animatorHead.SetBool("animateHead", false);
        }
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
                    onGround = true;
                    return true;
                }
            }
        }

        onGround = false;
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

                if (currentSlope < maximumSlopeAngle && onGround)
                {
                    rigid.useGravity = false;
                }
                else if (!onGround)
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

    /// <summary>
    /// Stuns the player for a given amount of seconds.
    /// </summary>
    /// <param name="seconds">The amount of seconds the player is stunned.</param>
    public void StunForSeconds(float seconds)
    {
        StartCoroutine(CoroutineStunForSeconds(seconds));
    }

    /// <summary>
    /// Coroutine that controls the stunned variable.
    /// </summary>
    /// <param name="seconds">The amount of seconds from the player is stunned till the player is no longer stunned.</param>
    private IEnumerator CoroutineStunForSeconds(float seconds)
    {
        stunned = true;

        yield return new WaitForSeconds(seconds);

        stunned = false;
    }

    public void ComparePlayerPos()
    {
        float fallDistance = fallHeigth - transform.position.y;

        if (fallDistance >= maxFallDistance)
        {
            EventManager.RaiseOnPlayerDeath();
        }
        else if(fallDistance >= maxFallDistance / 5)
        {
            float t = fallDistance / (maxFallDistance / 2);

            animatorHead.SetFloat("fallAmount", Mathf.Clamp(t, 0, 1));

            animatorHead.SetTrigger("triggerFall");

        }

        oldPlayerY = transform.position.y;
        comparePos = false;
    }

    private void CalculateFallHeigth()
    {
        if(transform.position.y > oldPlayerY)
        {
            oldPlayerY = transform.position.y;
        }
        else
        {
            fallHeigth = transform.position.y;
            comparePos = true;
        }
    }
}