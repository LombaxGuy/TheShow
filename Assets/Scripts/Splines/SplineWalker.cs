using UnityEngine;
using System.Collections;

public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

public class SplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public SplineWalkerMode mode;

    public float duration;

    public bool lookForward;

    private bool goingForward = true;

    private bool isTurning = false;

    private Vector3 targetRotation;

    private float progress;
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Angle(
            spline.GetDirection(progress), spline.GetDirection(progress + 0.01f)) > 80f &&
            !isTurning)
        {
            Debug.Log("Hit corner!");
            isTurning = true;

            targetRotation = spline.GetDirection(progress + 0.1f);
        }

        if(isTurning)
        {
            var rotation = Quaternion.FromToRotation(transform.position, targetRotation);
            Debug.Log(targetRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotation) < 5f)
                isTurning = true;
        }

        if (!isTurning)
        {
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

        if (lookForward && !isTurning)
            transform.LookAt(position + spline.GetDirection(progress));	
	}
}
