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

    [SerializeField]
    private bool isActivated = false;

    [SerializeField]
    private bool useEvent = true;


    private void OnEnable()
    {
        if (useEvent)
        {
            EventManager.OnTimerExpired += OnTimerExpired;
            EventManager.OnButtonPressed += OnButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (useEvent)
        {
            EventManager.OnTimerExpired -= OnTimerExpired;
            EventManager.OnButtonPressed -= OnButtonPressed;
        }
    }

    // Use this for initialization
    void Start()
    {
        coolDown -= offSet;
        if (!useEvent)
        {
            isActivated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
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

    private void OnButtonPressed()
    {
        isActivated = true;
    }

    private void OnTimerExpired()
    {
        isActivated = false;
    }
}
