using UnityEngine;
using System.Collections;

public class GazChamberActivator : MonoBehaviour {

    [SerializeField]
    private GameObject gazAreaTrigger;

    [SerializeField]
    private ParticleSystem gazAreaCloud;

    [SerializeField]
    private bool atStart;

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
                if (atStart)
                {
                    gazAreaTrigger.SetActive(true);
                    gameObject.SetActive(false);
                }
                else if (!atStart)
                {
                gazAreaCloud.Stop();
                gazAreaTrigger.GetComponent<GazChamber>().inside = false;
                gameObject.SetActive(false);
                }

    }
    }
}
