using UnityEngine;
using System.Collections;

public class HelperFunctions : MonoBehaviour
{
    #region Time and time-formats

    /// <summary>
    /// Converts a float to a string with the debugging time format: "00:00:00" (Hours, minuts and seconds)
    /// </summary>
    /// <param name="floatTime">The time in seconds that should be converted to a string.</param>
    /// <returns></returns>
    public static string ConvertToTimeFormatDebug(float floatTime)
    {
        // Number values used for calculations.
        int seconds;
        int minuts;
        int hours;

        // String values used to build the final return-string.
        string secondsString = "00";
        string minutsString = "00";
        string hoursString = "00";

        // Calculates the amount of whole hours.
        hours = (int)Mathf.Floor(floatTime / 3600);

        // Calculates the amount of whole minuts...
        minuts = (int)Mathf.Floor(floatTime / 60);

        //... and if there are more than 60 minuts in total...
        if (minuts > 60)
        {
            //... minuts are equal to the total amount of minuts modulo 60 (amount of minuts in an hour)
            minuts = minuts % 60;
        }

        // Calculates the amount of whole seconds.
        seconds = (int)(floatTime % 60);

        // If the number is less than 10, a '0' is put infront of it.
        if (seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        // Otherwise the number is unchanged but is converted to a string.
        else
        {
            secondsString = seconds.ToString();
        }

        if (minuts < 10)
        {
            minutsString = "0" + minuts;
        }
        else
        {
            minutsString = minuts.ToString();
        }

        if (hours < 10)
        {
            hoursString = "0" + hours;
        }
        else
        {
            hoursString = hours.ToString();
        }

        // Returns a string combined from the three string we just created.
        return hoursString + ":" + minutsString + ":" + secondsString;
    }

    /// <summary>
    /// Converts a float to a string with the default time format: "00:00" (Minuts and seconds)
    /// </summary>
    /// <param name="floatTime">The time in seconds that will be converted to a string.</param>
    /// <returns></returns>
    public static string ConvertToTimeFormat(float floatTime)
    {
        // Number values used for calculations.
        int seconds;
        int minuts;

        // String values used to build the final return-string.
        string secondsString = "00";
        string minutsString = "00";

        // Calculates the amount of whole minuts...
        minuts = (int)Mathf.Floor(floatTime / 60);

        // Calculates the amount of whole seconds.
        seconds = (int)(floatTime % 60);

        // If the number is less than 10, a '0' is put infront of it.
        if (seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        // Otherwise the number is unchanged but is converted to a string.
        else
        {
            secondsString = seconds.ToString();
        }

        if (minuts < 10)
        {
            minutsString = "0" + minuts;
        }
        else
        {
            minutsString = minuts.ToString();
        }

        // Returns a string combined from the three string we just created.
        return minutsString + ":" + secondsString;
    }

    #endregion

    #region Vectors
    /// <summary>
    /// Returns the normalized direction vector from point a to point b.
    /// </summary>
    /// <param name="a">The position the direction is calculated from.</param>
    /// <param name="b">The position the direction is calculated to.</param>
    /// <returns>The normalized direction vector.</returns>
    public static Vector3 DirectionFromTo(Vector3 a, Vector3 b)
    {
        return (b - a).normalized;
    }

    /// <summary>
    /// Returns the distance from point a to point b.
    /// </summary>
    /// <param name="a">The position the distance is calculated from.</param>
    /// <param name="b">The position the distance is calculated to.</param>
    /// <returns>The distance between the to points.</returns>
    public static float DistanceFromTo(Vector3 a, Vector3 b)
    {
        return (b - a).magnitude;
    }

    #endregion

    #region Gizmos
    /// <summary>
    /// Draws a Gizmo line with an arrow pointing in the forward direction.
    /// </summary>
    /// <param name="from">The position to draw from.</param>
    /// <param name="to">The position to draw to.</param>
    /// <param name="color">The color of the drawn line.</param>
    public static void GizmoLineWithDirection(Vector3 from, Vector3 to, Color color)
    {
        // Saves the old color so it can be reset
        Color oldColor = Gizmos.color;

        // Changes the color to the specified color
        Gizmos.color = color;
        // Draws a line between the 'from' and 'to' positions
        Gizmos.DrawLine(from, to);

        // Finds the distance and direction of the 'from-to' vector
        float distance = DistanceFromTo(from, to);
        Vector3 direction = DirectionFromTo(from, to);

        // Findes the mid-point of the vector
        Vector3 pointOfArrow = from + direction * (distance / 2);

        // The angle between the main line and one of the arrow-head lines
        float arrowHeadAngle = 45;

        // Creates the arrow-head lines direction vectors
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);

        // Draws the arrow head
        Gizmos.DrawRay(pointOfArrow + direction, right.normalized);
        Gizmos.DrawRay(pointOfArrow + direction, left.normalized);

        // Resets the color
        Gizmos.color = oldColor;
    }
    #endregion
}