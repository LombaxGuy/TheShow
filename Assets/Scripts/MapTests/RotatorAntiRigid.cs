using UnityEngine;
using System.Collections;

public class RotatorAntiRigid : MonoBehaviour {



    // Two timers
    private float idleTimer;

    private float offsetTimer;

    //Settings
    [SerializeField]
    [Tooltip("Time it takes to wait")]
    private float timeIdleOpen = 2;
    [SerializeField]
    [Tooltip("Time it takes to wait")]
    private float timeIdleClosed = 2;
    [SerializeField]
    [Tooltip("How long it takes until script starts running")]
    private float timeOffset = 0;

    private bool rotationDirection = false;

    //The quaternions that this script needs to function
    private Quaternion furthestRotationStop;
    private Quaternion standartRotationStop;
    //Curent rotation on our object
    private Vector3 currentRotation;

    // Use this for initialization
    /// <summary>
    /// Setting up the points where start and where stop is.
    /// </summary>
    void Start () {

        standartRotationStop = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 225);
        furthestRotationStop = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 310);
        
	}
	
	// Update is called once per frame
    /// <summary>
    /// Running the timers and setting the current transform
    /// </summary>
	void Update () {
        currentRotation = gameObject.transform.rotation.eulerAngles;
        
        if(offsetTimer < timeOffset)
        {
            offsetTimer = offsetTimer + 1 * Time.deltaTime;
        }
        else
        {
            idleTimer = idleTimer + 1 * Time.deltaTime;
        }

        StepRotate();
    }

    /// <summary>
    /// Checking position and doing the rotating
    /// </summary>
    private void StepRotate()
    {

        if (currentRotation.z >= furthestRotationStop.eulerAngles.z && rotationDirection == false)
        {            
            rotationDirection = true;
            idleTimer = 0;
        }
        else if (currentRotation.z <=  standartRotationStop.eulerAngles.z && rotationDirection == true)
        {
            rotationDirection = false;
            idleTimer = 0;
        }
        
        switch(rotationDirection)
        {
            case true:
                {
                    if(idleTimer > timeIdleClosed)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(currentRotation) * Quaternion.Euler(0, 0, -2);
                    }                  
                    break;
                }
            case false:
                {
                    if(idleTimer > timeIdleOpen)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(currentRotation) * Quaternion.Euler(0, 0, 10);
                    }
                    
                    break;
                }
        }
        
    }
}
