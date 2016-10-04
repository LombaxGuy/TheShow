using UnityEngine;
using System.Collections;

public class RoomTriggers : MonoBehaviour
{
    [SerializeField]
    private RoomComponent roomScript;

    private void OnTriggerEnter()
    {
        roomScript.PlayerInRoom = true;
    }

    private void OnTriggerExit()
    {
        roomScript.PlayerInRoom = false;
    }
}
