using UnityEngine;
using System.Collections;

public class Bubbles : MonoBehaviour
{
    private ParticleSystem system;
    private ParticleSystem.Particle[] emittedParticles;

    [SerializeField]
    private int currentNumberOfParticles = 0;

	// Use this for initialization
	void Start ()
    {
        system = GetComponent<ParticleSystem>();
        Debug.Log(system.name);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        emittedParticles = new ParticleSystem.Particle[system.particleCount];

        currentNumberOfParticles = system.GetParticles(emittedParticles);

        for (int i = 0; i < currentNumberOfParticles; i++)
        {
            GameObject bubbel = (GameObject)Instantiate(Resources.Load<GameObject>("Level/ToxicWaste/Bubble"), emittedParticles[i].position + new Vector3(system.shape.box.x / 4, 0, -system.shape.box.z / 2), Quaternion.identity, transform);

            float scale = Random.Range(0.25f, 0.75f);

            bubbel.transform.localScale = new Vector3(scale, scale, scale);

            emittedParticles[i].lifetime = 0;
        }

        system.SetParticles(emittedParticles, emittedParticles.Length);
	}
}
