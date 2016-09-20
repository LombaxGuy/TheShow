using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour {

    [SerializeField]
    private bool deathCell = false;

    [SerializeField]
    private GameObject[] doors;

    float timer = 0;

    private GameObject player;

    private bool playerIsInside = false;

    private bool roomCleared = false;

	// Use this for initialization
	void Start ()
    {
        	
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
        Debug.Log("I WAS HERE" + other.tag);
        if (other.transform.parent.tag == "Player" && playerIsInside == false && roomCleared == false)
        {
            timer = 2;
            playerIsInside = true;
            player = other.gameObject;
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<AnotherDoor>().CloseDoor();
            }
        }
    }
}
