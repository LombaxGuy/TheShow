using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

    private float maxPosition;
    private float minPosition;
    private float currentPossition;
    private bool state;
    private float speed = 1000;


    [SerializeField]
    private float cooldown = 2;
    [SerializeField]
    private float wait;
    [SerializeField]
    private float offset;
    private float waitOffset;

    private Rigidbody rig;
    private Vector3 dimentions;

	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody>();
        minPosition = gameObject.transform.position.x;
        dimentions = gameObject.GetComponent<Renderer>().bounds.size;
        maxPosition = minPosition + dimentions.x;
	}
	
	// Update is called once per frame
	void Update () {
        currentPossition = gameObject.transform.position.x;
        waitOffset = waitOffset + 1 * Time.deltaTime;
        wait = wait + 1 * Time.deltaTime;

        if (offset <= waitOffset)
        {     
            forward();
            backward();
        }
    }

    private void forward()
    {
        if (currentPossition <= maxPosition && !state && cooldown <= wait)
        {
            if (rig.velocity.x < 100)
            {
                //rig.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.VelocityChange);
                rig.velocity = new Vector3(speed * Time.deltaTime,0,0);
               
            }
        }

            if (currentPossition >= maxPosition && state == false && cooldown <= wait)
            {
                state = true;
                rig.velocity = new Vector3(0, 0, 0);
                //Debug.Log("Hit max");
                wait = 0;
            
            }
    }
    private void backward()
    {
        if (currentPossition >= minPosition && state == true && cooldown <= wait)
        {
            if (rig.velocity.x < 100)
            {
                //rig.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.VelocityChange);
                rig.velocity = new Vector3(-speed * Time.deltaTime, 0, 0);
            }

        }
        if (currentPossition <= minPosition && state == true && cooldown <= wait)
        {
            state = false;
            rig.velocity = new Vector3(0, 0, 0);
            //Debug.Log("hit min");
            wait = 0;
        }
    }

}
