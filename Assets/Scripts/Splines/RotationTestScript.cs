using UnityEngine;
using System.Collections;

public class RotationTestScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The walker will move through the following objects from first to last.")]
    private GameObject[] path;

    [SerializeField]
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
                //if (currentTargetIndex < path.Length)
                //{
                //    Debug.Log("BUHUUU");

                //    if (mode == SplineWalkerMode.PingPong)
                //    {
                //        if (goingForward)
                //        {
                //            currentTargetIndex += 1;
                //        }
                //        else
                //        {
                //            currentTargetIndex -= 1;
                //        }
                //    }
                //    else
                //    {
                //        currentTargetIndex += 1;
                //    }
                //}

                //if (currentTargetIndex == path.Length)
                //{
                //    switch (mode)
                //    {
                //        case SplineWalkerMode.Once:
                //            stop = true;
                //            break;

                //        case SplineWalkerMode.Loop:
                //            Debug.Log("LOOOPing");
                //            currentTargetIndex = 0;
                //            break;

                //        case SplineWalkerMode.PingPong:
                //            if (goingForward)
                //            {
                //                currentTargetIndex -= 1;
                //                //RotateYTo(transform, GetDirFromPointToPoint(path[currentTargetIndex - 1].transform.position, path[currentTargetIndex].transform.position), 3);
                //                goingForward = false;
                //            }
                //            else
                //            {
                //                currentTargetIndex += 1;
                //                goingForward = true;
                //            }
                //            break;
                //    }
                //}

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
                            RotateYTo(transform, GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        break;

                    case SplineWalkerMode.Loop:
                        currentTargetIndex += 1;

                        if (currentTargetIndex >= path.Length)
                        {
                            currentTargetIndex = 0;
                        }
                        Debug.Log(currentTargetIndex);
                        RotateYTo(transform, GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position), 3);

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
                            RotateYTo(transform, GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        else
                        {
                            currentTargetIndex -= 1;
                            RotateYTo(transform, GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position), 3);
                        }
                        break;
                }
            }

            if (!isRotating)
            {
                Debug.DrawRay(transform.position, GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position), Color.green);
                transform.position += GetDirFromPointToPoint(transform.position, path[currentTargetIndex].transform.position) * moveSpeed * Time.deltaTime;
            }
        }


        


	 //   if (Input.GetKeyDown(KeyCode.E))
     //   {
     //       RotateYTo(transform, transform.position - testObjs[0].transform.position, 3);
     //   }

     //   if (Input.GetKeyDown(KeyCode.R))
     //   {
     //       RotateYTo(transform, transform.position - testObjs[1].transform.position, 3);
     //   }

     //   if (Input.GetKeyDown(KeyCode.T))
     //   {
     //       RotateYTo(transform, transform.position - testObjs[2].transform.position, 3);
     //   }

     //   if (Input.GetKeyDown(KeyCode.Y))
     //   {
     //       RotateYTo(transform, transform.position - testObjs[3].transform.position, 3);
     //   }

     //   MoveWalker();
    }


    /// <summary>
    /// Moves the walker.
    /// </summary>
    private void MoveWalker()
    {

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
    /// Returns the normalized direction vector from point a to point b.
    /// </summary>
    /// <param name="a">The position the direction is calculated from.</param>
    /// <param name="b">The position the direction is calculated to.</param>
    /// <returns>The normalized direction vector.</returns>
    public Vector3 GetDirFromPointToPoint(Vector3 a, Vector3 b)
    {
        return (b - a).normalized;
    }

    /// <summary>
    /// Rotates the Y-axis of a given transform towards the targetDirection over an amount of time.
    /// </summary>
    /// <param name="rotatingTransform">The transform that will be rotated.</param>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    /// <param name="secondsPerRotation">The amount of seconds it takes to rotate the transform 360 degrees.</param>
    public void RotateYTo(Transform rotatingTransform, Vector3 targetDirection, float secondsPerRotation)
    {
        StartCoroutine(CoroutineRotateYTo(rotatingTransform, targetDirection, secondsPerRotation));
    }

    /// <summary>
    /// A coroutine that rotates the Y-axis of a given transform towards a targetDirection over an amount of time.
    /// </summary>
    /// <param name="rotatingTransform">The transform that will be rotated.</param>
    /// <param name="targetDirection">Roughly the direction the transform will face after the rotation is done.</param>
    /// <param name="secondsPerRotation">The amount of seconds it takes to rotate the transform 360 degrees.</param>
    private IEnumerator CoroutineRotateYTo(Transform rotatingTransform, Vector3 targetDirection, float secondsPerRotation)
    {
        //float rotationSpeed = 360 / secondsPerRotation;

        //Vector3 startDirection = rotatingTransform.forward;
        //float t = 0.0f;

        //float deltaAngle = Vector3.Angle(startDirection, targetDirection);

        //float totalRotationTime = deltaAngle / rotationSpeed;

        //while (t < 1.0f)
        //{
        //    rotatingTransform.rotation = Quaternion.Euler(0, deltaAngle * t, 0);
        //    t += Time.deltaTime / totalRotationTime;
        //    yield return null;
        //}

        //if (t != 1.0f)
        //{
        //    t = 1.0f;
        //    rotatingTransform.rotation = Quaternion.Euler(0, deltaAngle * t, 0);
        //}

        // -- SHOULD WORK --
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

        isRotating = false;
        // -- SHOULD WORK --
    }
}
