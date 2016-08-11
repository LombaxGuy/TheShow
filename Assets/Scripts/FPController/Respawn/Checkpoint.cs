using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// Is used to change the spawn point for the player, in the PlayerRespawn script, when the player collides with the checkpoint.
    /// </summary> 
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerRespawn>().targetSpawnpoint = transform.gameObject;
            GetComponent<Collider>().enabled = false;
            //Param need to changes to the right BoxCollider marked as trigger.
            GameObject.Find("GameResetManager").GetComponent<GameObjectPositionReset>().UpdateListWithGameObjects(GetComponent<BoxCollider>());
        }
   
    }
}
