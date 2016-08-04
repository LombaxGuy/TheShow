using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject checkpoint;
	// Use this for initialization
	void Start () {
        checkpoint = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// IS used to change the spawn point for the player, in the PlayerRespawn script, when the player collides with the checkpoint.
    /// </summary> 
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerRespawn>().targetSpawnpoint = checkpoint;
        GetComponent<Collider>().enabled = false;
           
    }
}
