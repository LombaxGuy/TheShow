using UnityEngine;
using System.Collections;

public class CameraTriggerBox : MonoBehaviour {

    [SerializeField]
    GameObject cameraWithScript;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("other tag " + other.tag);
        if (other.tag == "Player")
        {
            cameraWithScript.GetComponent<CameraLookAtPlayer>().PlayerInTheArea = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraWithScript.GetComponent<CameraLookAtPlayer>().PlayerInTheArea = false;
        }
    }

}
