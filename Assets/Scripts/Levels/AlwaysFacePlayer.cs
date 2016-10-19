using UnityEngine;
using System.Collections;

public class AlwaysFacePlayer : MonoBehaviour
{

    private Camera playerCamera;

    // Use this for initialization
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = playerCamera.transform.position - transform.position;
        lookPos.y = 0;

        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
