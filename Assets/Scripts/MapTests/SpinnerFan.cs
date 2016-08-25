using UnityEngine;
using System.Collections;

public class SpinnerFan : MonoBehaviour {

    private Vector3 current;

    [SerializeField]
    [Tooltip("Spinning speed")]
    private float speed = 5;
    [SerializeField]
    [Tooltip("The offset")]
    private float offset = 0;

    private float offsetTimer;
	
	// Update is called once per frame
    //For rotating the hingepoint
	void Update () {

        if (offsetTimer < offset)
        {
            offsetTimer += Time.deltaTime;
        }
        else
        {
            current = this.transform.localRotation.eulerAngles;

            transform.localRotation = Quaternion.Euler(current) * Quaternion.Euler(0, speed, 0);
        }


        
	}
}
