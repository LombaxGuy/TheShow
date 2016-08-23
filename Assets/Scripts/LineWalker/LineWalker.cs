using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LineWalker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The walker will move through the following objects from first to last.")]
    private GameObject[] path;

    private int currentTargetIndex = 0;

    [SerializeField]
    private WalkerMode mode;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    [Tooltip("The amount of seconds it would take to rotate 360 degrees.")]
    private float secondsPerRotation = 3;

    private Quaternion startRotation;

    private float threshold = 0.1f;
    private bool goingForward = true;
    private bool stop = false;
    private bool isRotating = false;

    void OnEnable()
    {
        // Subscribes to the OnRewpawnReset event
        GameObjectPositionReset.OnResetObjects += OnRespawnReset;
    }

    void OnDisable()
    {
        // Unsubscribes from the OnRewpawnReset event
        GameObjectPositionReset.OnResetObjects -= OnRespawnReset;
    }

    /// <summary>
    /// Code is called when the OnRespawnReset even is called
    /// </summary>
    void OnRespawnReset()
    {
        // Resets the variables
        currentTargetIndex = 0;
        stop = false;
        goingForward = true;

        // Stops any rotation coroutines that might run
        StopCoroutine("CoroutineRotateForwardTo");

        // Resets the rotations to the start rotation
        if (startRotation != null)
        {
            transform.rotation = startRotation;
        }

        // Resets the position to the starting position
        if (path.Length > 0)
        {
            transform.position = path[0].transform.position;
        }
    }

    // Use this for initialization
    private void Start()
    {
        // Set the position of the walker to the first waypoint if there are any waypoints in the array.
        if (path.Length > 0)
        {
            transform.position = path[0].transform.position;

            startRotation = Quaternion.LookRotation(HelperFunctions.DirectionFromTo(path[0].transform.position, path[1].transform.position));
            transform.rotation = startRotation;
        }   
    }

    // Update is called once per frame
    private void Update()
    {
        // If the length of the path is not 0 and the walker has not reached its destination (Only applys if mode is set to Once)
        if (path.Length > 0 && !stop)
        {
            // If the walker has reached ist current destination...
            if (WalkerReachedDestination())
            {
                switch (mode)
                {
                    //... and the mode is 'Once'.
                    case WalkerMode.Once:

                        // If the target index is also the last index in the path array 'stop' is set to true
                        if (currentTargetIndex >= path.Length - 1)
                        {
                            stop = true;
                        }
                        // Otherwise the target index is increased and we will rotate towards the target direction
                        else if (currentTargetIndex < path.Length - 1)
                        {
                            currentTargetIndex += 1;
                            RotateForwardTo(HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position));
                        }
                        break;

                    //... and the mode is 'Loop'.
                    case WalkerMode.Loop:

                        // The target index is increased
                        currentTargetIndex += 1;

                        // If the target index is larger than or equal to the length of the path array the new target index is set to 0
                        if (currentTargetIndex >= path.Length)
                        {
                            currentTargetIndex = 0;
                        }

                        // Rotate towards the current target
                        RotateForwardTo(HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position));

                        break;

                    //... and the mode is 'PingPong'.
                    case WalkerMode.PingPong:

                        // If the target index is larger than or equal to the length of the path array 'goingForward' is set to false
                        if (currentTargetIndex >= path.Length - 1)
                        {
                            goingForward = false;
                        }
                        // Otherwise if the target index is equal to 0 'goingForward' is set to true
                        else if (currentTargetIndex == 0)
                        {
                            goingForward = true;
                        }

                        // If we are going forward...
                        if (goingForward)
                        {
                            //... the target index is increased and we rotate towards the new target
                            currentTargetIndex += 1;
                            RotateForwardTo(HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position));
                        }
                        // Otherwise...
                        else
                        {
                            //... the target index is decreased and we rotate towards the new target
                            currentTargetIndex -= 1;
                            RotateForwardTo(HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position));
                        }
                        break;
                }
            }

            // If we are not rotating...
            if (!isRotating)
            {
                //... we move towards the target
                transform.position = Vector3.MoveTowards(transform.position, path[currentTargetIndex].transform.position, moveSpeed * Time.deltaTime);
                Debug.DrawRay(transform.position, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), Color.green);
            }
        }
    }

    /// <summary>
    /// Returns a bool based on whether or not the walker has reached its destination.
    /// </summary>
    /// <returns>Returns true if the walker has reached ist destination.</returns>
    private bool WalkerReachedDestination()
    {
        if (transform.position.x <= path[currentTargetIndex].transform.position.x + threshold && transform.position.x >= path[currentTargetIndex].transform.position.x - threshold &&
            transform.position.y <= path[currentTargetIndex].transform.position.y + threshold && transform.position.y >= path[currentTargetIndex].transform.position.y - threshold &&
            transform.position.z <= path[currentTargetIndex].transform.position.z + threshold && transform.position.z >= path[currentTargetIndex].transform.position.z - threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Rotates the forward-vector of a given transform towards the targetDirection over an amount of time.
    /// </summary>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    public void RotateForwardTo(Vector3 targetDirection)
    {
        StartCoroutine("CoroutineRotateForwardTo", targetDirection);      
    }

    /// <summary>
    /// A coroutine that rotates the forward-vector of a given transform towards a targetDirection over an amount of time.
    /// </summary>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    private IEnumerator CoroutineRotateForwardTo(Vector3 targetDirection)
    {
        isRotating = true;

        // Calculates the rotation speed based on 'secondsPerRotation'.
        float rotationSpeed = 360 / secondsPerRotation;

        // Saves the starting direction of the forward vector.
        Vector3 startDirection = transform.forward;

        // 't' value used to Slerp the rotation
        float t = 0.0f;

        // The change in angles. Always between 0 and 180 degrees.
        float deltaAngle = Vector3.Angle(startDirection, targetDirection);

        // Calculates the total amount of time the rotation should take.
        float totalRotationTime = deltaAngle / rotationSpeed;

        // Saves the starting rotation as a Quaternion
        Quaternion startRotation = transform.localRotation;
        // Gets the target rotation as a Quaternion
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // While the is less than 0...
        while (t < 1.0f)
        {
            //... the rotation is Slerp'ed...
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            //... and 't' is increased.
            t += Time.deltaTime / totalRotationTime;

            yield return null;
        }

        t = 1.0f;
        // When the method leaves the while loop we run one last Slerp to make sure that we arrive at exactly the rotation we wanted.
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

        // The rotation is now done and 'isRotating' is set to false
        isRotating = false;
    }

    /// <summary>
    /// Daws the gizmos for the path.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Try-Catch to avoid null-referance exceptions
        try
        {
            // If the path array is longer than 0
            if (path.Length > 0)
            {
                // Runs through the array of waypoints
                for (int i = 0; i < path.Length; i++)
                {
                    // Setting the color to a transparent blue color and draws a sphere
                    Gizmos.color = new Color(0, 0, 1, 0.2f);
                    Gizmos.DrawSphere(path[i].transform.position, 0.1f);

                    // Setting the color to a solid blue color and draws a wire sphere
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(path[i].transform.position, 0.1f);

                    // If we are looking at the last waypoint...
                    if (i == path.Length - 1)
                    {
                        switch (mode)
                        {
                            //... and the mode is 'Once'.
                            case WalkerMode.Once:

                                // Draws the endpoint as a red cube
                                Gizmos.color = new Color(1, 0, 0, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                Gizmos.color = Color.red;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                break;

                            //... and the mode is 'Loop'.
                            case WalkerMode.Loop:

                                // Draws an arrow line from the end point to the start point
                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[0].transform.position, Color.blue);
                                break;

                            //... and the mode is 'PingPong'.
                            case WalkerMode.PingPong:

                                // Draws a yellow cube at the end to indicate a direction change point
                                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                                Gizmos.color = Color.yellow;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));

                                // Draws a normal blue line to the next point
                                Gizmos.color = Color.blue;
                                Gizmos.DrawLine(path[i].transform.position, path[0].transform.position);
                                break;
                        }
                    }
                    // Otherwise if we are looking at the first waypoint...
                    else if (i == 0)
                    {
                        //... a green cube is drawn to indicate the starting point...
                        Gizmos.color = new Color(0, 1, 0, 0.2f);
                        Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));

                        switch (mode)
                        {
                            //... and the mode is 'Once'.
                            case WalkerMode.Once:

                                // An arrow line is drawn to the next point in the path
                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                                break;

                            //... and the mode is 'Once'.
                            case WalkerMode.Loop:

                                // An arrow line is drawn to the next point in the path
                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                                break;

                            //... and the mode is 'Once'.
                            case WalkerMode.PingPong:

                                // Draws a yellow cube at the end to indicate a direction change point
                                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                                Gizmos.color = Color.yellow;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));

                                // Draws a normal blue line to the next point
                                Gizmos.color = Color.blue;
                                Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                                break;
                        }
                    }
                    // If we are not looking at the first or the last point in the path...
                    else
                    {
                        //... and the mode is 'PingPong'...
                        if (mode == WalkerMode.PingPong)
                        {
                            //... we draw a normal blue line to the next waypoint
                            Gizmos.color = Color.blue;
                            Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                        }
                        //... and the mode is not 'PingPont'...
                        else
                        {
                            //... we draw a blue arrow line to the next waypoint
                            HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                        }
                    }
                }
            }
        }
        // If an exception occured in the code above...
        catch
        {
            //... the folowing line is displayed in the Debug Log.
            Debug.Log("LineWalker.cs: Some Gizmos could not be dawn! Please make sure that all elements of the array 'path' is set.");
        }
    }
}
