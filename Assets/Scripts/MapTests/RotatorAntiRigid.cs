using UnityEngine;
using System.Collections;

public class RotatorAntiRigid : MonoBehaviour {

    //Used vectors
    private Quaternion breakPoint;
    private Quaternion backPoint;
    private Vector3 current;

    // Two timers
    private float timer;
    private float offTimer;

    //Settings
    [SerializeField]
    [Tooltip("Time it takes to wait")]
    private float wait = 2;
    [SerializeField]
    [Tooltip("How long it takes until script starts running")]
    private float offset;


    private bool offsetstop;
    private bool mode = false;


	// Use this for initialization
    /// <summary>
    /// Setting up the points where start and where stop is.
    /// </summary>
	void Start () {

        backPoint = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 225);
        breakPoint = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 310);
        
	}

    /// <summary>
    /// Where the object is moved
    /// </summary>
    void FixedUpdate()
    {
        if(offTimer > offset)
        {
            StepRotate();
           offsetstop = true;
        }
        
    }
	
	// Update is called once per frame
    /// <summary>
    /// Running the timers and setting the current transform
    /// </summary>
	void Update () {
        current = gameObject.transform.rotation.eulerAngles;
        timer = timer + 1 * Time.deltaTime;

        if(!offsetstop)
        offTimer = offTimer + 1 * Time.deltaTime;


    }

    /// <summary>
    /// Checking position and doing the rotating
    /// </summary>
    private void StepRotate()
    {


        if (current.z >= breakPoint.eulerAngles.z && mode == false)
        {            
            mode = true;
            timer = 0;
        }
        else if (current.z <=  backPoint.eulerAngles.z && mode == true)
        {
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
