using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// The control point mode dictates the relationship between the two velocities where two curves meet.
/// </summary>
public enum BezierControlPointMode
{
    Free, //Both velocities can be altered independently.
    Aligned, //The velocities are aligned but the magnitude can be changed independently.
    Mirrored //Velocity and magnitude are mirrored.
}

/// <summary>
/// Class for making bezier splines.
/// Bezier splines consists of curves, each curve containing two velocity handles.
/// </summary>
public class BezierSpline : MonoBehaviour {

    /// <summary>
    /// Stores the ControlPointMode between individual curves.
    /// A curve consists of two ControlPoints and two handles to control the curve between the points.
    /// </summary>
    [SerializeField]
    private BezierControlPointMode[] modes;

    /// <summary>
    /// Contains all points in the spline, including the handles.
    /// Point(0) - handle(1) - handle(2) - point(3)
    /// </summary>
    [SerializeField]
    private Vector3[] points;

    /// <summary>
    /// Is should this spline loop?
    /// </summary>
    [SerializeField]
    private bool loop;

    /// <summary>
    /// Get or set the bool "loop".
    /// When set to true, the ControlPointMode of the last ControlPoint is set = the first ControlPoint.
    /// </summary>
    public bool Loop
    {
        get { return loop; }

        set
        {
            loop = value;
            if(value == true)
            {
                modes[modes.Length - 1] = modes[0];

                //Sets position of the first ControlPoint to the position of the first ControlPoint.
                //Sounds stupid, I know, but SetControlPoint should take care of the other points.
                SetControlPoint(0, points[0]);
            }
        }
    }


    /// <summary>
    /// Gets the amount of ControlPoints.
    /// </summary>
    public int ControlPointCount
    {
        get { return points.Length; }
    }
    
    /// <summary>
    /// Returns position of ControlPoint at specified index.
    /// </summary>
    /// <param name="index">Index of the ControlPoint.</param>
    /// <returns>Position of ControlPoint at index.</returns>
    public Vector3 GetControlPoint (int index)
    {
        return points[index];
    }

    /// <summary>
    /// Sets the position of a ControlPoint and it's velocity handles.
    /// </summary>
    /// <param name="index">Index of ControlPoint to set.</param>
    /// <param name="point">Position.</param>
    public void SetControlPoint(int index, Vector3 point)
    {
        //If the remainder of index / 3 == 0.
        //This returns true if the selected point is a ControlPoint ( as opposed to a handle ).
        if(index % 3 == 0)
        {
            //Find difference of the position of the desired point and the position of the ControlPoint at index.
            //This value is used to make the velocity handles move with the ControlPoint.
            Vector3 delta = point - points[index];

            //If the spline is set to loop.
            if (loop)
            {
                if (index == 0)
                {
                    points[1] += delta;

                    points[points.Length - 2] += delta;

                    points[points.Length - 1] = point;
                }

                else if (index == points.Length - 1)
                {
                    points[0] = point;
                    points[1] += delta;
                    points[index - 1] += delta;
                }

                else
                {
                    points[index - 1] += delta;
                    points[index + 1] += delta;
                }
            }

            else
            {
                if (index > 0)
                    points[index - 1] += delta;

                if (index + 1 < points.Length)
                    points[index + 1] += delta;
            }
        }
        
        //Sets position of point at index.
        points[index] = point;


        EnforceMode(index);
    }

    /// <summary>
    /// Gets ControlPointMode of ControlPoint at index.
    /// </summary>
    /// <param name="index">Index of the ControlPoint.</param>
    /// <returns>ControlPointMode of ControlPoint at index.</returns>
    public BezierControlPointMode GetControlPointMode(int index)
    {
        return modes[(index + 1) / 3];
    }

    /// <summary>
    /// Set ControlPointMode at given index.
    /// </summary>
    /// <param name="index">Index of ControlPoint.</param>
    /// <param name="mode">Mode to change to.</param>
    public void SetControlPointMode(int index, BezierControlPointMode mode)
    {
        int modeIndex = (index + 1) / 3;
        modes[modeIndex] = mode;

        if(loop)
        {
            if (modeIndex == 0)
                modes[modes.Length - 1] = mode;

            else if (modeIndex == modes.Length - 1)
                modes[0] = mode;
        }

        EnforceMode(index);
    }

    /// <summary>
    /// Enforces ControlPointMode my moving the velocity handles of a ControlPoint at given index.
    /// </summary>
    /// <param name="index">Index of the point.</param>
    private void EnforceMode(int index)
    {
        int modeIndex = (index + 1) / 3;

        BezierControlPointMode mode = modes[modeIndex];
        if(mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1))
        {
            return;
        }

        int middleIndex = modeIndex * 3;
        int fixedIndex, enforcedIndex;

        if(index <= middleIndex)
        {
            fixedIndex = middleIndex - 1;

            if (fixedIndex < 0)
                fixedIndex = points.Length - 2;

            enforcedIndex = middleIndex + 1;

            if (enforcedIndex >= points.Length)
                enforcedIndex = 1;
        }

        else
        {
            fixedIndex = middleIndex + 1;

            if (fixedIndex >= points.Length)
                fixedIndex = 1;

            enforcedIndex = middleIndex - 1;

            if(enforcedIndex < 0)
                enforcedIndex = points.Length - 2;
        }

        Vector3 middle = points[middleIndex];
        Vector3 enforcedTangent = middle - points[fixedIndex];
        if(mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
        }

        points[enforcedIndex] = middle + enforcedTangent;
    }

    /// <summary>
    /// Returns the ammount of curves in the spline.
    /// </summary>
    public int CurveCount
    {
        get { return (points.Length - 1) / 3; }
    }

    /// <summary>
    /// Get position of point on the curve.
    /// </summary>
    /// <param name="t">The point along the curve.</param>
    /// <returns>Position of point on curve.</returns>
    public Vector3 GetPoint(float t)
    {
        int i;

        if(t >= 1f)
        {
            t = 1f;
            i = points.Length - 4;
        }

        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(Bezier.GetPoint(
            points[i], points[i + 1], points[i + 2], points[i + 3], t));
    }

    /// <summary>
    /// Get the velocity at a point along the curve.
    /// </summary>
    /// <param name="t">The point on the curve.</param>
    /// <returns>Direction of velocity at the point t.</returns>
    public Vector3 GetVelocity(float t)
    {
        int i;

        if (t >= 1f)
        {
            t = 1f;
            i = points.Length - 4;
        }

        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(
            Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    /// <summary>
    /// Adds a curve to the spline.
    /// </summary>
    public void AddCurve()
    {
        Vector3 point = points[points.Length - 1];
        Array.Resize(ref points, points.Length + 3);

        point.x += 1f;
        points[points.Length - 3] = point;

        point.x += 1f;
        points[points.Length - 2] = point;

        point.x += 1f;
        points[points.Length - 1] = point;

        Array.Resize(ref modes, modes.Length + 1);
        modes[modes.Length - 1] = modes[modes.Length - 2];
        EnforceMode(points.Length - 4);

        if(loop)
        {
            points[points.Length - 1] = points[0];
            modes[modes.Length - 1] = modes[0];
            EnforceMode(0);
        }
    }

    /// <summary>
    /// Resets the spline, called when you rightclick the component and hit "Reset".
    /// </summary>
    public void Reset()
    {
        points = new Vector3[]
        {
            new Vector3(1f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(4f, 0f, 0f)
        };

        modes = new BezierControlPointMode[]
        {
            BezierControlPointMode.Free,
            BezierControlPointMode.Free
        };
    }
}
