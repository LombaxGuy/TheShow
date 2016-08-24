using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour
{
    Rigidbody rigid;

    [SerializeField]
    GameObject head;

    [SerializeField]
    CapsuleCollider bottomCollider;

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
    bool grounded = false;

    bool locked = false;

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

        if (crouching)
        {
            EndCrouch();
        }
    }

    private void OnPlayerRespawn()
    {
        locked = false;
    }

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        //Sets the reference to the head position and the rest position.
        headPosition = head.transform.localPosition;
        restPosition = headPosition;
    }

    // Update is called once per frame
    void Update()
    {
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

            if (grounded)
                HeadBob();
        }
    }

    void FixedUpdate()
    {
        //Slows the velocity each frame if the player is grounded.
        //Counteracts the sliding resulting from AddForce.
        if (grounded)
            rigid.velocity *= 0.9f;

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

        if (Input.GetKey(KeyBindings.KeyMoveCrouch) && grounded)
        {
            crouchKey = true;

            //Adds the crouchSpeedModifier to the move vector.
            moveVector *= crouchSpeedModifier;
        }

        if (Input.GetKey(KeyBindings.KeyMoveSprint) && !crouchKey)
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
        ////Slows the velocity each frame if the player is grounded.
        ////Counteracts the sliding resulting from AddForce.
        //if (grounded)
        //    rigid.velocity *= 0.9f;

        //If either W, A, S or D is held.
        if (moveVector != Vector3.zero)
        {
            moveVelocity = Vector3.zero;

            //Creates the new desired velocity based on the moveVector.
            moveVelocity.x = moveVector.x * speedMultiplier;
            moveVelocity.y = 0f;
            moveVelocity.z = moveVector.z * speedMultiplier;

            if (grounded && rigid.velocity.magnitude <= maxMagnitude)
                //If grounded, add the velocity.
                rigid.AddRelativeForce((moveVelocity * 500) * Time.deltaTime);

            else if (!grounded && rigid.velocity.magnitude <= maxMagnitude)
            {
                //If not grounded, add the velocity multiplied by the inAirSpeedModifier.
                rigid.AddRelativeForce(((moveVelocity * inAirSpeedModifier) * 500) * Time.deltaTime);
            }
        }

        if (!jumpKey && jumping && grounded)
        {
            jumping = false;
        }

        //Called when first jumping, adds the initial jump velocity.
        if (jumpKey && grounded && !jumping)
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

        //If the crouch key is held, the player isn't already crouching and is grounded.
        else if (crouchKey && !crouching && grounded)
        {
            Crouch();
        }

        //If the crouch key is no longer held, and the player is crouching.
        else if (!crouchKey && crouching)
        {
            EndCrouch();
        }
    }

    /// <summary>
    /// Disables the upper capsule collider and moves the camera down.
    /// </summary>
    void Crouch()
    {
        crouching = true;

        //Makes the top collider a trigger so it won't collide with the environment.
        topCollider.isTrigger = true;

        //Set's the rest position of the camera to the crouch height. 
        //The HeadBob method handles the position of the camera.
        restPosition.y -= crouchHeight;
    }

    /// <summary>
    /// Enables the upper capsule collider and moves the camera back up.
    /// </summary>
    void EndCrouch()
    {
        //Get's the position of the top collider.
        Vector3 topColliderPosition = topCollider.transform.position + topCollider.center;

        //If the OverlapSphere intersects two colliders or less, stand back up.
        //(Top and bottom collider)
        if (Physics.OverlapSphere(topColliderPosition, 0.5f).Length <= 2)
        {
            crouching = false;
            topCollider.isTrigger = false;

            restPosition.y += crouchHeight;
        }
    }

    /// <summary>
    /// Moves an empty gameObject called the head, based on the moveVector.
    /// </summary>
    void HeadBob()
    {
        //If sprinting increase the amount of headbobbing.
        if (sprintKey)
        {
            curBobAmount = bobAmountSprinting;
            curBobSpeed = bobSpeedSprinting;
        }

        //If not sprinting, change the amount of headbobbing back to normal.
        if (!sprintKey)
        {
            //Used a lerp to make the transition from sprinting to walking more smooth.
            curBobAmount = Mathf.Lerp(bobAmountSprinting, bobAmount, 0.2f);
            curBobSpeed = Mathf.Lerp(bobSpeedSprinting, bobSpeed, 0.2f);
        }

        //If WASD is pressed.
        if (moveVector != Vector3.zero)
        {
            //Increased the progress of the current "bob" based on the bobSpeed.
            bobTimer += curBobSpeed * Time.deltaTime;

            //Calculates the new position of the camera based on the progress of the bob.
            Vector3 newPosition = new Vector3(Mathf.Cos(bobTimer) * curBobAmount,
                                                Mathf.Lerp(headPosition.y, restPosition.y + Mathf.Abs((Mathf.Sin(bobTimer) * curBobAmount)), transitionSpeed * Time.deltaTime),
                                                restPosition.z);

            headPosition = newPosition;
        }

        //If no movement keys are pressed.
        else
        {
            //Reset the bob timer.
            bobTimer = Mathf.PI / 2;

            //Lerps the position back to the rest position.
            Vector3 newPosition = new Vector3(Mathf.Lerp(headPosition.x, restPosition.x, transitionSpeed * Time.deltaTime),
                                              Mathf.Lerp(headPosition.y, restPosition.y, transitionSpeed * Time.deltaTime),
                                              Mathf.Lerp(headPosition.z, restPosition.z, transitionSpeed * Time.deltaTime));

            headPosition = newPosition;
        }

        //If the bobTimer is too large, reset it to restart the bob.
        if (bobTimer > Mathf.PI * 2)
        {
            bobTimer = 0;
        }

        //Set the localPosition of the head.
        head.transform.localPosition = headPosition;
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = true;
    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = false;
    }
}
