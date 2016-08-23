using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
    
    private float speed = 5000;

    private bool isOut = false;

    private bool isMoving = false;


    [SerializeField]
    private float distance = 5;

    private float pushOutTime = 0.2f;

    private float offsetSeconds = 0;
    private float offsetTimer = 0;

    private float idleTimer = 0;

    private float idleInSeconds = 3;
    private float idleOutSeconds = 1;

    private BoxCollider stampTrigger;

    private Vector3 startPos;

    private Vector3 extPos;

    // Use this for initialization
    void Start () {

        stampTrigger = transform.GetChild(0).GetComponent<BoxCollider>();
        startPos = transform.position;
        extPos = transform.position + transform.forward * distance;

    }
	
	// Update is called once per frame
	void Update () {
        if(!isMoving)
        {
            stampTrigger.enabled = false;
            if (offsetTimer < offsetSeconds)
            {
                
                offsetTimer += Time.deltaTime;
            }
            else
            {
                idleTimer += Time.deltaTime;
            }
        }   


        if(isOut)
        {
            if(idleTimer > idleOutSeconds)
            {
                Move(extPos, startPos, pushOutTime);
                stampTrigger.enabled = false;
                isOut = false;
                idleTimer = 0;
            }

        }
        else
        {
            if (idleTimer > idleInSeconds)
            {
                Move(startPos, extPos, pushOutTime);
                stampTrigger.enabled = true;
                isOut = true;
                idleTimer = 0;
            }
        }

        
        
    }

    private IEnumerator CoroutineMove(Vector3 from, Vector3 to, float timeInSeconds)
    {
        isMoving = true;
        float t = 0;

        while(t < 1)
        {
            transform.position = Vector3.Lerp(from, to, t);

            t += Time.deltaTime / timeInSeconds;

            yield return null;
        }


        transform.position = Vector3.Lerp(from, to, t);
        isMoving = false;
    }

    private void Move( Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

    }
}
