using UnityEngine;
using System.Collections;

public class Room2_CrouchTrigger : MonoBehaviour
{
    [SerializeField]
    private Level0_Room2 room;


    private void OnTriggerEnter()
    {
        room.TriggerCrouchLine = true;
    }
}
