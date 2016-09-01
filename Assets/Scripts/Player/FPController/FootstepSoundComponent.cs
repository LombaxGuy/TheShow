using UnityEngine;
using System.Collections;

public enum GroundType {None, Dirt, Grass, Metal, Water, Wood, Concrete, Cloth };

public class FootstepSoundComponent : MonoBehaviour
{
    [Header("- Walking/Running Sound -")]
    // Serialized Fields
    [SerializeField]
    [Tooltip("Sounds played when the player walks on dirt.")]
    private AudioClip[] dirtSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on grass.")]
    private AudioClip[] grassSounds;
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
    [Tooltip("Sounds played when the player walks on concrete.")]
    private AudioClip[] concreteSounds;
    [SerializeField]
    [Tooltip("Sounds played when the player walks on cloth.")]
    private AudioClip[] clothSounds;

    private AudioSource audioSource;
    private float rayCastLength = 1.001f;

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

            case GroundType.Grass:
                if (grassSounds.Length > 0)
                {
                    audioSource.PlayOneShot(grassSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.Wood:
                if (woodSounds.Length > 0)
                {
                    audioSource.PlayOneShot(woodSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.Cloth:
                if (clothSounds.Length > 0)
                {
                    audioSource.PlayOneShot(clothSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.Concrete:
                if (concreteSounds.Length > 0)
                {
                    audioSource.PlayOneShot(concreteSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.Water:
                if (waterSounds.Length > 0)
                {
                    audioSource.PlayOneShot(waterSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            case GroundType.Metal:
                if (metalSounds.Length > 0)
                {
                    audioSource.PlayOneShot(metalSounds[Random.Range(0, dirtSounds.Length - 1)]);
                }
                break;

            default:
                Debug.Log("Played no sound because the ground type is 'None'. (Remember to set the correct tag)");
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
        if (Physics.Raycast(transform.parent.position, -transform.up, out hit, rayCastLength))
        {
            switch (hit.collider.tag)
            {
                case "Ground_Dirt":
                    groundType = GroundType.Dirt;

                    break;

                case "Ground_Grass":
                    groundType = GroundType.Grass;

                    break;

                case "Ground_Cloth":
                    groundType = GroundType.Cloth;

                    break;

                case "Ground_Wood":
                    groundType = GroundType.Wood;

                    break;

                case "Ground_Concrete":
                    groundType = GroundType.Concrete;

                    break;

                case "Ground_Water":
                    groundType = GroundType.Water;

                    break;

                case "Ground_Metal":
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