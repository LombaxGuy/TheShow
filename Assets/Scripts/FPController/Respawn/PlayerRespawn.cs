using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //For the respawn cordinates
    private Vector3 spawnCords;

    [SerializeField]
    private bool isAlive;

    //For other scripts to use
    public GameObject targetSpawnpoint;

	//Initialize the master spawn and image
	void Start ()
    {
        isAlive = true;
        targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
    }
	
	// Update is called once per frame
    /// <summary>
    /// Use rigmt mouse atm
    /// </summary>
	void Update ()
    {
	    if(isAlive == false && Input.GetKey(KeyCode.Mouse1))
        {
            Respawn();
        }
    }

    // Sets the respawn position to the current selected checkpoint and moves the player to the checkpoint
    void Respawn()
    {
        transform.position  = targetSpawnpoint.transform.position;
        
        GetComponent<FirstPersonController>().LockControls(false);

        isAlive = true;
    }

    // Locks the controller in FirstPersonController script
   public virtual void Death()
    {
        isAlive = false;

        GetComponent<FirstPersonController>().LockControls(true);
    }
}
