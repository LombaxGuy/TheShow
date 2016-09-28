using UnityEngine;
using System.Collections;

public class GazChamberActivator : MonoBehaviour {

    [SerializeField]
    private GameObject gazAreaTrigger;

    [SerializeField]
    private ParticleSystem gazAreaCloud;

    [SerializeField]
    private bool atStart;

    private bool hasRun;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
            Transform player = other.GetComponent<Collider>().transform;
            if (player.parent != null)
            if (player.parent.tag == "Player")
            {
                if(!hasRun)
                {
                    if (atStart)
                    {
                        gazAreaTrigger.SetActive(true);
                        gazAreaTrigger.GetComponent<GazChamber>().entered = true;
                        gazAreaTrigger.GetComponent<GazChamber>().inside = true;

                    }
                    else if (!atStart)
                    {
                        gazAreaCloud.Stop();
                        gazAreaTrigger.GetComponent<GazChamber>().TurnOffSprinklers();
                        gazAreaTrigger.GetComponent<GazChamber>().entered = false;
                        gazAreaTrigger.GetComponent<GazChamber>().inside = false;

                        
                    }

                    hasRun = true;
                }


            }
    }

    void OnPlayerRespawn()
    {
        hasRun = false;
    }
}
