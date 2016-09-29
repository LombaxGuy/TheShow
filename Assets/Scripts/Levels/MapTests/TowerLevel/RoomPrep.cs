using UnityEngine;
using System.Collections;

public class RoomPrep : MonoBehaviour {

    [SerializeField]
    private GameObject[] spinners;
    [SerializeField]
    private GameObject[] SpinnerBlades;
    [SerializeField]
    private GameObject[] droppers;
    [SerializeField]
    private GameObject[] pushers;
    [SerializeField]
    private GameObject[] mDoors;
    [SerializeField]
    private GameObject[] sFires;
    [SerializeField]
    private GameObject[] bFires;
    [SerializeField]
    private GameObject[] lights;

    

    private bool initiated = false;
    private int lightNum = 0;
    private float timer;
    private float delay = 1;



	// Use this for initialization
	void Start () {

        for (int i = 0; i < spinners.Length; i++)
        {
            spinners[i].GetComponent<Spinner>().enabled = false;
            SpinnerBlades[i].GetComponent<SpinnerFan>().enabled = false;
        }
        for (int i = 0; i < droppers.Length; i++)
        {
            droppers[i].GetComponent<Dropper>().enabled = false;
            
        }
        for (int i = 0; i < pushers.Length; i++)
        {
            pushers[i].GetComponent<Pusher>().enabled = false;

        }
        for (int i = 0; i < mDoors.Length; i++)
        {
            mDoors[i].GetComponent<HeavyMetalDoor>().enabled = false;

        }
        for (int i = 0; i < sFires.Length; i++)
        {
            sFires[i].GetComponent<PipeScript>().enabled = false;

        }
        for (int i = 0; i < bFires.Length; i++)
        {
            bFires[i].GetComponent<PipeScript>().enabled = false;

        }

    }
	
	// Update is called once per frame
	void Update () {

        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    Initiate();
        //}

        if(initiated)
        {
            
            timer += Time.deltaTime;
            LightOn();
        }
        
	    
	}

    void LightOn()
    {

        if(timer > delay && lights.Length > lightNum)
        {
            lights[lightNum].gameObject.SetActive(true);
            timer = 0;
            lightNum++;
            
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;
        if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                Initiate();
            }
    }

    public void Initiate()
    {
        for (int i = 0; i < spinners.Length; i++)
        {
            spinners[i].GetComponent<Spinner>().enabled = true;
            SpinnerBlades[i].GetComponent<SpinnerFan>().enabled = true;
        }
        for (int i = 0; i < droppers.Length; i++)
        {
            droppers[i].GetComponent<Dropper>().enabled = true;

        }
        for (int i = 0; i < pushers.Length; i++)
        {
            pushers[i].GetComponent<Pusher>().enabled = true;

        }
        for (int i = 0; i < mDoors.Length; i++)
        {
            mDoors[i].GetComponent<HeavyMetalDoor>().enabled = true;

        }
        for (int i = 0; i < sFires.Length; i++)
        {
            sFires[i].GetComponent<PipeScript>().enabled = true;

        }
        for (int i = 0; i < bFires.Length; i++)
        {
            bFires[i].GetComponent<PipeScript>().enabled = true;

        }

        initiated = true;
    }
}
