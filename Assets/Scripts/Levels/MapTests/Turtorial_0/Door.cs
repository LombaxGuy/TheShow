using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    [SerializeField]
    private GameObject item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            item.SetActive(false);
        }
        
    }
}
