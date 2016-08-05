using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class DeathFadeCC : MonoBehaviour
{
    [SerializeField]
    private float fadeStepsPerSecond = 10;

    [SerializeField]
    private float timeInSeconds = 1;

    [SerializeField]
    private float targetSaturation = 0;

    [SerializeField]
    private float targetVignetting = 0.3f;

    [SerializeField]
    private float targetBlurredCorners = 1;

    private ColorCorrectionCurves colorCorrectionEffect;
    private VignetteAndChromaticAberration vignette;
    private float defaultSaturation = 1;
    private float defaultVignetting = 0.1f;
    private float defaultBlurredCorners = 0;

    void Start()
    {
        colorCorrectionEffect = GetComponent<ColorCorrectionCurves>();
        vignette = GetComponent<VignetteAndChromaticAberration>();
    }

    public IEnumerator StartBlackAndWhiteCCFade()
    {
        float deltaSaturation = Mathf.Abs(colorCorrectionEffect.saturation - targetSaturation);
        float deltaVignetting = Mathf.Abs(vignette.intensity - targetVignetting);
        float deltaBlurredCorners = Mathf.Abs(vignette.blur - targetBlurredCorners);

        if (targetSaturation > colorCorrectionEffect.saturation) // Fade to black and white
        {
            Debug.Log("Entered if");
            while (colorCorrectionEffect.saturation < targetSaturation)
            {
                colorCorrectionEffect.saturation += (deltaSaturation / fadeStepsPerSecond) / timeInSeconds;

                yield return new WaitForSeconds((float)(1 / fadeStepsPerSecond));
            }
        }
        else // Fade to colors
        {
            while (colorCorrectionEffect.saturation > targetSaturation)
            {
                colorCorrectionEffect.saturation -= (deltaSaturation / fadeStepsPerSecond) / timeInSeconds;
                vignette.intensity += (deltaVignetting / fadeStepsPerSecond) / timeInSeconds;
                vignette.blur += (deltaBlurredCorners / fadeStepsPerSecond) / timeInSeconds;

                if (vignette.blur > 1)
                {
                    vignette.blur = 1;
                }

                yield return new WaitForSeconds((float)(1 / fadeStepsPerSecond));
            }
        }
    }

    public void ResetCC()
    {
        colorCorrectionEffect.saturation = defaultSaturation;
        vignette.intensity = defaultVignetting;
        vignette.blur = defaultBlurredCorners;
    }
}
