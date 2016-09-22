using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    private float screenShakeAmount = 0.0f;
    private float distanceToPlayer = 0.0f;

    [SerializeField]
    [Tooltip("The maximum distance at which the shake animation will be played.")]
    private float screenShakeDistance = 10.0f;

    [SerializeField]
    [Tooltip("The object from which the screenshake distance will be calculated.")]
    private GameObject screenShakeCenter;

    private GameObject player;
    private Animator playerAnimator;

	// Use this for initialization
	private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();	
	}

    private void Update()
    {
        // If the screenShakeCenter is not set...
        if (screenShakeCenter == null)
        {
            //... use the position of this gameobject to calculate distanceToPlayer
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        }
        // If the screenShakeCenter is set...
        else
        {
            //... use the screenShakeCenter to calculate distanceToPlayer
            distanceToPlayer = Vector3.Distance(player.transform.position, screenShakeCenter.transform.position);
        }
    }

    /// <summary>
    /// Calculates the values of screenShakeAmount and set the layerWeight of the animator accordingly.
    /// </summary>
    private void SetScreenShakeValues()
    {      
        // Calculates screenShakeAmount 
        if (distanceToPlayer <= screenShakeDistance)
        {
            screenShakeAmount = 1 - (distanceToPlayer / screenShakeDistance);
        }
        else
        {
            screenShakeAmount = 0.0f;
        }

        // Set layerWeight
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Shake Layer"), screenShakeAmount);
    }

    /// <summary>
    /// Triggers the screenshake if the player is close enough to the object calling it.
    /// </summary>
    public void TriggerScreenShake()
    {
        // If the player is close enough...
        if (distanceToPlayer < screenShakeDistance)
        {
            //... set the values...
            SetScreenShakeValues();

            //... and set the values of the animator.
            playerAnimator.SetFloat("shakeAmount", screenShakeAmount);
            playerAnimator.SetTrigger("triggerShake");
        }
    }
}
