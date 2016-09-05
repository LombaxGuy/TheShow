using UnityEngine;
using System.Collections;

public class PlayerOnGameObjectMovement : MonoBehaviour {

    //GameObject Y scale need to be 1, since it will mess til Player controller
    [Header("Change the speed.")]
    [SerializeField]
    private float speed = 2;
    [Header("Move when Player is on it?")]
    [SerializeField]
    private bool moveOnPlayerTouch = false;
    [Header("Move empty gameObject locations")]
    [SerializeField]
    private GameObject gameObjectLocationOne;
    [SerializeField]
    private GameObject gameObjectLocationTwo;
    [SerializeField]
    private int distance = 1;
    [Tooltip("This is one of the gameobjects it will start moving to.")]
    [SerializeField]
    private bool LocationOneFirst = false;

    private bool isMovingWithPlayer = false;


    void FixedUpdate()
    {
        if (moveOnPlayerTouch == false)
        {
            GameObjectMove();
        }
        else
        {
            if (isMovingWithPlayer == true)
            {
                GameObjectMoveOnPlayerTouch();
            }
            else
            {
                GameObjectMoveBack();
            }
            
        }
    }

    /// <summary>
    /// Getting collision data and if Player is in it. It will make the GameObject parent to Player.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.transform.parent.tag == "Player")
        {
            other.gameObject.transform.parent.transform.parent = transform.parent;
        }

        isMovingWithPlayer = true;
    }

    /// <summary>
    /// When Player is leaving the collision area. Then gameobject will no longer be parent to Player.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            other.gameObject.transform.parent.transform.parent = null;
        }

        isMovingWithPlayer = false;
    }
    
    /// <summary>
    /// If move gameobject only when player touch, Then it will move to the distination and then stop moving.
    /// </summary>
    private void GameObjectMoveOnPlayerTouch()
    {

        if (Distance(false) > distance)
        {
            Move(false);
        } 
    }

    /// <summary>
    /// If the player is not touching it, it will go back to start location
    /// </summary>
    private void GameObjectMoveBack()
    {

        if (Distance(true) > distance)
        {
            Move(true);
        }
            
    }


    /// <summary>
    /// Move the gameObject
    /// </summary>
    /// <param name="direction"></param>
    private void Move(bool direction)
    {
        if(direction == true)
        {
            transform.parent.Translate((gameObjectLocationOne.transform.position - transform.parent.position).normalized * Time.deltaTime * speed);
        }
        else
        {
            transform.parent.Translate((gameObjectLocationTwo.transform.position - transform.parent.position).normalized * Time.deltaTime * speed);
        }
    }


    /// <summary>
    /// Gets the distance between this gameObject and on of the location gameobjects, and return it as float.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private float Distance(bool direction)
    {
        float distanceBetween = 0;

        if(direction == true)
        {
            distanceBetween = Vector3.Distance(gameObjectLocationOne.transform.position, transform.parent.position);
        }
        else
        {
            distanceBetween = Vector3.Distance(gameObjectLocationTwo.transform.position, transform.parent.position);
        }

        return distanceBetween; 
    }


    /// <summary>
    /// Will make the GameObject move towards empty GameObject
    /// </summary>
    private void GameObjectMove()
    {

        if (LocationOneFirst == true)
        {
            Move(true);

            if (Distance(true) < distance)
            {
                LocationOneFirst = false;
            }
        }
        else
        {
            Move(false);

            if (Distance(false) < distance)
            {
                LocationOneFirst = true;
            }
        }
    }

    
}
