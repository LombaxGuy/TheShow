using UnityEngine;
using System.Collections;

public class EmitParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particleSystems;

    public void StartEmmitting()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }
}
