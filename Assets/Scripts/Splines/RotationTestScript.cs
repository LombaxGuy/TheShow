using UnityEngine;
using System.Collections;

public class RotationTestScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The walker will move through the following objects from first to last.")]
    private GameObject[] path;

    private int currentTargetIndex = 0;

    [SerializeField]
    private SplineWalkerMode mode;

    private float moveSpeed = 10f;
    private float threshold = 0.1f;
    private bool goingForward = true;
    private bool stop = false;
    private bool isRotating = false;


    // Use this for initialization
    private void Start()
    {
        // Set the position of the walker to the first waypoint if there are any waypoints in the array.
        if (path.Length > 0)
        {
            transform.position = path[0].transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (path.Length > 0 && !stop)
        {
            if (WalkerReachedDestination())
            {
                switch (mode)
                {
                    case SplineWalkerMode.Once:
                        if (currentTargetIndex >= path.Length - 1)
                        {
                            stop = true;
                        }
                        else if (currentTargetIndex < path.Length - 1)
                        {
                            currentTargetIndex += 1;
                            RotateForwardTo(transform, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        break;

                    case SplineWalkerMode.Loop:
                        currentTargetIndex += 1;

                        if (currentTargetIndex >= path.Length)
                        {
                            currentTargetIndex = 0;
                        }
                        Debug.Log(currentTargetIndex);
                        RotateForwardTo(transform, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), 3);

                        break;

                    case SplineWalkerMode.PingPong:
                        if (currentTargetIndex >= path.Length - 1)
                        {
                            goingForward = false;
                        }
                        else if (currentTargetIndex == 0)
                        {
                            goingForward = true;
                        }

                        if (goingForward)
                        {
                            currentTargetIndex += 1;
                            RotateForwardTo(transform, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        else
                        {
                            currentTargetIndex -= 1;
                            RotateForwardTo(transform, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        break;
                }
            }

            if (!isRotating)
            {
                Debug.DrawRay(transform.position, HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position), Color.green);
                transform.position += HelperFunctions.DirectionFromTo(transform.position, path[currentTargetIndex].transform.position) * moveSpeed * Time.deltaTime;
            }
        }
    }

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
    /// <param name="rotatingTransform">The transform that will be rotated.</param>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    /// <param name="secondsPerRotation">The amount of seconds it takes to rotate the transform 360 degrees.</param>
    public void RotateForwardTo(Transform rotatingTransform, Vector3 targetDirection, float secondsPerRotation)
    {
        StartCoroutine(CoroutineRotateForwardTo(rotatingTransform, targetDirection, secondsPerRotation));
    }

    /// <summary>
    /// A coroutine that rotates the forward-vector of a given transform towards a targetDirection over an amount of time.
    /// </summary>
    /// <param name="rotatingTransform">The transform that will be rotated.</param>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    /// <param name="secondsPerRotation">The amount of seconds it takes to rotate the transform 360 degrees.</param>
    private IEnumerator CoroutineRotateForwardTo(Transform rotatingTransform, Vector3 targetDirection, float secondsPerRotation)
    {
        isRotating = true;

        float rotationSpeed = 360 / secondsPerRotation;

        Vector3 startDirection = rotatingTransform.forward;
        float t = 0.0f;

        float deltaAngle = Vector3.Angle(startDirection, targetDirection);

        float totalRotationTime = deltaAngle / rotationSpeed;
        
        Quaternion startRotation = rotatingTransform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation; 

        while (t < 1.0f)
        {
            newRotation = Quaternion.Slerp(startRotation, targetRotation, t);

            rotatingTransform.rotation = newRotation;

            t += Time.deltaTime / totalRotationTime;

            yield return null;
        }

        t = 1.0f;
        rotatingTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

        isRotating = false;
    }

    private void OnDrawGizmos()
    {
        if (path.Length > 0)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    Gizmos.color = new Color(0, 0, 1, 0.2f);
                    Gizmos.DrawSphere(path[i].transform.position, 0.1f);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(path[i].transform.position, 0.1f);

                    if (i == path.Length - 1)
                    {
                        switch (mode)
                        {
                            case SplineWalkerMode.Once:
                                Gizmos.color = new Color(1, 0, 0, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                Gizmos.color = Color.red;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                break;

                            case SplineWalkerMode.Loop:
                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[0].transform.position, Color.blue);

                                break;

                            case SplineWalkerMode.PingPong:
                                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                                Gizmos.color = Color.yellow;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                                break;
                        }
                    }
                    else if (i == 0)
                    {
                        switch (mode)
                        {
                            case SplineWalkerMode.Once:
                                Gizmos.color = new Color(0, 1, 0, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                Gizmos.color = Color.green;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));

                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                                break;

                            case SplineWalkerMode.Loop:
                                Gizmos.color = new Color(0, 1, 0, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                Gizmos.color = Color.green;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));

                                HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                                break;

                            case SplineWalkerMode.PingPong:
                                Gizmos.color = new Color(0, 1, 0, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));
                                Gizmos.color = Color.green;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.4f, 0.4f, 0.4f));

                                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.2f);
                                Gizmos.DrawCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                                Gizmos.color = Color.yellow;
                                Gizmos.DrawWireCube(path[i].transform.position, new Vector3(0.2f, 0.2f, 0.2f));

                                Gizmos.color = Color.blue;
                                Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                                break;
                        }
                    }
                    else
                    {
                        if (mode == SplineWalkerMode.PingPong)
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                        }
                        else
                        {
                            HelperFunctions.GizmoLineWithDirection(path[i].transform.position, path[i + 1].transform.position, Color.blue);
                        }
                    }
                }
            }
        }
    }
}
