using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    //For the respawn cordinates
    private Vector3 spawnCords;

    [SerializeField]
    private float defaultDeathCooldown = 2;

    [SerializeField]
    private bool spawnAtCheckpoint = false;

    private float deathCooldown = 2;

    private bool isAlive;

    private GameObject targetSpawnpoint;
    private Animator animator;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    public GameObject TargetSpawnpoint
    {
        get { return targetSpawnpoint; }
        set { targetSpawnpoint = value; }
    }

    void OnEnable()
    {
        // Subscribes to the OnRewpawnReset event
        EventManager.OnPlayerDeath += OnPlayerDeath;
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        // Unsubscribes from the OnRewpawnReset event
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    //Initialize the master spawn and image
    void Start()
    {
        isAlive = true;
        animator = GetComponentInChildren<Animator>();

        try
        {
            targetSpawnpoint = GameObject.FindGameObjectWithTag("MasterRespawn");
        }
        catch
        {
            Debug.Log("PlayerRespawn.cs: No GameObject with the tag 'MasterRespawn' could be found in the scene!");
        }

        //Sets the player position to the last saved checkpoints position if there is a savefile
        if (File.Exists(Application.persistentDataPath + "/SaveData/SaveGame.blargh") && spawnAtCheckpoint)
        {
            SaveGame save = SaveLoad.Load();
            transform.position = new Vector3(save.playerPosX, save.playerPosY, save.playerPosZ);
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
                EventManager.RaiseOnPlayerRespawn();
            }

            if (!isAlive)
            {
                deathCooldown -= Time.deltaTime;
            }
        }
    }

    public void Kill()
    {
        if (isAlive)
        {
            EventManager.RaiseOnPlayerDeath();

            isAlive = false;
        }
        else
        {
            Debug.Log("PlayerRespawn.cs: The player is already dead! The player cannot be killed while 'isAlive' is false.");
        }
    }

    private void OnPlayerDeath()
    {
        deathCooldown = defaultDeathCooldown;

        //GetComponent<FirstPersonController>().LockControls(true);

        float randomAnim = Random.Range(0, 2);

        animator.SetFloat("random", randomAnim);
        animator.SetBool("playerDead", true);

        StatTracker.TotalTimesDead += 1;
    }

    private void OnPlayerRespawn()
    {
        transform.position = targetSpawnpoint.GetComponent<Checkpoint>().GetRespawnTransform().position;
        transform.rotation = targetSpawnpoint.GetComponent<Checkpoint>().GetRespawnTransform().rotation;

        animator.SetBool("playerDead", false);

        isAlive = true;
    }
}
