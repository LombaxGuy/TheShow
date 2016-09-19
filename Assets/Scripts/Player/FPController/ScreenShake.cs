using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    private float screenShakeAmount = 0.0f;
    private float distanceToPlayer = 0.0f;

    [SerializeField]
    private float screenShakeDistance = 10.0f;

    [SerializeField]
    private GameObject player;
    private Animator playerAnimator;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();	
	}

    private void SetScreenShakeValues()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= screenShakeDistance)
        {
            screenShakeAmount = 1 - (distanceToPlayer / screenShakeDistance);
        }
        else
        {
            screenShakeAmount = 0.0f;
        }

        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Shake Layer"), screenShakeAmount);
    }

    public void TriggerScreenShake()
    {
        SetScreenShakeValues();

        playerAnimator.SetFloat("shakeAmount", screenShakeAmount);
        playerAnimator.SetTrigger("triggerShake");
    }
}
