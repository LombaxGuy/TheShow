using UnityEngine;
using System.Collections;

public class CameraTriggerBox : MonoBehaviour {

    [SerializeField]
    GameObject[] cameraWithScript;


    void OnTriggerEnter(Collider other)
    {
        if(cameraWithScript != null)
        {
            if (other.transform.parent.tag == "Player")
            {
                for (int i = 0; i < cameraWithScript.Length; i++)
                {
                    cameraWithScript[i].transform.GetChild(0).GetComponent<CameraLookAtPlayer>().PlayerInTheArea = true;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (cameraWithScript != null)
        {
            if (other.transform.parent.tag == "Player")
            {
                for (int i = 0; i < cameraWithScript.Length; i++)
                {
                    cameraWithScript[i].transform.GetChild(0).GetComponent<CameraLookAtPlayer>().PlayerInTheArea = false;
                }
            }
        }
    }

}
