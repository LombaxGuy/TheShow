using UnityEngine;
using System.Collections;

public class GazChamber : MonoBehaviour {

    private float gazTimer;
    private bool entered = false;
    public bool inside = false;

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
    private GameObject player;

    private Transform gaz;
    
	// Use this for initialization
	void Start () {
        gaz = gazArea.GetComponent<Transform>();

	
	}
	
	// Update is called once per frame
	void Update () {

        

        if(entered)
        {
            gazTimer += Time.deltaTime;

            if(gaz.position.y < 35)
            gaz.position = new Vector3(gaz.position.x, gaz.position.y + 0.007f, gaz.position.z);

            if (gazTimer > solveTime && inside == true)
            {
                player.GetComponent<PlayerRespawn>().Kill();
            }

        }
	
	}


    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;
            if (player.parent != null)
            if (player.parent.tag == "Player")
            {               
                if(!entered)
                {
                    inside = true;
                    entered = true;
                    enterDoor.GetComponent<DoorBehaviourComponent>().LockDoor(true);

                    gazArea.Play();

                    for (int i = 0; i < gazVents.Length; i++)
                    {
                        gazVents[i].Play();
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
                gazArea.Clear();

                for (int i = 0; i < gazVents.Length; i++)
                {
                    gazVents[i].Stop();
                }
                enterDoor.GetComponent<DoorBehaviourComponent>().LockDoor(false);
                gameObject.SetActive(false);
            }
    }
}
