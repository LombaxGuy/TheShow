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
    private float upWait = 2;
    [SerializeField]
    private float downWait = 2;
    [SerializeField]
    private float offset;

    [SerializeField]
    private Vector3 maxRange;
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 destination;


    private Vector3 down;



    [SerializeField]
    private bool mode;

	// Use this for initialization
	void Start () {
        thing = gameObject.transform;
        rig = gameObject.GetComponent<Rigidbody>();
        start = thing.position;
        destination = new Vector3(0, 0 ,0);
        

        RaycastHit hit;

        if (Physics.Raycast(thing.position, Vector3.down, out hit ,20))
        {
            Debug.DrawRay(thing.position, Vector3.down * 20, Color.red);
            
            
            destination.y = hit.distance;
            Debug.Log(destination);
        }
        maxRange = start - destination;
       
    }
	
	// Update is called once per frame
	void Update () {

        if (offTimer < offset)
            offTimer += Time.deltaTime;

        if (offTimer > offset)
        {
            timer += Time.deltaTime;
           // Drop();
        }

    }

    void Drop()
    {

        if (thing.position.y == maxRange.y + 0.5f && mode == false && timer > upWait)
        {
            mode = true;
            rig.velocity = new Vector3(0,0,0);
            timer = 0;
            Debug.Log("first");
        }
        else if (thing.position.y == start.y && mode == true && timer > downWait)
        {
            Debug.Log("sec");
            mode = false;
            rig.velocity = new Vector3(0, 0, 0);
            timer = 0;
        }


        switch (mode)
        {
            case true:
                {
                    //DownTop
                    if (timer > downWait)
                    {
                        //
                        Debug.Log("nt");
                    }

                    break;
                }
            case false:
                {
                    //TopDown
                    if (timer > upWait)
                    {
                        //rig.velocity = new Vector3(thing.position.x, -(speed * Time.deltaTime), thing.position.z);                                                                
                        
                        Debug.Log("dt");
                    }

                    break;
                }               
        }
    }
}
