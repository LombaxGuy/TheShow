using UnityEngine;
using System.Collections;

public class OpenDoorAfterTime : MonoBehaviour {

    [SerializeField]
    float timer = 30;

    private bool completed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(completed == false)
        {
            if (timer <= 0)
            {
                completed = true;
                transform.GetComponent<DoorBehaviourComponent>().Locked = false;
                transform.GetComponent<DoorBehaviourComponent>().LockDoor(false);
                Debug.Log("Door Unlocked!");
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

	
	}
}
