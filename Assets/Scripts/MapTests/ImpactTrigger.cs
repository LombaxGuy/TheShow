using UnityEngine;
using System.Collections;

public class ImpactTrigger : MonoBehaviour {

    //THIS SCRIPT IS TO BE USED WITH IMPACT TRIGGERS
    [SerializeField]
    private float force = 2000;

    /// <summary>
    /// See if player is hit and push the player
    /// </summary>
    /// <param name="other">player</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force ,ForceMode.Impulse);

    }
}
