// This script should be attached to all interactable objects (Tagged objects with "Interactable"). Interactable objects are things like doors and buttons and not things that can be picked up.
// Along with this script another script which defines the behaviour of the object when it is interacted with must also be pressent for anything to happen. 
// The 'Behaviour' script mentioned above must set the behaviourDelegate in this script to a method which matches the delegate signature.
// 
using UnityEngine;
using System.Collections;

public class InteractableObjectComponent : MonoBehaviour
{
    public delegate void BehaviourDelegate();
    public BehaviourDelegate behaviourDelegate;

    public void InteractWithObject()
    {
        // Calls the delegate if it's not null
        if (behaviourDelegate != null)
        {
            behaviourDelegate();
        }
    }
}