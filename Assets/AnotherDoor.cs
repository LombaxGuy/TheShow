using UnityEngine;
using System.Collections;

public class AnotherDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            OpenDoor();
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            CloseDoor();
        }

	}

    public void OpenDoor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    public void CloseDoor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
    }
}
