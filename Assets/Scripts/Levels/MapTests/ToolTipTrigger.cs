using UnityEngine;
using System.Collections;

public class ToolTipTrigger : MonoBehaviour
{
    private GameObject worldManager;

    [SerializeField]
    private IntroSequence introSequence;

    [SerializeField]
    private string messege;

    // Use this for initialization
    void Start()
    {
        worldManager = GameObject.Find("WorldManager");
    }

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
        {
            if (player.parent.tag == "Player")
            {

                introSequence.lightEntered = true;
                introSequence.inLight = true;

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
        {
            if (player.parent.tag == "Player")
            {
                introSequence.inLight = false;
            }
        }
    }
}
