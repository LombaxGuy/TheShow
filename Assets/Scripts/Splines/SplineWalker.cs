using UnityEngine;
using System.Collections;

public enum WalkerMode
{
    Once,
    Loop,
    PingPong
}

public class SplineWalker : MonoBehaviour
{
    public BezierSpline spline;

    [SerializeField]
    private WalkerMode mode;

    [SerializeField]
    private bool lookForward = true;

    [SerializeField]
    private float duration = 4;

    private bool goingForward = true;

    private bool isRotating = false;

    private float progress;

    private float timeToRotateOneLoop = 3.0f;
    private float rotationSpeed;

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
        progress = 0;
    }

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


        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion newRotation;

        while (t < 1.0f)
        {
            newRotation = Quaternion.Slerp(startRotation, targetRotation, t);

            transform.rotation = newRotation;

            t += Time.deltaTime / totalRotationTime;

            yield return null;
        }

        //Debug.Log("Done turning! " + spline.GetDirection(progress + nextDirectionThreshold));
        isRotating = false;
    }

    // Update is called once per frame
    void Update ()
    {
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
                        case WalkerMode.Once:
                            progress = 1.0f;
                            break;

                        case WalkerMode.Loop:
                            progress = 0.0f;
                            break;

                        case WalkerMode.PingPong:
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

        if (mode == WalkerMode.PingPong && !isRotating)
        {
            if (progress <= 0 || progress >= 1)
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
    }
}
