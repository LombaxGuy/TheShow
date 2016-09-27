using UnityEngine;
using System.Collections;

public class StartSpinnerMovement : MonoBehaviour {

    [SerializeField]
    private GameObject[] spinners = new GameObject[3];
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        for (int i = 0; i < spinners.Length; i++)
        {
            spinners[i].GetComponent<Spinner>().move = true;
        }
    }
}
