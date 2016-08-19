using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Dropper : MonoBehaviour {

    private Transform thing;
    private Rigidbody rig;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveRange;

    private Vector3 maxRange;
    private Vector3 minRange;
    private Vector3 start;
    private Vector3 destination;


    [SerializeField]
    private bool mode;

	// Use this for initialization
	void Start () {
        thing = gameObject.transform;
        rig = gameObject.GetComponent<Rigidbody>();
        start = thing.position;
        destination = new Vector3(0, moveRange ,0);
        maxRange = start - destination;
        minRange = start + destination;
	
	}
	
	// Update is called once per frame
	void Update () {
        Drop();
	
	}

    void Drop()
    {

        if (thing.position.y < maxRange.y && mode == false)
        {
            mode = true;
        }
        else if(thing.position.y > minRange.y && mode == true)
        {
            mode = false;
        }


        switch(mode)
        {
            case true:
                {
                    rig.velocity = new Vector3(thing.position.x, (speed * Time.deltaTime), thing.position.z);
                    break;
                }
            case false:
                {
                    rig.velocity = new Vector3(thing.position.x, -(speed * Time.deltaTime), thing.position.z);
                    break;
                }               
        }
    }
}
