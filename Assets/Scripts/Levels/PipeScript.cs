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

    [SerializeField]
    private GameObject flameLight;

    private Light lightInt;

    private bool flameOn = false;
    private bool lightIntensity = false;
    private bool isFireing = false;

    [SerializeField]
    public bool isActivated = false;

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
       lightInt = flameLight.GetComponent<Light>();
       
       lightInt.intensity = 0;

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
                    flameOn = true;
                    
                    lightInt.intensity = 5;
                }

            }
            else
            {
                if (coolDown > duration)
                {
                    coolDown = 0;
                    isFireing = false;
                    transform.GetChild(0).gameObject.SetActive(false);                    
                    flameOn = false;
                    
                }


            }
            if (flameOn)
            {
                float r;
                
                if(lightIntensity)
                {
                    r = Random.Range(3, 5);
                    lightInt.intensity = r;
                    lightIntensity = false;
                }
                if(!lightIntensity)
                {
                    r = Random.Range(3, 5);
                    lightInt.intensity = r;
                    lightIntensity = true;
                }
                
            }

            if (!flameOn && lightInt.intensity > 0)
            {
                lightInt.intensity = lightInt.intensity - 8 * Time.deltaTime;
            }
        }

    }

    private void OnButtonPressed()
    {
        isActivated = true;
    }

    private void OnTimerExpired()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isActivated = false;
        lightInt.intensity = 0;
        flameOn = false;
    }
}
