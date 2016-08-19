using UnityEngine;
using System.Collections;

public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

public class SplineWalker : MonoBehaviour
{
    public BezierSpline spline;

    [SerializeField]
    private SplineWalkerMode mode;

    [SerializeField]
    private bool lookForward;

    public float duration;

    private bool goingForward = true;

    public bool isRotating = false;

    private float progress;

    private float orthogonalAngleThreshold = 80.0f;
    private float nextDirectionThreshold = 0.003f;

    private float timeToRotateOneLoop = 3.0f;
    private float rotationSpeed;

    private float rotationCoolDown = 1.0f;

    void Start()
    {
        rotationSpeed = 360 / timeToRotateOneLoop;
    }

    /// <summary>
    /// Rotates the spline walker to the target direction using the rotation speed.
    /// </summary>
    /// <param name="targetDirection">The direction the object should rotate to.</param>
    private IEnumerator RotateTo(Vector3 targetDirection)
    {
        Debug.Log("Started a coroutine");
        isRotating = true;

        float rotationSpeed = 360 / timeToRotateOneLoop;

        Vector3 startDirection = transform.forward;
        float t = 0.0f;

        float deltaAngle = Vector3.Angle(startDirection, targetDirection);

        float totalRotationTime = deltaAngle / rotationSpeed;

        //float startAngle = transform.rotation.eulerAngles.y;

        //while (t < 1.0f)
        //{
        //    Debug.DrawRay(transform.position, targetDirection, Color.red);
        //    Debug.DrawRay(transform.position, transform.forward, Color.green);

        //    transform.rotation = Quaternion.Euler(0, startAngle + deltaAngle * t, 0);
        //    t += Time.deltaTime / totalRotationTime;
        //    yield return null;
        //}

        //if (t != 1.0f)
        //{
        //    t = 1.0f;
        //    transform.rotation = Quaternion.Euler(0, deltaAngle * t, 0);
        //}
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation;
        //Vector3 startDirection = transform.forward;
        //float t = 0.0f;

        //float deltaAngle = Vector3.Angle(startDirection, targetDirection);
        //float totalRotationTime = deltaAngle / rotationSpeed;
        //Debug.Log(deltaAngle);

        //if (Vector3.Angle(transform.forward, targetDirection) < orthogonalAngleThreshold)
        //{
        //    Debug.Log("Done turning! " + spline.GetDirection(progress + nextDirectionThreshold));

        //    isRotating = false;
        //}

        while (t < 1.0f)
        {
            newRotation = Quaternion.Slerp(startRotation, targetRotation, t);

            transform.rotation = newRotation;

            //Debug.Log("Start dir: " + startDirection);
            //Debug.Log("Target dir: " + targetDirection);

            //Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
            //Debug.DrawRay(transform.position, targetDirection * 10, Color.red);

            t += Time.deltaTime / totalRotationTime;

            yield return null;
        }

        //Debug.Log("Done turning! " + spline.GetDirection(progress + nextDirectionThreshold));
        isRotating = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //if(isRotating)
        //{
        //    //var rotation = Quaternion.FromToRotation(transform.position, targetRotation);

        //    float yAngle = Mathf.Lerp(startAngle.y, startAngle.y + deltaAngle, t);

        //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, yAngle, transform.eulerAngles.z);



        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);

        //    if (Quaternion.Angle(transform.rotation, rotation) < 5f)
        //        isRotating = true;
        //}

        if (!isRotating)
        {
            // If the spline walker is going forward on the track. (progress going towards 1.0f)
            if (goingForward)
            {
                Debug.Log("Going forward!");
                progress += Time.deltaTime / duration;

                if (progress > 1.0f)
                {
                    switch (mode)
                    {
                        case SplineWalkerMode.Once:
                            progress = 1.0f;
                            break;

                        case SplineWalkerMode.Loop:
                            progress = 0.0f;
                            break;

                        case SplineWalkerMode.PingPong:
                            progress = 1.0f;
                            goingForward = false;
                            break;
                    }
                }
            }
            // If the spline walker is going backwards on the track. (progress going towards 0.0f
            else
            {
                Debug.Log("Going backward!");
                progress -= Time.deltaTime / duration;

                if (progress < 0f)
                {
                    progress = 0;
                    goingForward = true;
                }
            }
        }

        if (mode == SplineWalkerMode.PingPong && !isRotating)
        {
            if (progress == 0 || progress == 1)
            {
                StartCoroutine(RotateTo(-transform.forward));
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.position = position;

        if (lookForward && !isRotating)
        {
            if (goingForward)
            {
                transform.LookAt(position + spline.GetDirection(progress));
            }
            else
            {
                transform.LookAt(position - spline.GetDirection(progress));
            }
        }
            
        rotationCoolDown += Time.deltaTime;

        float deltaAngle = Vector3.Angle(spline.GetDirection(progress), spline.GetDirection(progress + nextDirectionThreshold));

        if (deltaAngle > orthogonalAngleThreshold && !isRotating && rotationCoolDown >= 1 && (progress != 0 || progress != 1))
        {
            Debug.Log("Hit corner! " + spline.GetDirection(progress + nextDirectionThreshold));

            rotationCoolDown = 0.0f;

            StartCoroutine(RotateTo(spline.GetDirection(progress + nextDirectionThreshold)));

            //targetRotation = spline.GetDirection(progress + 0.1f);
            //targetRotation = spline.GetDirection(progress + nextDirectionThreshold);
        }
    }
}
