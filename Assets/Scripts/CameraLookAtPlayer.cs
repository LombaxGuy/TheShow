using UnityEngine;
using System.Collections;

public class CameraLookAtPlayer : MonoBehaviour {

    [SerializeField]
    GameObject player;

	// Use this for initialization
	void Start () {
        Debug.Log("time time : " + Time.time);
	}
	
    void FixedUpdate()
    {

        //Vector3 direction = player.transform.position - transform.position;
        //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.7f);
    }

	// Update is called once per frame
	void Update () {

        Vector3 tmp = 2 * transform.position - player.transform.position;

        transform.LookAt(tmp);




        Vector3 temp = transform.rotation.eulerAngles;
        temp.y = Mathf.Clamp(transform.rotation.eulerAngles.y, 100, 280);
        temp.x = Mathf.Clamp(transform.rotation.eulerAngles.x, 310, 350);

        transform.rotation = Quaternion.Euler(temp);



    }
}
