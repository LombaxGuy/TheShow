using UnityEngine;
using System.Collections;

public class SpinnerFan : MonoBehaviour {

    private Vector3 current;

    [SerializeField]
    [Tooltip("Speed")]
    private float speed = 5;

	// Use this for initialization
	void Start () {
        

	
	}
	
	// Update is called once per frame
	void Update () {
        current = this.transform.localRotation.eulerAngles;

        transform.localRotation = Quaternion.Euler(current) * Quaternion.Euler(0, speed, 0);
        
	}
}
