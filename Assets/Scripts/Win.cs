using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {


    public GameObject wola;
    public GameObject wola2;
    public GameObject wola3;
    public GameObject wola4;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        wola.SetActive(true);
        wola2.SetActive(true);
        wola3.SetActive(true);
        wola4.SetActive(true);
    }
}
