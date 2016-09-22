using UnityEngine;
using System.Collections;

public class GridDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private Transform openedDoorTransform;

    private Vector3 openedDoorPos;
    private Vector3 closedDoorPos;

    private Quaternion closedDoorRot;

    [SerializeField]
    private float closeTime = 0.5f;

    private bool doorMoving = false;
    private bool doorIsOpen = false;

    public bool DoorIsOpen
    {
        get
        {
            return doorIsOpen;
        }

        set
        {
            doorIsOpen = value;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        closedDoorPos = door.transform.position;
        closedDoorRot = door.transform.rotation;

        openedDoorPos = openedDoorTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (!doorMoving)
        //    {
        //        if (doorIsOpen)
        //        {
        //            CloseDoor();
        //        }
        //        else
        //        {
        //            OpenDoor();
        //        }
        //    }
        //}
    }

    public void OpenDoor()
    {
        Debug.Log("Open door");
        StartCoroutine(CoroutineOpenDoor());
    }

    public void CloseDoor()
    {
        Debug.Log("Close door");
        StartCoroutine(CoroutineCloseDoor());
    }

    private IEnumerator CoroutineOpenDoor()
    {
        doorMoving = true;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / closeTime;

            door.transform.position = Vector3.Lerp(closedDoorPos, openedDoorPos, t);

            yield return null;
        }

        door.transform.position = Vector3.Lerp(closedDoorPos, openedDoorPos, t);

        DoorIsOpen = true;
        doorMoving = false;
    }

    private IEnumerator CoroutineCloseDoor()
    {
        doorMoving = true;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / closeTime;

            door.transform.position = Vector3.Lerp(openedDoorPos, closedDoorPos, t);

            yield return null;
        }

        door.transform.position = Vector3.Lerp(openedDoorPos, closedDoorPos, t);

        DoorIsOpen = false;
        doorMoving = false;
    }

    private void OnDrawGizmos()
    {
        MeshCollider meshCollider = door.GetComponent<MeshCollider>();

        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawMesh(meshCollider.sharedMesh, openedDoorTransform.position, openedDoorTransform.rotation);

        Gizmos.color = Color.green;
        Gizmos.DrawWireMesh(meshCollider.sharedMesh, openedDoorTransform.position, openedDoorTransform.rotation);

        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawMesh(meshCollider.sharedMesh, closedDoorPos, door.transform.rotation);

        Gizmos.color = Color.green;
        Gizmos.DrawWireMesh(meshCollider.sharedMesh, closedDoorPos, door.transform.rotation);
    }
}
