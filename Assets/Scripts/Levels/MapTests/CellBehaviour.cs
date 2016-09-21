using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour {
    
    [Tooltip("Is set to true, a death event will happen to the player. Else the room is considered the right way and wont kill the player.")]
    [SerializeField]
    private bool deathCell = false;

    [Tooltip("Add the doors that connects the room.")]
    [SerializeField]
    private GameObject[] doors;

    [Tooltip("Time when the player is trapped inside the room till its finish, death or not.")]
    [SerializeField]
    float timeTrapped = 4;


    float timer = 0;



    private GameObject player;

    private bool playerIsInside = false;

    private bool roomCleared = false;

	// Use this for initialization
	void Start ()
    {
        	
	}
	
    void OnEnable()
    {
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    private void OnPlayerRespawn()
    {
        playerIsInside = false;
        roomCleared = false;
    }

	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(playerIsInside == true && timer <= 0)
        {
            DeathOrOpenDoors();
        }
	}

    private void DeathOrOpenDoors()
    {
        if(deathCell == true)
        {
            player.transform.parent.transform.GetComponent<PlayerRespawn>().Kill();

        }
        else
        {
            //Play Sound
            //Confetti?
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<AnotherDoor>().OpenDoor();
            }
        }
        roomCleared = true;
        playerIsInside = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player" && playerIsInside == false && roomCleared == false)
        {
            timer = timeTrapped;
            playerIsInside = true;
            player = other.gameObject;
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<AnotherDoor>().CloseDoor();
            }
        }
    }
}
