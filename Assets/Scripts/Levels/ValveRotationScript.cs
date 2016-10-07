using UnityEngine;
using System.Collections;

public class ValveRotationScript : MonoBehaviour {


    [SerializeField]
    private bool startRotating = false;

    [SerializeField]
    private bool rotated = false;

    private float rotateTimer = 0;

    private Vector3 current;

    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
	    if(startRotating && !rotated)
        {
            rotateTimer += Time.deltaTime;

            current = this.transform.localRotation.eulerAngles;

            transform.localRotation = Quaternion.Euler(current) * Quaternion.Euler(0, 0, 6);

            if (rotateTimer >= 4)
            {
                rotated = true;
            }
        }
	}

    public void StartRotation()
    {
        startRotating = true;
    }
}
