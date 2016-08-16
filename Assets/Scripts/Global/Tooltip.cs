using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum TooltipPosition
{
    TopRight, TopCenter, TopLeft, CenterRight, CenterLeft, BottomRight, BottomCenter, BottomLeft
}

public class Tooltip : MonoBehaviour
{
    #region Singleton
    // Code for the singleton

    // The singleton instance
    private static Tooltip instance;

    /// <summary>
    /// Returns the instance of Tooltip
    /// </summary>
    public static Tooltip Instance
    {
        get
        {
            // If the instance is null the instance is created.
            if (instance == null)
            {
                GameObject newObject = new GameObject("Tooltip (Singleton)");
                instance = newObject.AddComponent<Tooltip>();          
            }

            // Returns the instance
            return instance;
        }
    }

    // Awake() used to initialize the singleton. Duplicates of the singleton is destroyed.
    private void Awake()
    {
        // If there are duplicates of the singleton destroy this
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        // Makes sure that the singleton is carried over from scene to scene
        DontDestroyOnLoad(this.gameObject);

        #region Not Singleton Code
        // Creates an array of all the canvases in the scene.
        Canvas[] temp = GameObject.FindObjectsOfType<Canvas>();

        // Runs through the array until the canvas with the correct name is found.
        for (int i = 0; i < temp.Length; i++)
        {
            // If the correct canvas is found the for-loop is broken.
            if (temp[i].gameObject.name == canvasName)
            {
                inGameUI = temp[i];
                break;
            }

            if (i == temp.Length - 1 && inGameUI == null)
            {
                // Prints a message to the debug log if no canvas in the scene has the specified name.
                Debug.Log("Warning: No canvas with the name '" + canvasName + "' was found in the scene. No UI can be displayed without a canvas.");
            }
        }

        // If the UI canvas is not null we find the child that should display text in the center of the screen.
        if (inGameUI != null)
        {
            // Finds the child with the specified name and gets the Text component.
            centerText = inGameUI.transform.Find(textName).GetComponent<Text>();
        }
        #endregion
    }
    #endregion

    // The canvas used to display the UI ingame
    private Canvas inGameUI;
    // The name of the canvas to look for
    private string canvasName = "InGameUI";

    private Text centerText;
    private string textName = "CenterText";

    private float borderPixelValue = 20;

    private float fadePixelMovement = 25;

    [SerializeField]
    [Tooltip("The time in seconds it takes to fade-in or fade-out.")]
    private float fadeTime = 0.5f;

    [SerializeField]
    [Tooltip("The color of the text. Always keep the alpha-channel on 0!")]
    // White with the alpha-channel set to 0 (transparent)
    private Color defaultColor = new Color(1, 1, 1, 0);

    /// <summary>
    /// Displays a text in the center of the screen.
    /// </summary>
    /// <param name="text">The text that will be displayed.</param>
    public void DisplayText(string text)
    {
        centerText.text = text;
    }

    /// <summary>
    /// Removes the text in the center of the screen created by the method DisplayText.
    /// </summary>
    public void ClearDisplayText()
    {
        centerText.text = "";
    }

    /// <summary>
    /// Display a tooltip on the screen for a specified amount of time. (Coroutine)
    /// </summary>
    /// <param name="tooltipText">The text to display in the tooltip.</param>
    /// <param name="seconds">The amount of seconds after the fade-in stops to the fadeout starts.</param>
    public void DisplayTooltipForSeconds(string tooltipText, float seconds)
    {
        StartCoroutine(CoroutineTooltipForSeconds(tooltipText, seconds));
    }

    /// <summary>
    /// Display a tooltip on the screen for a specified amount of time. (Coroutine)
    /// </summary>
    /// <param name="tooltipText">The text to display in the tooltip.</param>
    /// <param name="seconds">The amount of seconds after the fade-in stops to the fadeout starts.</param>
    /// <param name="tooltipPosition">The position on the screen the tooltip should be displayed.</param>
    public void DisplayTooltipForSeconds(string tooltipText, float seconds, TooltipPosition tooltipPosition)
    {
        StartCoroutine(CoroutineTooltipForSeconds(tooltipText, seconds, tooltipPosition));
    }

