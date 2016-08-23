using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The box collider used to find all the objects that should be reset on respawn. If left blank no object will be saved.")]
    private BoxCollider savePositionsWithinBox;
    private GameObjectPositionReset resetPositionsScript;

    private Transform resetTransform;
    private string gameResetManagerName = "GameResetManager";

    private void Start()
    {
        resetTransform = transform.GetChild(0).transform;

        try
        {
            resetPositionsScript = GameObject.Find(gameResetManagerName).GetComponent<GameObjectPositionReset>();
        }
        catch
        {
            Debug.Log("Checkpoint.cs: No GameObject with the name '" + gameResetManagerName + "' could be found in the scene!");
        }
    }


    /// <summary>
    /// Is used to change the spawn point for the player, in the PlayerRespawn script, when the player collides with the checkpoint.
    /// </summary> 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerRespawn respawnScript = other.GetComponent<PlayerRespawn>();

            // Only if the player is alive can a checkpoint be triggered
            if (respawnScript.IsAlive)
            {
                //If the checkpoint is our Mastercheckpoint this code is run
                if (this.tag == "MasterRespawn")
                {
                    //Saves the current position of the player
                        SaveGame save = new SaveGame();
                        save.playerPosX = this.transform.position.x;
                        save.playerPosY = this.transform.position.y;
                        save.playerPosZ = this.transform.position.z;
                        SaveLoad.Save(save);

                        respawnScript.targetSpawnpoint = transform.gameObject;
                        GetComponent<Collider>().enabled = false;
                }

                // If the 'savePositionsWithinBox' is not left blank (is not null)
                if (savePositionsWithinBox)
                {
                    resetPositionsScript.UpdateListWithGameObjects(GetComponent<BoxCollider>());
                }
                else
                {
                    Debug.Log("Checkpoint.cs: No positions will be saved because 'savePositionsWithinBox' has not been set!");
                }
            }
        }
    }

    /// <summary>
    /// Returns the resetTransform
    /// </summary>
    /// <returns>The transform of the child object.</returns>
    public Transform GetRespawnTransform()
    {
        return resetTransform;
    }

    /// <summary>
    /// Draws the box and rotation gizmos.
    /// </summary>
    void OnDrawGizmos()
    {
        // Sets matrix to also take rotation into account
        Matrix4x4 newMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = newMatrix;

        // Draws the collider box of the checkpoint
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);

        // Draws the forward vector for the SpawnRotation gameobject. Position is already set by the new matrix.
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(0, 0, 0), transform.GetChild(0).transform.forward);

        // Draws the up vector for the SpawnRotation gameobject. Position is already set by the new matrix.
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(0, 0, 0), transform.GetChild(0).transform.up);

        if (savePositionsWithinBox != null && savePositionsWithinBox.isTrigger)
        {
            newMatrix = Matrix4x4.TRS(savePositionsWithinBox.transform.position, savePositionsWithinBox.transform.rotation, savePositionsWithinBox.transform.lossyScale);
            Gizmos.matrix = newMatrix;

            Gizmos.color = new Color(0, 0, 1, 0.2f);
            Gizmos.DrawCube(savePositionsWithinBox.center, savePositionsWithinBox.size);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(savePositionsWithinBox.center, savePositionsWithinBox.size);
        }
    }
}
