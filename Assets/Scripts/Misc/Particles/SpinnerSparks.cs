using UnityEngine;
using System.Collections;

public class SpinnerSparks : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem system;

    [SerializeField]
    private Light lightSource;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EmitParticleOnHit")
        {
            system.Play();
            lightSource.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EmitParticleOnHit")
        {
            lightSource.enabled = false;
        }
    }
}
