using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private Image logo;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    [Tooltip("The time in seconds it takes for the logo on the splash screen to fade in and out. This is also the time between the fade in ends to the fade out starts.")]
    private float duration = 2;

    private bool fadeInDone = false;
    private bool waitDone = false;
    private bool fadeOutDone = false;
    private bool skipSplashScreen = false;

    // Use this for initialization
    private void Start()
    {
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0);

        StartCoroutine(CoroutineFadeIn(duration));
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player presses escape the splash screen is skipped.
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            skipSplashScreen = true;
        }

        // If the fade in is done the wait is started and fadeInDone is set to false to avoid running this again.
        if (fadeInDone)
        {
            StartCoroutine(CoroutineWait(duration));
            fadeInDone = false;
        }
        // Same as above only this time it starts the fade out when the wait is done.
        else if (waitDone)
        {
            StartCoroutine(CoroutineFadeOut(duration));
            waitDone = false;
        }

        // If the fade out is done or the splash screen is skipped...
        if (fadeOutDone || skipSplashScreen)
        {
            //Stops all the coroutines
            StopAllCoroutines();

            // Turns off the splash screen
            gameObject.SetActive(false);

            // Turns on the main menu
            mainMenu.SetActive(true);

            // Loads the main menu scene
            SceneManager.LoadScene("Menu");
        }
    }

    /// <summary>
    /// Coroutine used to fade in the logo over a specified amount of seconds.
    /// </summary>
    /// <param name="timeInSeconds">The time in seconds it should take to fade in.</param>
    public IEnumerator CoroutineFadeIn(float timeInSeconds)
    {
        float alpha = 0;
        float elapsedTime = 0;

        while (alpha < 1)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Sin((Mathf.PI / 2) * elapsedTime / timeInSeconds);
            
            if (elapsedTime > timeInSeconds)
            {
                alpha = 1;
            }

            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, alpha);

            yield return null;
        }

        fadeInDone = true;
    }

    /// <summary>
    /// Coroutine used to fade out the logo over a specified amount of seconds.
    /// </summary>
    /// <param name="timeInSeconds">The time in seconds it should take to fade out.</param>
    /// <returns></returns>
    public IEnumerator CoroutineFadeOut(float timeInSeconds)
    {
        float alpha = 1;
        float elapsedTime = 0;

        while (alpha > 0)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Cos((Mathf.PI / 2) * elapsedTime / timeInSeconds);

            if (elapsedTime > timeInSeconds)
            {
                alpha = 0;
            }

            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, alpha);

            yield return null;
        }

        fadeOutDone = true;
    }

    /// <summary>
    /// Coroutine used to wait for a specified amount of seconds.
    /// </summary>
    /// <param name="timeInSeconds">The time in seconds the coroutine should wait.</param>
    public IEnumerator CoroutineWait(float timeInSeconds)
    {
        float elapsedTime = 0;

        while (elapsedTime < timeInSeconds)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        waitDone = true;
    }
}
