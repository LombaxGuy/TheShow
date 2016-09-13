using UnityEngine;
using System.Collections;

public class ImpactTrigger : MonoBehaviour {

    //THIS SCRIPT IS TO BE USED WITH IMPACT TRIGGERS
    [SerializeField]
    [Tooltip("2500 is default for a hard push")]
    private float force = 2500;

    

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
                    fpsComponent.StunForSeconds(0.5f);
                }  
            }
        }
    }
}
