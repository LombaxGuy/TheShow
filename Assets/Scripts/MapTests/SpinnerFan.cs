using UnityEngine;
using System.Collections;

public class SpinnerFan : MonoBehaviour {

    private Vector3 current;

    private Transform hinge;

    [SerializeField]
    private int state;

	// Use this for initialization
	void Start () {
        hinge = GetComponentInParent<Transform>();
        

	
	}
	
	// Update is called once per frame
	void Update () {
        current = this.transform.localRotation.eulerAngles;

        switch(state)
        {
            case 1:
                {
                    transform.rotation = Quaternion.Euler(current) * Quaternion.Euler(0, 1, 0);
                    break;
                }
            case 2:
                {
                    transform.localRotation = Quaternion.Euler(current) * Quaternion.Euler(0, 2, 0);
                    break;
                }
            case 3:
                {


                    break;
                }


        }
        
        
        

	}
}
