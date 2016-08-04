using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour {

    //For the respawn cordinates
    private Transform respawnPoint;
    private Vector3 spawnCords;

    //For the fade screen
    //private Image img;
    private float fadetime = 120f;
    private float dark = 255f;
    private float bright = 0.1f;


    [SerializeField]
    private bool isAlive = true;


    //For other scripts to use
    public GameObject targetSpawnpoint;

    

	//Initialize the master spawn and image
	void Start () {
        targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        respawnPoint = targetSpawnpoint.GetComponent<Transform>();

        //img = GameObject.Find("FadePanel").GetComponent<Image>();
        //img.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
    }
	
	// Update is called once per frame
    // Need to change input to a death
	void Update () {
	
        if(isAlive == false && Input.GetKey(KeyCode.Mouse0))
        {
            Respawn();
            WakeUp();

        }

    }

    //sets the respawn position to the current selected checkpoint and moves the player to the checkpoint
    void Respawn()
    {
        respawnPoint = targetSpawnpoint.GetComponent<Transform>();
        GetComponent<Transform>().position  = respawnPoint.position;
        
        GetComponent<FirstPersonController>().LockControls(false);

        
        //img.CrossFadeAlpha(bright, 1, true);
        isAlive = true;


    }

    //Locks the controller in FirstPersonController script
   public virtual void Death()
    {
        isAlive = false;
        GetComponent<FirstPersonController>().LockControls(true);

        //img.CrossFadeAlpha(dark, 1, true);
        

    }

    void WakeUp()
    {

    }

}
