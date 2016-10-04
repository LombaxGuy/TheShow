using UnityEngine;
using System.Collections;

public class CameraLookAtPlayer : MonoBehaviour {

    [SerializeField]
    GameObject player;


    [SerializeField]
    bool lookAtPlayer = true;

    private bool playerInTheArea;

    public bool PlayerInTheArea
    {
        get { return playerInTheArea; }

        set {  playerInTheArea = value; }
    }

    // Use this for initialization
    void Start ()
    {
        playerInTheArea = false;
        Debug.Log("time time : " + Time.time);
	}
	
    void FixedUpdate()
    {

        //Vector3 direction = player.transform.position - transform.position;
        //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.5f * Time.time);
    }

	// Update is called once per frame
	void Update ()
    {

        if(lookAtPlayer == true && PlayerInTheArea == true)
        {
            CamFollowPlayer();
        }
        

    }

    private void CamFollowPlayer()
    {

        transform.LookAt(player.transform);

        Vector3 temp = transform.localRotation.eulerAngles;

        if (transform.localRotation.eulerAngles.y <= 270 && transform.localRotation.eulerAngles.y > 180)
        {
            temp.y = 270;
        }
        else if (transform.localRotation.eulerAngles.y <= 180 && transform.localRotation.eulerAngles.y >= 90)
        {
            temp.y = 90;
        }

        if (transform.localRotation.eulerAngles.x <= 350 && transform.localRotation.eulerAngles.x > 180)
        {
            temp.x = 350;
        }
        else if (transform.localRotation.eulerAngles.x <= 180 && transform.localRotation.eulerAngles.x >= 30)
        {
            temp.x = 30;
        }

        transform.localRotation = Quaternion.Euler(temp);

    }

    
}
