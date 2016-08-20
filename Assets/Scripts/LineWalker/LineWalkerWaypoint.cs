using UnityEngine;
using System.Collections;

public class LineWalkerWaypoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Waypoint");
    }
}
