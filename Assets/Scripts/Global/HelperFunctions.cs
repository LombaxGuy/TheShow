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
}