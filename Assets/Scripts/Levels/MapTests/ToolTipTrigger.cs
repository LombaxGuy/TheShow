using UnityEngine;
using System.Collections;

public class ToolTipTrigger : MonoBehaviour
{
    private enum HelpText { HowToMove, HowToJump, HowToCrouch, HowToInteract}

    [SerializeField]
    private HelpText tooltipText = HelpText.HowToMove;

    private GameObject worldManager;

    private Collider trigger;

    // Use this for initialization
    void Start()
    {
        worldManager = GameObject.Find("WorldManager");
        trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter()
    {
        trigger.enabled = false;

        string tooltip = "";

        switch (tooltipText)
        {
            case HelpText.HowToMove:
                tooltip = string.Format("Use {0}, {1}, {2} ,{3} to move around", KeyBindings.KeyMoveForward, KeyBindings.KeyMoveLeft, KeyBindings.KeyMoveBackward, KeyBindings.KeyMoveRight);
                break;

            case HelpText.HowToJump:
                tooltip = string.Format("Use {0} to jump.", KeyBindings.KeyMoveJump);
                break;

            case HelpText.HowToCrouch:
                tooltip = string.Format("Use {0} to crouch.", KeyBindings.KeyMoveCrouch);
                break;

            case HelpText.HowToInteract:
                tooltip = string.Format("Use {0} to interact with objects.", KeyBindings.KeyInteraction);
                break;
        }
        
        worldManager.GetComponent<Tooltip>().DisplayTooltipForSeconds(tooltip, 3);
    }
}
