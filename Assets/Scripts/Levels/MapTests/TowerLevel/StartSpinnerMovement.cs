using UnityEngine;
using System.Collections;

public class StartSpinnerMovement : MonoBehaviour {

    [SerializeField]
    private GameObject[] spinners = new GameObject[3];


    private bool collided = false;
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
                if(!collided)
                {
                    for (int i = 0; i < spinners.Length; i++)
                    {
                        spinners[i].GetComponent<Spinner>().move = true;
                    }
                  collided = true;
                }

            }
    }
}
