using UnityEngine;
using System.Collections;

public class DeathArea : MonoBehaviour
{
    /// <summary>
    /// When an object enters the trigger box it is cheked if it is the player. If it is the player is killed.
    /// </summary>
    /// <param name="other">The object that entered the trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        // If the other object is tagged 'Player'...
        if(other.tag == "Player")
        {
            PlayerRespawn respawnScript = other.GetComponent<PlayerRespawn>();
            
            //... and the player is still alive...
            if (respawnScript.IsAlive)
            {
                //... the player is killed.
                respawnScript.Kill();
            }
        }
    }
}
