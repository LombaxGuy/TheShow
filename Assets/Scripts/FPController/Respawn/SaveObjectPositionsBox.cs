using UnityEngine;
using System.Collections;

public class SaveObjectPositionsBox : MonoBehaviour
{
    /// <summary>
    /// Draws the gizmo for the box collider
    /// </summary>
    private void OnDrawGizmos()
    {
        // Sets matrix to also take rotation into account
        Matrix4x4 newMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = newMatrix;

        // Draws the collider box of the save positions collider
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}