    /// <summary>
    /// A coroutine that displays a tooltip on screen.
    /// </summary>
    /// <param name="tooltipText">The text displayed in the tooltip.</param>
    /// <param name="seconds">The time the tooltip is visible.</param>
    private IEnumerator CoroutineTooltipForSeconds(string tooltipText, float seconds)
    {
        // Instantiates a UI text element prefab.
        Text uiText = Instantiate(Resources.Load<Text>("UI/Elements/Tooltip"));

        // Sets the parrent of the text element to the correct canvas.
        uiText.transform.SetParent(inGameUI.transform, false);

        // Moves the text element to the default starting position.
        uiText.rectTransform.anchorMin = new Vector2(0, 1);
        uiText.rectTransform.anchorMax = new Vector2(1, 1);
        uiText.rectTransform.pivot = new Vector2(0.5f, 1);
        uiText.alignment = TextAnchor.UpperCenter;
        uiText.rectTransform.anchoredPosition = new Vector2(0, -(borderPixelValue + fadePixelMovement));

        // Sets the color of the text to the default color.
        uiText.color = defaultColor;

        // Sets the text of the tooltip
        uiText.text = tooltipText;

        // Starts a coroutine for the fade-in and one for the movement.
        StartCoroutine(FadeIn(uiText));
        StartCoroutine(MoveToOverSeconds(uiText, uiText.rectTransform.anchoredPosition + new Vector2(0, fadePixelMovement), 0.5f, true));

        // Makes this coroutine wait for the time it takes to fade in + the time the text should be displayed.
        for (float i = 0; i < seconds + fadeTime; i += Time.deltaTime)
        {
            yield return null;
        }

        // Starts a coroutine for the fade-out and one for the movement.
        StartCoroutine(FadeOut(uiText));
        StartCoroutine(MoveToOverSeconds(uiText, uiText.rectTransform.anchoredPosition + new Vector2(0, 2 * fadePixelMovement), 0.5f, false));
    }

    /// <summary>
    /// A coroutine that displays a tooltip on screen at a specified position.
    /// </summary>
    /// <param name="tooltipText">The text displayed in the tooltip.</param>
    /// <param name="seconds">The time the tooltip is visible.</param>
    /// <param name="tooltipPosition">The position of the tooltip on the screen.</param>
    private IEnumerator CoroutineTooltipForSeconds(string tooltipText, float seconds, TooltipPosition tooltipPosition)
    {
        // Instantiates a UI text element prefab.
        Text uiText = Instantiate(Resources.Load<Text>("UI/Elements/Tooltip"));

        // Sets the parrent of the text element to the correct canvas.
        uiText.transform.SetParent(inGameUI.transform, false);

        // Moves the text element to the default starting position.
        switch (tooltipPosition)
        {
            case TooltipPosition.TopRight:
                uiText.rectTransform.anchorMin = new Vector2(0, 1);
                uiText.rectTransform.anchorMax = new Vector2(1, 1);
                uiText.rectTransform.pivot = new Vector2(1, 1);
                uiText.alignment = TextAnchor.UpperRight;
                uiText.rectTransform.anchoredPosition = new Vector2(-borderPixelValue, -(borderPixelValue + fadePixelMovement));

                break;

            case TooltipPosition.TopCenter:
                uiText.rectTransform.anchorMin = new Vector2(0, 1);
                uiText.rectTransform.anchorMax = new Vector2(1, 1);
                uiText.rectTransform.pivot = new Vector2(0.5f, 1);
                uiText.alignment = TextAnchor.UpperCenter;
                uiText.rectTransform.anchoredPosition = new Vector2(0, -(borderPixelValue + fadePixelMovement));

                break;

            case TooltipPosition.TopLeft:
                uiText.rectTransform.anchorMin = new Vector2(0, 1);
                uiText.rectTransform.anchorMax = new Vector2(1, 1);
                uiText.rectTransform.pivot = new Vector2(0, 0);
                uiText.alignment = TextAnchor.UpperLeft;
                uiText.rectTransform.anchoredPosition = new Vector2(borderPixelValue, -(borderPixelValue + fadePixelMovement));

                break;

            case TooltipPosition.CenterRight:
                uiText.rectTransform.anchorMin = new Vector2(0, 0.5f);
                uiText.rectTransform.anchorMax = new Vector2(1, 0.5f);
                uiText.rectTransform.pivot = new Vector2(1, 1);
                uiText.alignment = TextAnchor.MiddleRight;
                uiText.rectTransform.anchoredPosition = new Vector2(-borderPixelValue, -borderPixelValue);

                break;

            case TooltipPosition.CenterLeft:
                uiText.rectTransform.anchorMin = new Vector2(0, 0.5f);
                uiText.rectTransform.anchorMax = new Vector2(1, 0.5f);
                uiText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                uiText.alignment = TextAnchor.MiddleLeft;
                uiText.rectTransform.anchoredPosition = new Vector2(borderPixelValue, -borderPixelValue);

                break;

            case TooltipPosition.BottomRight:
                uiText.rectTransform.anchorMin = new Vector2(0, 0);
                uiText.rectTransform.anchorMax = new Vector2(1, 0);
                uiText.rectTransform.pivot = new Vector2(1, 0);
                uiText.alignment = TextAnchor.LowerRight;
                uiText.rectTransform.anchoredPosition = new Vector2(-borderPixelValue, borderPixelValue);

                break;

            case TooltipPosition.BottomCenter:
                uiText.rectTransform.anchorMin = new Vector2(0, 0);
                uiText.rectTransform.anchorMax = new Vector2(1, 0);
                uiText.rectTransform.pivot = new Vector2(0.5f, 0);
                uiText.alignment = TextAnchor.LowerCenter;
                uiText.rectTransform.anchoredPosition = new Vector2(0, borderPixelValue);

                break;

            case TooltipPosition.BottomLeft:
                uiText.rectTransform.anchorMin = new Vector2(0, 0);
                uiText.rectTransform.anchorMax = new Vector2(1, 0);
                uiText.rectTransform.pivot = new Vector2(0, 0);
                uiText.alignment = TextAnchor.LowerLeft;
                uiText.rectTransform.anchoredPosition = new Vector2(borderPixelValue, borderPixelValue);

                break;
        }

        // Sets the color of the text to the default color.
        uiText.color = defaultColor;

        // Sets the text of the tooltip
        uiText.text = tooltipText;

        // Starts a coroutine for the fade-in and one for the movement.
        StartCoroutine(FadeIn(uiText));
        StartCoroutine(MoveToOverSeconds(uiText, uiText.rectTransform.anchoredPosition + new Vector2(0, fadePixelMovement), 0.5f, true));

        // Makes this coroutine wait for the time it takes to fade in + the time the text should be displayed.
        for (float i = 0; i < seconds + fadeTime; i += Time.deltaTime)
        {
            yield return null;
        }

        // Starts a coroutine for the fade-out and one for the movement.
        StartCoroutine(FadeOut(uiText));
        StartCoroutine(MoveToOverSeconds(uiText, uiText.rectTransform.anchoredPosition + new Vector2(0, 2 * fadePixelMovement), 0.5f, false));
    }

