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
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                IntroSequence.firstCleared = true;
                gameObject.SetActive(false);
            }

    }
}
