using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Spinner : MonoBehaviour {
    [SerializeField]
    private float speed = 5000;
    [SerializeField]
    [Tooltip("Use this to set a delay")]
    private float offset;
    [SerializeField]
    private float wait = 0;
    [SerializeField]
    [Tooltip("Use this to change max speed")]
    private float maxRotationSpeed = 2;

    private float posx = 0;
    private float posy = 50;
    private float posz = 50;

    private Rigidbody rig;
    private Vector3 pushPoint;



    [SerializeField]
    [Tooltip("Set this for the different rotation options")]
    private int rotationMode;


    // Use this for initialization
    void Start () {
        rig = gameObject.GetComponent<Rigidbody>();
        pushPoint = new Vector3(transform.position.x - posy, transform.position.y - posx, transform.position.z - posz);
        rig.maxAngularVelocity = maxRotationSpeed;


        //hinge = GetComponent<HingeJoint>();
    }
	
	// Update is called once per frame
	void Update () {
        if(rig == null)
        {
            Debug.Log("GameObjetct: " + gameObject.name + " needs a rgidbody");
            
        }
        else
        {
            if (wait >= offset)
            {
                Spin();
            }
            else
            {
                wait = wait + 1 * Time.deltaTime;
            }

            
        }
    }

    private void Spin()
    {

        switch(rotationMode)
        {

            case 1:
                {
                    rig.AddForceAtPosition((Vector3.back * speed * Time.deltaTime), pushPoint);


                    break;
                }
            case 2:
                {
                    rig.AddTorque((Vector3.forward * speed * Time.deltaTime), ForceMode.VelocityChange);

                    break;
                }

            case 3:
                {
                    rig.AddForce((Vector3.right * speed * Time.deltaTime), ForceMode.VelocityChange);
                    break;
                }     
        }
    }

    private void spintest()
    {
        rig.velocity = new Vector3(0,0,0);
    }
}