    /// <summary>
    /// Fades a given text in.
    /// </summary>
    /// <param name="uiText">The text to fade-in.</param>
    private IEnumerator FadeIn(Text uiText)
    {
        Color newColor;

        // Sets the newAlpha to the current alpha
        float newAlpha = uiText.color.a;

        // 't' variable used to Lerp the alpha
        float t = uiText.color.a;

        // While the target alpha has not been reached yet. If 't' is 1 the target alpha has been reached.
        while (t < 1.0f)
        {
            // Lerp the alpha-value
            newAlpha = Mathf.Lerp(0, 1, t);

            // Decrease 't' with the time since last frame divided by the total time it should take to fade out.
            t += Time.deltaTime / fadeTime;

            // Creates a new color with the new alpha value
            newColor = new Color(uiText.color.r, uiText.color.g, uiText.color.b, newAlpha); // Changing the alpha of the text color

            // Sets the color of the UI text element to the the new color.
            uiText.color = newColor;

            yield return null;
        }
    }

    /// <summary>
    /// Fades a given text out. Destroys the gameObject upon completion.
    /// </summary>
    /// <param name="uiText">The text to fade-out</param>
    private IEnumerator FadeOut(Text uiText)
    {
        Color newColor;

        // Sets the newAlpha to the current alpha
        float newAlpha = uiText.color.a;

        // 't' variable used to Lerp the alpha
        float t = uiText.color.a;

        // While 't' is greater than 0 the alpha is reduced. If 't' is 0 the alpha is also 0.
        while (t > 0.0f)
        {
            // Lerp the alpha-value
            newAlpha = Mathf.Lerp(0, 1, t);

            // Decrease 't' with the time since last frame divided by the total time it should take to fade out.
            t -= Time.deltaTime / fadeTime;

            // Creates a new color with the new alpha value
            newColor = new Color(uiText.color.r, uiText.color.g, uiText.color.b, newAlpha); // Changing the alpha of the text color

            // Sets the color of the UI text element to the the new color.
            uiText.color = newColor;

            yield return null;
        }

        // Removes the text when the fade-out is done.
        Destroy(uiText.gameObject);
    }

    /// <summary>
    /// Moves a UI text from one position to another over a given amount of time.
    /// </summary>
    /// <param name="uiText">The UI text element to move.</param>
    /// <param name="to">The position the object should move to.</param>
    /// <param name="seconds">The time it takes for the object to move to the target position.</param>
    /// <param name="smooth">Should the text slow down as it approach the target position.
    /// If true the object will start with high speed and slowdown towards the end. 
    /// If false the object will move with a constant speed.</param>
    private IEnumerator MoveToOverSeconds(Text uiText, Vector2 to, float seconds, bool smooth)
    {
        Vector2 newPosition;

        // Start position of the UI text element
        Vector2 startPosition = uiText.rectTransform.anchoredPosition;

        // 't' variable used to Lerp the positions
        float t = 0;

        // While we haven't reached the target position yet. If 't' is 1.0f the target has been reached.
        while (t < 1.0f)
        {
            // If the UI text element is not null. It is null if it has been destroyed by the fade-out method.
            if (uiText != null)
            {
                // If the smooth option is true we use the current position as one of the values to Lerp with...
                if (smooth)
                {
                    newPosition = Vector2.Lerp(uiText.rectTransform.anchoredPosition, to, t);
                }
                // ... Otherwise we use the starting position of the UI text element.
                else
                {
                    newPosition = Vector2.Lerp(startPosition, to, t);
                }

                // Increase 't' with the time since last frame divided by the total time it should take.
                t += Time.deltaTime / seconds;

                // Sets the position of the UI text to the new position
                uiText.rectTransform.anchoredPosition = newPosition;

                yield return null;
            }
            // If the UI text element has been destroyed
            else
            {
                yield break;
            }
        }
    }
}
