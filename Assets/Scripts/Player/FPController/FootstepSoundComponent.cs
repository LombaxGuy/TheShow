using UnityEngine;
using System.Collections;

public enum GroundType {None, Dirt, Metal, Water, Wood, RottenWood, Concrete, Cloth };

public class FootstepSoundComponent : MonoBehaviour
{
    [Header("- Walking/Running Sound -")]
    // Serialized Fields
    [SerializeField]
    [Tooltip("Sounds played when the player walks on dirt.")]
    private AudioClip[] dirtSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on metal.")]
    private AudioClip[] metalSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks in water.")]
    private AudioClip[] waterSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on wood.")]
    private AudioClip[] woodSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on rotten wood.")]
    private AudioClip[] rottenWoodSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on concrete.")]
    private AudioClip[] concreteSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on cloth.")]
    private AudioClip[] clothSounds;

    private AudioSource audioSource;
    private float rayCastLength = 1.01f;

    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the footstep sound based on the ground type.
    /// </summary>
    public void PlaySound()
    {
        switch (GetCurrentGroundType())
        {
            case GroundType.Dirt:
                if (dirtSounds.Length > 0)
                {
                    audioSource.PlayOneShot(dirtSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.RottenWood:
                if (rottenWoodSounds.Length > 0)
                {
                    audioSource.PlayOneShot(rottenWoodSounds[Random.Range(0, rottenWoodSounds.Length - 1)]);
                }
                break;

            case GroundType.Wood:
                if (woodSounds.Length > 0)
                {
                    audioSource.PlayOneShot(woodSounds[Random.Range(0, woodSounds.Length - 1)]);
                }
                break;

            case GroundType.Cloth:
                if (clothSounds.Length > 0)
                {
                    audioSource.PlayOneShot(clothSounds[Random.Range(0, clothSounds.Length - 1)]);
                }
                break;

            case GroundType.Concrete:
                if (concreteSounds.Length > 0)
                {
                    audioSource.PlayOneShot(concreteSounds[Random.Range(0, concreteSounds.Length - 1)]);
                }
                break;

            case GroundType.Water:
                if (waterSounds.Length > 0)
                {
                    audioSource.PlayOneShot(waterSounds[Random.Range(0, waterSounds.Length - 1)]);
                }
                break;

            case GroundType.Metal:
                if (metalSounds.Length > 0)
                {
                    audioSource.PlayOneShot(metalSounds[Random.Range(0, metalSounds.Length - 1)]);
                }
                break;

            default:
                Debug.Log("Played no sound because the ground type is 'None'. (Remember to set the correct physic material)");
                break;
        }
    }

    /// <summary>
    /// Returns the ground type the player is currently standing on.
    /// </summary>
    /// <returns></returns>
    private GroundType GetCurrentGroundType()
    {
        GroundType groundType = GroundType.None;

        RaycastHit hit;

        if (Physics.Raycast(transform.parent.parent.position, -transform.up, out hit, rayCastLength))
        {
            switch (hit.collider.material.name)
            {
                case "Ground_Dirt (Instance)":
                    groundType = GroundType.Dirt;

                    break;

                case "Ground_RottenWood (Instance)":
                    groundType = GroundType.RottenWood;

                    break;

                case "Ground_Cloth (Instance)":
                    groundType = GroundType.Cloth;

                    break;

                case "Ground_Wood (Instance)":
                    groundType = GroundType.Wood;

                    break;

                case "Ground_Concrete (Instance)":
                    groundType = GroundType.Concrete;

                    break;

                case "Ground_Water (Instance)":
                    groundType = GroundType.Water;

                    break;

                case "Ground_Metal (Instance)":
                    groundType = GroundType.Metal;

                    break;

                default:
                    groundType = GroundType.None;

                    break;
            }
        }

        //Debug.Log(groundType);
        return groundType;
    }
}