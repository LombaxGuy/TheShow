using UnityEngine;
using System.Collections;

public class HeavyMetalDoor : MonoBehaviour
{
    [SerializeField]
    private float timeFromOpenToClose = 3.0f;

    [SerializeField]
    private float timeFromCloseToOpen = 1.0f;

    [SerializeField]
    private Animator doorAnimator;

    private float cooldown = 0;

    private bool doorOpen = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if (doorOpen)
        {
            if (cooldown > timeFromOpenToClose)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = false;
            }
        }
        else
        {
            if (cooldown > timeFromCloseToOpen)
            {
                doorAnimator.SetTrigger("triggerDoor");
                cooldown = 0;
                doorOpen = true;
            }
        }
    }
}
