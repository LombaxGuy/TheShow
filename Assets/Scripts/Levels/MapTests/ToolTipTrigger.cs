using UnityEngine;
using System.Collections;

public class ToolTipTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject manager;

    [SerializeField]
    private string messege;

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("WorldManager");
	
	}

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if(player.parent != null)
        if(player.parent.tag == "Player")
        {
            //manager.GetComponent<Tooltip>().DisplayTooltipForSeconds(messege, 5);
            IntroSequence.lightEntered = true;
            IntroSequence.timeInSeconds = IntroSequence.timer;
            gameObject.SetActive(false);

        }
    }
}
