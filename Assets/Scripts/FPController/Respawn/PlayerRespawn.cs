using UnityEngine;
using System.IO;
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

    public bool IsAlive
    {
        get { return isAlive; }
    }

    //For other scripts to use
    public GameObject targetSpawnpoint;
    private Animator animator;

    private GameObject gameResetManager;
    private string gameResetManagerName = "GameResetManager";

    //Initialize the master spawn and image
    void Start()
    {
        isAlive = true;
        CCScript = GetComponentInChildren<DeathFadeComponent>();
        animator = GetComponentInChildren<Animator>();

        try
        {
            gameResetManager = GameObject.Find(gameResetManagerName);
        }
        catch
        {
            Debug.Log("PlayerRespawn.cs: No GameObject with the name '" + gameResetManagerName + "' could be found in the scene!");
        }

        try
        {
            targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        }
        catch
        {
            Debug.Log("PlayerRespawn.cs: No GameObject with the tag 'MasterRespawn' could be found in the scene!");
        }
        if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh"))
        {
            SaveGame save = SaveLoad.Load();
            transform.position = save.playerPos;
        }
    }

    /// <summary>
    /// Use rigmt mouse atm
    /// </summary>
    void Update()
    {
        if (!Pause.GetPauseState())
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
        //gameResetManager.GetComponent<GameObjectPositionReset>().GameObjectToStartLocation();
        GameObject.Find("GameResetManager").GetComponent<GameObjectPositionReset>().ResetObjects();

        transform.position = targetSpawnpoint.GetComponent<Checkpoint>().GetRespawnTransform().position;
        transform.rotation = targetSpawnpoint.GetComponent<Checkpoint>().GetRespawnTransform().rotation;

        GetComponent<FirstPersonController>().LockControls(false);

        animator.SetBool("playerDead", false);

        CCScript.ResetImageEffects();

        isAlive = true;

    }

    /// <summary>
    /// This is the method that is called to kill the player.
    /// </summary>
    public void Death()
    {
        if (isAlive)
        {
            deathCooldown = defaultDeathCooldown;

            GetComponent<FirstPersonController>().LockControls(true);

            StartCoroutine(CCScript.StartDeathFade());

            float randomAnim = Random.Range(0, 2);

            animator.SetFloat("random", randomAnim);
            animator.SetBool("playerDead", true);

            StatTracker.TotalTimesDead += 1;

            isAlive = false;
        }
        else
        {
            Debug.Log("PlayerRespawn.cs: The player is already dead! The player cannot be killed while 'isAlive' is false.");
        }
    }
}
