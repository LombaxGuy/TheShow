using UnityEngine;
using System.Collections;

public class Room1_Light : MonoBehaviour
{
    [SerializeField]
    private Level0_Room1 room1;

    private bool playerAlreadyEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            if (other.transform.parent.tag == "Player" && !playerAlreadyEntered)
            {
                room1.PlayerEnteredLight = true;
                playerAlreadyEntered = true;
            }
        }
    }
}
