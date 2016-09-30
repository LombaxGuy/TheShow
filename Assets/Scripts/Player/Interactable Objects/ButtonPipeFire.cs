using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class ButtonPipeFire : MonoBehaviour {

    InteractableObjectComponent interactableObjectComponent;

    [SerializeField]
    private GameObject[] pipeFlames;

    private bool pipeFireing = true;

    public bool outOfArea = false;



    private void OnEnable()
    {
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }
    //
    // Use this for initialization
    void Start ()
    {
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
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ThisSpecificBehaviour()
    {

        if (pipeFireing == true)
        {
            for (int i = 0; i < pipeFlames.Length; i++)
            {
                pipeFlames[i].GetComponent<PipeScript>().isActivated = false;
                pipeFireing = false;
            }
        }


    }

    private void OnPlayerRespawn()
    {
        if (pipeFireing == false && outOfArea == false)
        {
            for (int i = 0; i < pipeFlames.Length; i++)
            {
                pipeFlames[i].GetComponent<PipeScript>().isActivated = true;
                pipeFireing = true;
            }
        }
    }
}
