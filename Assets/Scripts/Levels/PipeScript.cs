using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour
{

    [SerializeField]
    private float offSet = 0;
    [SerializeField]
    private float duration = 3;
    [SerializeField]
    private float idleTime = 3;
    [SerializeField]
    private ParticleSystem system;

    private float coolDown;

    private bool isFireing = false;


    // Use this for initialization
    void Start()
    {
        coolDown -= offSet;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;

        if (isFireing == false)
        {
            if (coolDown > idleTime)
            {
                coolDown = 0;
                system.Play();
                isFireing = true;
                transform.GetChild(0).gameObject.SetActive(true);
            }

        }
        else
        {
            if (coolDown > duration)
            {
                coolDown = 0;
                isFireing = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }
}
