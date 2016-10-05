using UnityEngine;
using System.Collections;


public class Spinner : MonoBehaviour
{



    private bool isOut = false;

    [SerializeField]
    private bool useEvent = false;

    [SerializeField]
    private bool isActivated = true;

    private bool isMoving = false;

    [SerializeField]
    [Tooltip("")]
    private int moveMode;

    //Used to set the things people use
    [SerializeField]
    [Tooltip("How far this extends")]
    private float distance = 5;
    [SerializeField]
    [Tooltip("How fast this extends")]
    private float pushOutTime = 0.2f;
    [SerializeField]
    [Tooltip("How long in seconds until the script starts running")]
    private float offsetSeconds = 0;
    [SerializeField]
    [Tooltip("How long this stays in")]
    private float idleInSeconds = 3;
    [SerializeField]
    [Tooltip("How long this stays out")]
    private float idleOutSeconds = 1;
    [SerializeField]
    [Tooltip("If the spinner should move at all times or only when triggered")]
    public bool move = true;

    //used timers
    private float offsetTimer = 0;
    private float idleTimer = 0;

    private AudioSource[] audioChilden;
    private bool audioInSession = false;

    private Vector3 startPos;

    private Vector3 extPos;

    private void OnEnable()
    {
        if (useEvent)
        {
            EventManager.OnTimerExpired += OnTimerExpired;
            EventManager.OnButtonPressed += OnButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (useEvent)
        {
            EventManager.OnTimerExpired -= OnTimerExpired;
            EventManager.OnButtonPressed -= OnButtonPressed;
        }
    }

    // Use this for initialization
    //set positions and casing the rotation/piston mode
    void Start()
    {
        audioChilden = GetComponentsInChildren<AudioSource>();

        startPos = transform.position;
        extPos = transform.position + transform.forward * distance;

        switch (moveMode)
        {
            case 1:
                extPos = transform.position + transform.forward * distance;
                break;
            case 2:
                extPos = transform.position + transform.right * distance;
                break;
        }
    }

    // Update is called once per frame
    /// <summary>
    /// controlling the time and checking positions
    /// </summary>
    void Update()
    {
        if (isActivated)
        {
            if (move)
            {
                if (!isMoving)
                {
                    if (offsetTimer < offsetSeconds)
                    {

                        offsetTimer += Time.deltaTime;
                    }
                    else
                    {
                        idleTimer += Time.deltaTime;
                    }
                }

                if (isOut)
                {
                    if (idleTimer > idleOutSeconds)
                    {

                        Move(extPos, startPos, pushOutTime);
                        isOut = false;
                        idleTimer = 0;
                    }

                }
                else
                {
                    if (idleTimer > idleInSeconds)
                    {
                        Move(startPos, extPos, pushOutTime);
                        isOut = true;
                        idleTimer = 0;
                    }
                }

            }

        }

    }

    /// <summary>
    /// Coroutine for the movement of the object
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    /// <returns></returns>
    private IEnumerator CoroutineMove(Vector3 from, Vector3 to, float timeInSeconds)
    {
        isMoving = true;
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(from, to, t);

            t += Time.deltaTime / timeInSeconds;

            yield return null;
        }


        transform.position = Vector3.Lerp(from, to, t);
        isMoving = false;
    }

    /// <summary>
    /// Coroutine for the movement of the object
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    private void Move(Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

        if (!audioInSession)
        {
            for (int i = 0; i < audioChilden.Length; i++)
            {
                audioChilden[i].Play();
            }
            audioInSession = true;
        }
    }

    private void OnButtonPressed()
    {
        isActivated = true;
    }

    private void OnTimerExpired()
    {
        isActivated = false;
    }


}
