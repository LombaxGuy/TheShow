using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(ColorCorrectionCurves))]
[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class DeathFadeComponent : MonoBehaviour
{
    [SerializeField]
    // Number of updates per second
    private float fadeStepsPerSecond = 60;

    [SerializeField]
    // The time it takes in seconds to finish the fade. (roughly... the higer fadeStepsPerSecond is the longer it takes)
    private float timeInSeconds = 1;

    [SerializeField]
    // The saturation value when the fade is finished
    private float targetSaturation = 0;

    [SerializeField]
    // The vignette intensity when the fade is finished
    private float targetVignetting = 0.3f;

    [SerializeField]
    // The amount of blur when the fade is finished
    private float targetBlurredCorners = 0.9f;

    // ImageEffect components
    private ColorCorrectionCurves colorCorrectionEffect;
    private VignetteAndChromaticAberration vignette;

    // Default values
    private float defaultSaturation = 1;
    private float defaultVignetting = 0.1f;
    private float defaultBlurredCorners = 0;

    private bool continueFade = true;

    void OnEnable()
    {
        // Subscribes to events
        EventManager.OnPlayerDeath += OnPlayerDeath;
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        // Unsubscribes from events
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    /// <summary>
    /// Starts a coroutine that initiates the death fade when the OnPlayerDeath event is raised
    /// </summary>
    private void OnPlayerDeath()
    {
        StartCoroutine(DeathFade());
    }

    /// <summary>
    /// Calls the method ResetImageEffects when the OnPlayerRespawn event is raised
    /// </summary>
    private void OnPlayerRespawn()
    {
        ResetImageEffects();
    }

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        colorCorrectionEffect = GetComponent<ColorCorrectionCurves>();
        vignette = GetComponent<VignetteAndChromaticAberration>();
    }

    /// <summary>
    /// Starts a coroutine which fades to black and white and blurs the screen. Used for when the player dies.
    /// </summary>
    /// <returns>IEnumerator (This method starts a coroutine)</returns>
    public IEnumerator DeathFade()
    {
        // Calculating difference in the changed values
        float deltaSaturation = Mathf.Abs(colorCorrectionEffect.saturation - targetSaturation);
        float deltaVignetting = Mathf.Abs(vignette.intensity - targetVignetting);
        float deltaBlurredCorners = Mathf.Abs(vignette.blur - targetBlurredCorners);

        // Variable used to smooth out the vignette fade
        float loopNumber = 0;

        continueFade = true;

        // Fade to colors
        while ((colorCorrectionEffect.saturation > targetSaturation || 
                vignette.blur < targetBlurredCorners || 
                vignette.intensity < targetVignetting) &&
                continueFade)
        {
            // Only change the saturation if the current saturation is smaller than the target saturation
            if (colorCorrectionEffect.saturation < targetSaturation)
            {
                colorCorrectionEffect.saturation -= (deltaSaturation / fadeStepsPerSecond) / timeInSeconds;
            }
            // If the current saturation is larger than the target saturation
            else
            {
                colorCorrectionEffect.saturation = targetSaturation;
            }

            // Only change the amount of blur if the current amount is smaller than the target amount
            if (vignette.blur < targetBlurredCorners)
            {
                vignette.blur += (deltaBlurredCorners / fadeStepsPerSecond) / timeInSeconds;

                // If the current amount of blur is larger than the target amount 
                if (vignette.blur > targetBlurredCorners)
                {
                    vignette.blur = targetBlurredCorners;
                }
            }

            // Only change the vignette intensity if the current intensity is smaller than the target intensity
            if (vignette.intensity < targetVignetting)
            {
                loopNumber++;
                //float newDeltaIntensity = Mathf.Abs(vignette.intensity - targetVignetting);

                vignette.intensity = defaultVignetting + Mathf.Sin((Mathf.PI / 2) * timeInSeconds / fadeStepsPerSecond * loopNumber) * deltaVignetting;
            }
            // If the current vignette intensity is larger than the target intensity 
            else
            {
                vignette.intensity = targetVignetting;
            }

            // Coroutine continues from the yield keyword. Coroutine waits for an amount of time before continuing
            yield return new WaitForSeconds((float)(1 / fadeStepsPerSecond));
        }
    }

    /// <summary>
    /// Resets image effects changed by this script.
    /// </summary>
    public void ResetImageEffects()
    {
        continueFade = false;
        colorCorrectionEffect.saturation = defaultSaturation;
        vignette.intensity = defaultVignetting;
        vignette.blur = defaultBlurredCorners;
    }
}