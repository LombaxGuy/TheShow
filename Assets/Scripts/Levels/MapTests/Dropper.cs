using UnityEngine;
using System.Collections;


public class Dropper : MonoBehaviour {

    //Used to check position
    private Vector3 topPosition;

    private Vector3 botPosition;

    private bool isDown = false;

    private bool isMoving = false;

    //Timers That are used
    private float offsetTimer = 0;

    private float idleTimer = 0;

    //Settings to change dropper behaviour
    [SerializeField]
    [Tooltip("Time it takes to move down")]
    private float dropDownTime = 0.4f;
    [SerializeField]
    [Tooltip("Time it takes to move up")]
    private float dropUpTime = 1f;
    [SerializeField]
    [Tooltip("How long in seconds until the script starts running")]
    private float offsetSeconds = 0;
    [SerializeField]
    [Tooltip("How long until this starts going down")]
    private float idleInSeconds = 3;
    [SerializeField]
    [Tooltip("How long until this starts going up")]
    private float idleOutSeconds = 1;



    /// <summary>
    /// Setting the positions for top and bottom vectors
    /// Using raycast for the length from first position to the closest object beneath it. Max 20 range down.
    /// </summary>
    void Start()
    {
        topPosition = transform.position;
        botPosition = transform.position;


        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20))
        {
            Debug.DrawRay(transform.position, Vector3.down * 20, Color.red);

            botPosition = new Vector3(transform.position.x, transform.position.y - hit.distance, transform.position.z);
        }

    }

    /// <summary>
    /// Checking the positions to see which way to move. Handling the timers for offset and wait.
    /// </summary>
    void Update()
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


        if (isDown)
        {
            if (idleTimer > idleOutSeconds)
            {
                Move(botPosition, topPosition, dropUpTime);
                isDown = false;
                idleTimer = 0;
            }

        }
        else
        {
            if (idleTimer > idleInSeconds)
            {
                Move(topPosition, botPosition, dropDownTime);
                isDown = true;
                idleTimer = 0;
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
    /// This is simply used to call the coroutine.
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    private void Move(Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

    }
}
