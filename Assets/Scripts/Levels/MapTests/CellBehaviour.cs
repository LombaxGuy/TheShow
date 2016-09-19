using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour {

    [SerializeField]
    private bool deathCell = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(deathCell == true)
        {
            if(other.transform.tag == "Player")
            {
                other.transform.GetComponent<PlayerRespawn>().Kill();
            }
        }
    }
}
