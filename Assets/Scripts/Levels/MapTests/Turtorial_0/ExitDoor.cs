using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class ExitDoor : MonoBehaviour
{
    private bool locked = true;

    [SerializeField]
    private string nextLevelName = "";

    private void OnEnable()
    {
        EventManager.OnMusicSaved += OnMusicSaved;
    }

    private void OnDisable()
    {
        EventManager.OnMusicSaved -= OnMusicSaved;
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
            //Debug.Log("Behaviour assigned to the BehaviourDelegate.");
        }
        else
        {
            Debug.Log("TemplateBehaviourComponent.cs: No 'InteractableObjectComponent' was found!");
        }
    }

    /// <summary>
    /// Defines the behaviour of the object when it is interacted with.
    /// </summary>
    private void ThisSpecificBehaviour()
    {
        if (!locked)
        {
            GetComponent<Animator>().SetTrigger("openDoor");
        }
    }

    private void OnMusicSaved()
    {
        locked = false;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

}
