using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //For the respawn cordinates
    private Vector3 spawnCords;
    private DeathFadeCC CCScript;

    [SerializeField]
    private bool isAlive = true;

    //For other scripts to use
    public GameObject targetSpawnpoint;

	//Initialize the master spawn and image
	void Start ()
    {
        targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        CCScript = GetComponentInChildren<DeathFadeCC>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(isAlive == false && Input.GetKey(KeyCode.Mouse0))
        {
            Respawn();
        }
    }

    // Sets the respawn position to the current selected checkpoint and moves the player to the checkpoint
    void Respawn()
    {
        transform.position  = targetSpawnpoint.transform.position;
        
        GetComponent<FirstPersonController>().LockControls(false);

        CCScript.ResetCC();

        isAlive = true;
    }

    // Locks the controller in FirstPersonController script
    public void Death()
    {
        GetComponent<FirstPersonController>().LockControls(true);

        StartCoroutine(CCScript.StartBlackAndWhiteCCFade());

        isAlive = false;
    }
}
