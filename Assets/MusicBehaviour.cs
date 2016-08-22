using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class MusicBehaviour : MonoBehaviour {

    [SerializeField]
    private Camera playerCam;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private bool musicMode = false;

    public Camera PlayerCam
    {
        get { return playerCam; }
    }

    public Camera Cam
    {
        get { return Cam; }
    }

    // The script containing the delegate
    InteractableObjectComponent interactableObjectComponent;

    // Use this for initialization
    void Start()
    {
        // Get the script
        interactableObjectComponent = GetComponent<InteractableObjectComponent>();

        // If the component exists on the object we assign a behaviour to the delegate in the script component.
        if (interactableObjectComponent != null)
        {
            interactableObjectComponent.behaviourDelegate = ThisSpecificBehaviour;
            Debug.Log("Music Behaviour assigned to the BehaviourDelegate.");
        }
        else
        {
            Debug.Log("No 'InteractableObjectComponent' was found!");
        }
    }

    /// <summary>
    /// Defines the behaviour of the object when it is interacted with.
    /// </summary>
    private void ThisSpecificBehaviour()
    {
        if(playerCam.enabled == true)
        {
            if (musicMode == false)
            {
                playerCam.GetComponent<Transform>().LookAt(transform);

                GetComponent<MusicRecord>().PlayerClicked = !GetComponent<MusicRecord>().PlayerClicked;
            }
            else
            {
                cam.SetActive(!cam.activeSelf);
                playerCam.enabled = !playerCam.enabled;
                GetComponent<MusicRecord>().PlayerClicked = !GetComponent<MusicRecord>().PlayerClicked;
            }
        }
        
    }

    public void GetOut()
    {
        cam.SetActive(!cam.activeSelf);
        playerCam.enabled = !playerCam.enabled;
        GetComponent<MusicRecord>().PlayerClicked = !GetComponent<MusicRecord>().PlayerClicked;
    }
}
