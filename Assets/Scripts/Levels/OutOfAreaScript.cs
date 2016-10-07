using UnityEngine;
using System.Collections;

public class OutOfAreaScript : MonoBehaviour {

    [SerializeField]
    private GameObject limitedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            if (other.transform.parent.tag == "Player")
            {
                limitedObject.GetComponent<PipeFlameValve>().OutOfArea = true;
            }
        }
    }
}
