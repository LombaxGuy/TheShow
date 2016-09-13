using UnityEngine;
using System.Collections;

public class ImpactTrigger : MonoBehaviour {

    //THIS SCRIPT IS TO BE USED WITH IMPACT TRIGGERS
    [SerializeField]
    [Tooltip("The force added to the player when the player is hit.")]
    private float force = 500;

    [SerializeField]
    [Tooltip("The amount of seconds the player is stunned when hit by the pusher.")]
    private float seconds = 0.5f;

    /// <summary>
    /// See if player is hit and push the player
    /// </summary>
    /// <param name="other">player</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            Transform otherTransform = other.transform.parent;

            if (otherTransform.tag == "Player")
            {
                FPSController fpsComponent = otherTransform.GetComponent<FPSController>();

                if (!fpsComponent.Stunned)
                {
                    otherTransform.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                    fpsComponent.StunForSeconds(seconds);
                }  
            }
        }
    }
}
