using UnityEngine;
using System.Collections;

public class GazChamber : MonoBehaviour {

    private float gazTimer;
    private float gazY;
    public bool entered = false;
    public bool inside = false;
    public bool completed = false;

    [SerializeField]
    [Tooltip("The Time to solve the chamber")]
    private float solveTime = 30;

    [SerializeField]
    private ParticleSystem[] gazVents;
    [SerializeField]
    private ParticleSystem gazArea;

    [SerializeField]
    private GameObject enterDoor;

    [SerializeField]
    private GameObject gateDoor;


    [SerializeField]
    private GameObject player;

    private Transform gaz;


    void OnEnable()
    {
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    // Use this for initialization
    void Start () {
        gaz = gazArea.GetComponent<Transform>();
        gazY = gaz.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {

        
    if(!completed)
        {
            if (entered)
            {
                gazTimer += Time.deltaTime;

                if (gaz.position.y < 35)
                    gaz.position = new Vector3(gaz.position.x, gaz.position.y + 0.007f, gaz.position.z);

                if (gazTimer > solveTime && inside == true)
                {
                    player.GetComponent<PlayerRespawn>().Kill();
                }

            }
        }

	
	}


    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;
 

        if (player.parent != null)
            if (player.parent.tag == "Player")
            {   
                if(!completed)
                {
                    if (entered)
                    {
                        enterDoor.GetComponent<GridDoor>().CloseDoor();
                        gateDoor.GetComponent<GridDoor>().OpenDoor();
                        inside = true;
                        
                        gazArea.Play();

                        for (int i = 0; i < gazVents.Length; i++)
                        {
                            gazVents[i].Play();
                        }
                    }
                }            

            }
    }

    void OnTriggerExit(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;
        if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                inside = false;
                entered = false;
                gazArea.Clear();

                for (int i = 0; i < gazVents.Length; i++)
                {
                    gazVents[i].Stop();
                }
                

                completed = true;
            }

    }

    public void TurnOffSprinklers()
    {
        for (int i = 0; i < gazVents.Length; i++)
        {
            gazVents[i].Stop();
        }
    }

    void OnPlayerRespawn()
    {
        if(!completed)
        {
            entered = false;
            inside = false;
            gazArea.Stop();
            gazArea.Clear();
            gazTimer = 0;
            gaz.position = new Vector3(gaz.position.x, gazY, gaz.position.z);
            enterDoor.GetComponent<GridDoor>().OpenDoor();
            gateDoor.GetComponent<GridDoor>().CloseDoor();

            for (int i = 0; i < gazVents.Length; i++)
            {
                gazVents[i].Stop();
                gazVents[i].Clear();
            }
            gameObject.SetActive(false);
        }

    }
}
