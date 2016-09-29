using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtitleControl : MonoBehaviour
{
    private bool isDisplayed = false;
    private string line = "";

    [SerializeField]
    private Text subtitles;

    [SerializeField]
    private SubtitleSetting subtitleSetting;

    //private bool subtitlesEnabled = false;

    //public bool SubtitlesEnabled
    //{
    //    get { return subtitlesEnabled; }
    //}

    private void Update()
    {
        //if (subtitlesEnabled != subtitleSetting.SubtitlesEnabled)
        //{
        //    subtitlesEnabled = subtitleSetting.SubtitlesEnabled;
        //}
    }


    /// <summary>
    /// This is the method other scripts should call in order to display subtitles.
    /// </summary>
    /// <param name="subName">The name of the subtitle</param>
    /// <param name="levelName">The name of the level</param>
    /// <param name="duration">The amount of time the subtitle is displayed. Time is in seconds</param>
    public void StartSub(string subName, string levelName, float duration)
    {
        //Starts the coroutine if there is no subtitle currently being displayed
        //Starts a coroutine of the DisplaySubtitles method
        StartCoroutine(DisplaySubtitles(subName, levelName, duration));

    }

    /// <summary>
    /// Needs an int which is the voiceline that needs to be subbed.
    /// Calls the LoadSubtitle method from SubtileContainer.cs and looks through the contents of the subtitles list.
    /// If a subline with the same number as the sumNumber exists it's printed on screen.
    /// </summary>
    /// <param name="subName">The name of the subtitle</param>
    /// <param name="duration">The amount of time the subtitle is displayed. Time is in seconds</param>
    private IEnumerator DisplaySubtitles(string subName, string levelName, float duration)
    {
        line = "";
        SubtitleContainer sc = SubtitleContainer.LoadSubtitle(levelName);

        //if (subtitlesEnabled == true)
        //{
            //Looks through the contents of the subtitles List for an exact match of the number given when the method was called.
            foreach (Subtitle subtitle in sc.subtitles)
            {
                if (subtitle.name == (subName))
                {
                    line = subtitle.voiceLine;
                    break;
                }
            }
        //}

        //If the line is not found a debug log is mad. If the line is found it's displayed and the isDisplayed bool is set to true
        if (line == "")
        {
            Debug.Log("SubtitleControl.cs: Subtitle not found!");
        }
        else
        {
            subtitles.text = line;

            EnableSubtitle();

            isDisplayed = true;
        }

        //Stops the coroutine until a certain amount of time has passed
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            yield return null;
        }

        //When the amount of time has passed the subtitle disappears from the screen and the isDisplayed bool is set to false
        DisableSubtitel();
        isDisplayed = false;
    }

    /// <summary>
    /// Enables subtitles
    /// </summary>
    private void EnableSubtitle()
    {
        subtitles.enabled = true;
    }

    /// <summary>
    /// Disables subtitles
    /// </summary>
    private void DisableSubtitel()
    {
        subtitles.enabled = false;
    }
}
