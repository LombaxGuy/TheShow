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
        if (Input.GetKeyDown(KeyBindings.KeyEscape))
        {
            skipSplashScreen = true;
        }

        if (fadeInDone)
        {
            StartCoroutine(CoroutineWait(duration));
            fadeInDone = false;
        }
        else if (waitDone)
        {
            StartCoroutine(CoroutineFadeOut(duration));
            waitDone = false;
        }

        if (fadeOutDone || skipSplashScreen)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Menu");
            mainMenu.SetActive(true);
            enabled = false;
        }
    }

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
