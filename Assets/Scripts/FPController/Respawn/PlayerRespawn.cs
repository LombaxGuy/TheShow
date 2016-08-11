using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //For the respawn cordinates
    private Vector3 spawnCords;
    private DeathFadeComponent CCScript;

    [SerializeField]
    private float defaultDeathCooldown = 2;

    private float deathCooldown = 2;

    [SerializeField]
    private bool isAlive;

    //For other scripts to use
    public GameObject targetSpawnpoint;
    private Animator animator;

	//Initialize the master spawn and image
	void Start ()
    {
        isAlive = true;
        targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        CCScript = GetComponentInChildren<DeathFadeComponent>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
    /// <summary>
    /// Use rigmt mouse atm
    /// </summary>
	void Update ()
    {
        if (!Pause.getPauseState())
        {
            if (!isAlive && Input.GetKey(KeyCode.Mouse1) && deathCooldown <= 0)
            {
                Respawn();
            }

            if (!isAlive)
            {
                deathCooldown -= Time.deltaTime;
            }
        }
    }

    // Sets the respawn position to the current selected checkpoint and moves the player to the checkpoint
    void Respawn()
    {
        transform.position  = targetSpawnpoint.transform.position;
                
        GetComponent<FirstPersonController>().LockControls(false);

        animator.SetBool("playerDead", false);

        CCScript.ResetImageEffects();

        isAlive = true;

    }

    // Locks the controller in FirstPersonController script
    public void Death()
    {
        deathCooldown = defaultDeathCooldown;

        GetComponent<FirstPersonController>().LockControls(true);

        StartCoroutine(CCScript.StartDeathFade());

        float test = Random.Range(0, 2);

        Debug.Log(test);

        animator.SetFloat("random", test);
        animator.SetBool("playerDead", true);

        isAlive = false;
    }
}
