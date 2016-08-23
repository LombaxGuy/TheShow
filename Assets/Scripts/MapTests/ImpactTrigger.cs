using UnityEngine;
using System.Collections;

public class ImpactTrigger : MonoBehaviour {

    [SerializeField]
    private float force = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force ,ForceMode.Impulse);

    }
}
