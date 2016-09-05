using UnityEngine;
using System.Collections;

public class KeyBindings : MonoBehaviour
{
    private static KeyCode keyToggleDebug = KeyCode.F1;

    private static KeyCode keyMoveForward = KeyCode.W;
    private static KeyCode keyMoveBackward = KeyCode.S;
    private static KeyCode keyMoveRight = KeyCode.D;
    private static KeyCode keyMoveLeft = KeyCode.A;
    private static KeyCode keyMoveSprint = KeyCode.LeftShift;
    private static KeyCode keyMoveCrouch = KeyCode.LeftControl;
    private static KeyCode keyMoveJump = KeyCode.Space;
    private static KeyCode keyEscape = KeyCode.Q;

    private static KeyCode keyInteraction = KeyCode.E;
    private static KeyCode keyPrimaryAction = KeyCode.Mouse0;

    public static KeyCode KeyToggleDebug
    {
        get { return keyToggleDebug; }
        set { keyToggleDebug = value; }
    }

    public static KeyCode KeyMoveForward
    {
        get { return keyMoveForward; }
        set { keyMoveForward = value;}
    }

    public static KeyCode KeyMoveBackward
    {
        get { return keyMoveBackward; }
        set { keyMoveBackward = value; }
    }

    public static KeyCode KeyMoveRight
    {
        get { return keyMoveRight; }
        set { keyMoveRight = value; }
    }

    public static KeyCode KeyMoveLeft
    {
        get { return keyMoveLeft; }
        set { keyMoveLeft = value; }
    }

    public static KeyCode KeyMoveSprint
    {
        get { return keyMoveSprint; }
        set { keyMoveSprint = value; }
    }

    public static KeyCode KeyMoveCrouch
    {
        get { return keyMoveCrouch; }
        set { keyMoveCrouch = value; }
    }

    public static KeyCode KeyMoveJump
    {
        get { return keyMoveJump; }
        set { keyMoveJump = value; }
    }

    public static KeyCode KeyInteraction
    {
        get { return keyInteraction; }
        set { keyInteraction = value; }
    }

    public static KeyCode KeyPrimaryAction
    {
        get { return keyPrimaryAction; }
        set { keyPrimaryAction = value; }
    }

    public static KeyCode KeyEscape
    {
        get { return keyEscape; }
    }
}
