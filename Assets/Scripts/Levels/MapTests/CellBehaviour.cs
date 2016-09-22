using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour {

    enum DeathWay { GAS, FIRE, ROOF, SPIKES, WATER, STUCK}

    [Tooltip("Is set to true, a death event will happen to the player. Else the room is considered the right way and wont kill the player.")]
    [SerializeField]
    private bool deathCell = false;

    [SerializeField]
    private DeathWay deathWay;
    [SerializeField]
    private float timeBeforeDeath = 2;
    [SerializeField]
    private GameObject gasObject;
    private GameObject tempGasObject;

    [Tooltip("Add the doors that connects the room.")]
    [SerializeField]
    private GameObject[] doors;

    [Tooltip("Time when the player is trapped inside the room till its finish, death or not.")]
    [SerializeField]
    float timeTrapped = 4;

    [SerializeField]
    private bool[] allowedToOpen;
    [SerializeField]
    private int doorStartOpen = 0;

    [SerializeField]
    private bool startingRoom = false;

    [SerializeField]
    private GameObject particleObject;
    private float particleTime = 3;

    float timer = 0;

    [SerializeField]
    private GameObject soundObject;
    [SerializeField]
    private AudioClip clip;

    private AudioSource source;

    private GameObject player;

    private bool playerIsInside = false;

    private bool roomCleared = false;

    private bool deathActivated = false;

    

	// Use this for initialization
	void Start ()
    {
        if(startingRoom == true)
        {
            doors[doorStartOpen].GetComponent<GridDoor>().OpenDoor();
        }

        source = soundObject.GetComponent<AudioSource>();
        source.loop = false;
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
        switch (deathWay)
        {
            case DeathWay.GAS:
                Destroy(tempGasObject);
                break;
            case DeathWay.FIRE:
                break;
            case DeathWay.ROOF:
                break;
            case DeathWay.SPIKES:
                break;
            case DeathWay.WATER:
                break;
            case DeathWay.STUCK:
                break;
            default:
                break;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            if(startingRoom == false)
            {
                if(doors[i].GetComponent<GridDoor>().DoorIsOpen == true)
                {
                    doors[i].GetComponent<GridDoor>().CloseDoor();
                }
               
            }else if(startingRoom == true && i != doorStartOpen)
            {
                doors[i].GetComponent<GridDoor>().CloseDoor();
            }
                    
        }
    }

	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(playerIsInside == true && timer <= 0 && deathActivated == false)
        {
            DeathOrOpenDoors();
        }

        if(timer <= 0)
        {
            particleObject.SetActive(false);
        }

        if(deathActivated == true)
        {
            DeathWays();
        }
	}

    private void DeathWays()
    {
        if(timer <= 0)
        {
            player.transform.parent.transform.GetComponent<PlayerRespawn>().Kill();
            deathActivated = false;
            Debug.Log(deathWay + " killed the Player");
        }

    }

    private void DeathOrOpenDoors()
    {
        if(deathCell == true)
        {
            deathActivated = true;
            timer = timeBeforeDeath;
            switch (deathWay)
            {
                case DeathWay.GAS:
                    tempGasObject = Instantiate(gasObject);
                    tempGasObject.transform.SetParent(gameObject.transform.parent.transform);
                    tempGasObject.transform.localPosition = new Vector3(0, -2, 0);
                    break;
                case DeathWay.FIRE:
                    
                    break;
                case DeathWay.ROOF:
                    
                    break;
                case DeathWay.SPIKES:
                    
                    break;
                case DeathWay.WATER:
                    
                    break;
                case DeathWay.STUCK:
                    
                    break;
                default:
                    break;
            }
        }
        else
        {            
            source.PlayOneShot(clip);
            particleObject.SetActive(true);
            timer = particleTime;
            for (int i = 0; i < doors.Length; i++)
            {
                if(allowedToOpen[i] == true)
                {
                    doors[i].GetComponent<GridDoor>().OpenDoor();
                }           
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
                if(doors[i].GetComponent<GridDoor>().DoorIsOpen == true)
                {
                    doors[i].GetComponent<GridDoor>().CloseDoor();
                }           
            }
        }
    }
}
