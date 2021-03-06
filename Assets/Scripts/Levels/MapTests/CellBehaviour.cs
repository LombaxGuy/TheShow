﻿using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour {

    public enum DeathWay { GAS, FIRE, ROOF, SPIKES, WATER, STUCK}
    [SerializeField]
    private bool deathCell = false;
    [SerializeField]
    private DeathWay deathEvent;
    [SerializeField]
    private float timeBeforeDeath = 2;
    [SerializeField]
    private GameObject gasObject;

    private GameObject tempGasObject;
    [SerializeField]
    private GameObject[] doors = new GameObject[4];

    float timeTrapped = 4;
    [SerializeField]
    private bool[] allowedToOpen = new bool[4];
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

    public DeathWay DeathEvent
    {
        get { return deathEvent; }
        set { deathEvent = value; }
    }

    public GameObject GasObject
    {
        get { return gasObject; }
        set { gasObject = value; }
    }

    public bool DeathCell
    {
        get { return deathCell; }
        set { deathCell = value; }
    }

    public float TimeBeforeDeath
    {
        get { return timeBeforeDeath; }
        set { timeBeforeDeath = value; }
    }

    public GameObject[] Doors
    {
        get { return doors; }
        set { doors = value; }
    }

    public float TimeTrapped
    {
        get { return timeTrapped; }
        set { timeTrapped = value; }
    }

    public bool[] AllowedToOpen
    {
        get { return allowedToOpen; }
        set { allowedToOpen = value; }
    }

    public int DoorStartOpen
    {
        get { return doorStartOpen; }
        set { doorStartOpen = value; }
    }

    public bool StartingRoom
    {
        get { return startingRoom; }
        set { startingRoom = value; }
    }

    public GameObject ParticleObject
    {
        get { return particleObject; }
        set { particleObject = value; }
    }

    public GameObject SoundObject
    {
        get { return soundObject; }
        set { soundObject = value; }
    }

    public AudioClip Clip
    {
        get { return clip; }
        set { clip = value; }
    }



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
        switch (deathEvent)
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
            Debug.Log(deathEvent + " killed the Player");
        }

    }

    private void DeathOrOpenDoors()
    {
        if(deathCell == true)
        {
            deathActivated = true;
            timer = timeBeforeDeath;
            switch (deathEvent)
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
