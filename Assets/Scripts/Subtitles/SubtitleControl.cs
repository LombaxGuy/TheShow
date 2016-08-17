using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtitleControl : MonoBehaviour
{
    private static SubtitleControl instance;
    private Text subtitles;
    private string textName = "Subtitles";
    private Canvas subtitleUI;
    private string canvasName = "SubtitleUI";
    private bool isDisplayed = false;
    private string line = "";

    public static SubtitleControl Instance
    {
        get
        {
            //If the singleton dosen't exist it creates a new one.
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("Subtitle(Singleton)");
                instance = newGameObject.AddComponent<SubtitleControl>();
            }
            return instance;
        }

    }

    //The awake method which ensures that duplicates of the object is destroyed and the script that finds the desired canvas to use for subs
    private void Awake()
    {
        //Ensures that only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        //Creates an array of Canvas 
        Canvas[] temp = FindObjectsOfType<Canvas>();

        for (int i = 0; i < temp.Length; i++)
        {
            //Searches for a Canvas with the desired name and breaks the loop if it's found.
            if (temp[i].name == canvasName)
            {
                subtitleUI = temp[i];
                break;
            }

            //If no Canvas is found a debug.log is made
            if (i == temp.Length && subtitleUI == null)
            {
                Debug.Log("SubtitleControl.cs: No Canvas with the name '" + canvasName + "' could be found in the scene!");
            }
        }

        //If a Canvas is found, subtitles is set to a text component of a child object
        if (subtitleUI != null)
        {
            subtitles = subtitleUI.transform.Find(textName).GetComponent<Text>();
            subtitles.text = line;
        }
    }

    /// <summary>
    /// This is the method other scripts should call in order to display subtitles.
    /// </summary>
    /// <param name="subNumber"></param>
    /// <param name="duration"></param>
    public void StartSub(int subNumber, float duration)
    {
        //Starts the coroutine if there is no subtitle currently being displayed
        if (!isDisplayed)
        {
            //Starts a coroutine of the DisplaySubtitles method
            StartCoroutine(DisplaySubtitles(subNumber, duration));
        }
    }

    /// <summary>
    /// Needs an int which is the voiceline that needs to be subbed
    /// Calls the LoadSubtitle method from SubtileContainer.cs and looks through the contents of the subtitles list
    /// If a subline with the same number as the sumNumber exists it's printed on screen
    /// </summary>
    /// <param name="subNumber"></param>
    /// <param name="duration"></param>
    private IEnumerator DisplaySubtitles(int subNumber, float duration)
    {
        line = "";
        SubtitleContainer sc = SubtitleContainer.LoadSubtitle();

        //Looks through the contents of the subtitles List for an exact match of the number given when the method was called.
        foreach (Subtitle subtitle in sc.subtitles)
        {
            if (subtitle.name == ("sub" + subNumber))
            {
                line = subtitle.voiceLine;
                break;
            }
        }

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
