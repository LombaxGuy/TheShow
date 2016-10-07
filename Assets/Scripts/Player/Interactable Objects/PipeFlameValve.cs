using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractableObjectComponent))]
public class PipeFlameValve : MonoBehaviour
{
    InteractableObjectComponent interactableObjectComponent;

    [SerializeField]
    private GameObject[] pipeFlames;

    private bool pipeFireing = true;

    private bool outOfArea = false;

    private Animator animator;

    public bool OutOfArea
    {
        get { return outOfArea; }
        set { outOfArea = value; }
    }

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
    void Start()
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

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ThisSpecificBehaviour()
    {

        if (pipeFireing == true)
        {
            animator.SetTrigger("triggerTurn");

            for (int i = 0; i < pipeFlames.Length; i++)
            {
                pipeFlames[i].GetComponent<PipeScript>().OnTimerExpired();
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
                animator.SetTrigger("triggerReset");

                pipeFlames[i].GetComponent<PipeScript>().isActivated = true;
                pipeFireing = true;
            }
        }
    }
}
