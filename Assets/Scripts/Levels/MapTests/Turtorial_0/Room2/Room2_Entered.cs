using UnityEngine;
using System.Collections;

public class Room2_Entered : MonoBehaviour
{
    [SerializeField]
    private Level0_Room2 room2;

    private bool playerAlreadyEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            if (other.transform.parent.tag == "Player" && !playerAlreadyEntered)
            {
                room2.Entered = true;
                playerAlreadyEntered = true;
            }
        }
    }
}
