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
            player.GetComponent<Rigidbody>().MovePosition(player.transform.position + transform.forward * Time.deltaTime * 1);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerOnConveyor = true;
            player = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerOnConveyor = false;
        }
    }
}
