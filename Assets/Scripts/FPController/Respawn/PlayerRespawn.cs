using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //For the respawn cordinates
    private Vector3 spawnCords;
    private DeathFadeComponent CCScript;

    [SerializeField]
    private bool isAlive;

    //For other scripts to use
    public GameObject targetSpawnpoint;

	//Initialize the master spawn and image
	void Start ()
    {
        isAlive = true;
        targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        CCScript = GetComponentInChildren<DeathFadeComponent>();
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

        CCScript.ResetImageEffects();

        isAlive = true;
    }

    // Locks the controller in FirstPersonController script
    public void Death()
    {
        GetComponent<FirstPersonController>().LockControls(true);

        StartCoroutine(CCScript.StartDeathFade());

        isAlive = false;
    }
}
