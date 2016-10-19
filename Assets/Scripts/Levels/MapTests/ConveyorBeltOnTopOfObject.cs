using UnityEngine;
using System.Collections;

public class ConveyorBeltOnTopOfObject : MonoBehaviour {

    private GameObject player;
    private bool playerOnConveyor;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        if (playerOnConveyor == true)
        {
            player.GetComponent<Rigidbody>().MovePosition(player.transform.position + transform.forward * Time.deltaTime * 1.5f);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        
        if (other.transform.tag == "Player")
        {
            playerOnConveyor = true;
            player = other.gameObject;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            playerOnConveyor = false;
        }
    }
}
