using UnityEngine;
using System.Collections;

public class ConveyorBeltStartTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Door")
        {
            if(other.GetComponent<DoorBehaviourComponent>().Locked == false)
            {
                other.transform.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, -180, 0);
                other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Door")
        {
            if (other.GetComponent<DoorBehaviourComponent>().Locked == false)

            {
                other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}
