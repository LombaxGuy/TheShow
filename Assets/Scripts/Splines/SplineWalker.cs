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

    public SplineWalkerMode mode;

    public float duration;

    public bool lookForward;

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

        Quaternion newRotation;
        Vector3 startDirection = transform.forward;
        float t = 0.0f;

        float deltaAngle = Vector3.Angle(startDirection, targetDirection);
        float totalRotationTime = deltaAngle / rotationSpeed;
        //Debug.Log(deltaAngle);

        if (Vector3.Angle(transform.forward, targetDirection) < orthogonalAngleThreshold)
        {
            Debug.Log("Done turning! " + spline.GetDirection(progress + nextDirectionThreshold));

            isRotating = false;
        }

        while (t < 1.0f)
        {
            newRotation = Quaternion.LookRotation(Vector3.Lerp(startDirection, targetDirection, t));

            transform.rotation = newRotation;

            //Debug.Log("Start dir: " + startDirection);
            //Debug.Log("Target dir: " + targetDirection);

            //Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
            //Debug.DrawRay(transform.position, targetDirection * 10, Color.red);

            t += Time.deltaTime / totalRotationTime;

            yield return null;
        }

        Debug.Log("Done turning! " + spline.GetDirection(progress + nextDirectionThreshold));
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
            //Debug.Log("Entered IF!");
            if (goingForward)
            {
                progress += Time.deltaTime / duration;

                if (progress > 1f)
                {
                    if (mode == SplineWalkerMode.Once)
                        progress = 1f;

                    else if (mode == SplineWalkerMode.Loop)
                        progress -= 1f;

                    else
                    {
                        progress = 2f - progress;
                        goingForward = false;
                    }
                }
            }

            else
            {
                progress -= Time.deltaTime / duration;

                if (progress < 0f)
                {
                    progress = -progress;
                    goingForward = true;
                }
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;

        if (lookForward && !isRotating)
            transform.LookAt(position + spline.GetDirection(progress));

        rotationCoolDown += Time.deltaTime;
        float deltaAngle = Vector3.Angle(spline.GetDirection(progress), spline.GetDirection(progress + nextDirectionThreshold));

        if (deltaAngle > orthogonalAngleThreshold && !isRotating && rotationCoolDown >= 1)
        {
            Debug.Log("Hit corner! " + spline.GetDirection(progress + nextDirectionThreshold));

            rotationCoolDown = 0f;
            isRotating = true;
            StartCoroutine(RotateTo(spline.GetDirection(progress + nextDirectionThreshold)));

            //targetRotation = spline.GetDirection(progress + 0.1f);
            //targetRotation = spline.GetDirection(progress + nextDirectionThreshold);
        }
    }
}
