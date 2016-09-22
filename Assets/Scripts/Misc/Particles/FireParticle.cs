using UnityEngine;
using System.Collections;

public class FireParticle : MonoBehaviour
{
    private Light fireLight;

    [SerializeField]
    private float changeIntensityInterval = 0.1f;

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
            fireLight.intensity = Random.Range(6.0f, 7.0f);
            yield return new WaitForSeconds(changeIntensityInterval);
        }
    }
}
