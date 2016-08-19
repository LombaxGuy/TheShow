using UnityEngine;
using System.Collections;

public class RotatorAntiRigid : MonoBehaviour {
    private Quaternion breakPoint;
    private Quaternion backPoint;
    private Vector3 current;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float offTimer;
    [SerializeField]
    private float wait = 2;
    [SerializeField]
    private float offset;
    private bool offsetstop;

    private Transform thing;

    private bool mode = false;

    private Vector3 stop;

	// Use this for initialization
	void Start () {
        //thing = gameObject.transform;
        //gameObject.transform.rotation = Quaternion.Euler(0,180,225);
        backPoint = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 225);
        breakPoint = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 310);
        
        stop = new Vector3(0,0,225);
	}

    void FixedUpdate()
    {
        if(offTimer > offset)
        {
            StepRotate();
           offsetstop = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        current = gameObject.transform.rotation.eulerAngles;
        timer = timer + 1 * Time.deltaTime;

        if(!offsetstop)
        offTimer = offTimer + 1 * Time.deltaTime;


    }

    private void StepRotate()
    {


        if (current.z >= breakPoint.eulerAngles.z && mode == false)
        {
            
            Debug.Log("breakhit");
            mode = true;
            timer = 0;
        }
        else if (current.z <=  backPoint.eulerAngles.z && mode == true)
        {
            Debug.Log("back hit");
            mode = false;
            timer = 0;
        }
        
        switch(mode)
        {
            case true:
                {
                    if(timer > wait)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(current) * Quaternion.Euler(0, 0, -2);
                    }                  
                    break;
                }
            case false:
                {
                    if(timer > wait)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(current) * Quaternion.Euler(0, 0, 10);
                    }
                    
                    break;
                }
        }
        
    }
}
