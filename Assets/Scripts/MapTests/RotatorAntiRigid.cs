using UnityEngine;
using System.Collections;

public class RotatorAntiRigid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Rotate(1,0,0);

	}
}
