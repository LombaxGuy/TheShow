using UnityEngine;
using System.Collections;

public class FireParticle : MonoBehaviour
{
    private Light fireLight;

    [SerializeField]
    private float changeIntensityInterval = 0.1f;

    [SerializeField]
    private float minIntensity = 3f;

    [SerializeField]
    private float maxIntensity = 3.5f;

    // Use this for initialization
    void Start()
    {
        fireLight = GetComponent<Light>();

        StartCoroutine(ChangeLightIntensity());
    }

    private IEnumerator ChangeLightIntensity()
    {
        while (true)
        {
            fireLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(changeIntensityInterval);
        }
    }
}
