using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// Locks and hides the cursor.
    /// </summary>
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Unlocks and shows the cursor.
    /// </summary>
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
#if (DEBUG)
        Cursor.lockState = CursorLockMode.None;
#endif
        Cursor.visible = true;
    }
}
