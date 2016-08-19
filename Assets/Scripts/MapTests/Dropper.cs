using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Dropper : MonoBehaviour {

    private Transform thing;
    private Rigidbody rig;

    private float timer;
    private float offTimer;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveRange;
    [SerializeField]
    private float upWait = 2;
    [SerializeField]
    private float downWait = 2;
    [SerializeField]
    private float offset;

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
        minRange = maxRange + destination;
	
	}
	
	// Update is called once per frame
	void Update () {

        

        if(offTimer < 20)

        offTimer = offTimer + 1 * Time.deltaTime;

        if(offTimer > offset)
        {
            timer = timer + 1 * Time.deltaTime;
            Drop();
        }

	
	}

    void Drop()
    {

        if (thing.position.y < maxRange.y && mode == false && timer > upWait)
        {
            mode = true;
            timer = 0;
        }
        else if(thing.position.y > minRange.y && mode == true && timer > downWait)
        {
            mode = false;
            timer = 0;
        }


        switch(mode)
        {
            case true:
                {
                    if(timer > downWait)
                    rig.velocity = new Vector3(thing.position.x, (speed * Time.deltaTime), thing.position.z);
                    break;
                }
            case false:
                {
                    if (timer > upWait)
                        rig.velocity = new Vector3(thing.position.x, -(speed * Time.deltaTime), thing.position.z);
                    break;
                }               
        }
    }
}
