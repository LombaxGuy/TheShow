using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    [SerializeField]
    private Vector3 top;
    [SerializeField]
    private Vector3 bot;

    [SerializeField]
    float startTime;
    [SerializeField]
    float distanceDown;
    [SerializeField]
    float distanceUp;


    [SerializeField]
    private float speed = 5000;

    private bool isDown = false;

    private bool isMoving = false;


    private float distance = 10;


    private float dropDownTime = 0.4f;

    private float dropUpTime = 1f;

    private float offsetSeconds = 0;

    private float offsetTimer = 0;

    private float idleTimer = 0;

    private float idleInSeconds = 3;

    private float idleOutSeconds = 1;




    void Start()
    {
        top = transform.position;
        bot = transform.position;


        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20))
        {
            Debug.DrawRay(transform.position, Vector3.down * 20, Color.red);

            bot = new Vector3(transform.position.x,transform.position.y -hit.distance + 0.5f, transform.position.z);            

            Debug.Log(distance);
        }

    }
    void Update()
    {

        if (!isMoving)
        {
            
            if (offsetTimer < offsetSeconds)
            {

                offsetTimer += Time.deltaTime;
            }
            else
            {
                idleTimer += Time.deltaTime;
            }
        }


        if (isDown)
        {
            if (idleTimer > idleOutSeconds)
            {
                Move(bot, top, dropUpTime);               
                isDown = false;
                idleTimer = 0;
            }

        }
        else
        {
            if (idleTimer > idleInSeconds)
            {
                Move(top, bot, dropDownTime);
                isDown = true;
                idleTimer = 0;
            }
        }
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, top, speed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, bot, speed * Time.deltaTime);
        //}


    }

    private IEnumerator CoroutineMove(Vector3 from, Vector3 to, float timeInSeconds)
    {
        isMoving = true;
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(from, to, t);

            t += Time.deltaTime / timeInSeconds;

            yield return null;
        }


        transform.position = Vector3.Lerp(from, to, t);
        isMoving = false;
    }

    private void Move(Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

    }
}
