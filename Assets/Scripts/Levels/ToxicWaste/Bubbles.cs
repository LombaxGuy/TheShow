using System;
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
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        emittedParticles = new ParticleSystem.Particle[system.particleCount];

        currentNumberOfParticles = system.GetParticles(emittedParticles);

        for (int i = 0; i < currentNumberOfParticles; i++)
        {
            GameObject bubble = (GameObject)Instantiate(Resources.Load<GameObject>("Level/ToxicWaste/Bubble"), system.transform.position + emittedParticles[i].position, Quaternion.identity, transform);

            bubble.GetComponent<Animator>().SetBool("bigBubble", Convert.ToBoolean(UnityEngine.Random.Range(0, 1))); 

            emittedParticles[i].lifetime = 0;
        }

        system.SetParticles(emittedParticles, emittedParticles.Length);
	}
}
