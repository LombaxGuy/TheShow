﻿using UnityEngine;
using System.Collections;

public class PlayerOnGameObjectMovement : MonoBehaviour {

    //GameObject Y scale need to be 1, since it will mess til Player controller
    [Header("Change the speed.")]
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private bool moveOnPlayerTouch = false;
    [Header("Empty GameObjects")]
    [SerializeField]
    private GameObject gameObjectLocationOne;
    [SerializeField]
    private GameObject gameObjectLocationTwo;
    [SerializeField]
    private int detectionSize = 1;
    [Tooltip("This is one of the gameobjects it will start moving to.")]
    [SerializeField]
    private bool LocationOneFirst = false;

    

    // Use this for initialization
    void Start()
    {

    }


    /// <summary>
    /// If moveOnPlayerTouch is false, then the GameObject will move on its own.
    /// </summary>
    void Update ()
    {
        if(moveOnPlayerTouch == false)
        {
            GameObjectMove();
        }	

        
	}
    /// <summary>
    /// Getting collision data and if Player is in it. It will make the GameObject parent to Player.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            collision.gameObject.transform.parent = transform.parent;
        }
    }

    /// <summary>
    /// When Player is leaving the collision area. Then gameobject will no longer be parent to Player.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            collision.gameObject.transform.parent = null;
        }
    }


    /// <summary>
    /// If moveOnPlayerTouch is true. Then when the Player is on the GameObject, then it will run GameObjectMove, that will make the GameObject move.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerStay(Collider collision)
    {


        if (moveOnPlayerTouch == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObjectMove();
            }
        }


    }

    /// <summary>
    /// Will make the GameObject move towards empty GameObject
    /// </summary>
    private void GameObjectMove()
    {

        if (LocationOneFirst == true)
        {
            transform.parent.Translate((gameObjectLocationOne.transform.position - transform.parent.position).normalized * Time.deltaTime * speed);

            if (gameObjectLocationOne.transform.position.x - transform.parent.position.x <= detectionSize && gameObjectLocationOne.transform.position.x - transform.parent.position.x >= -detectionSize &&
                gameObjectLocationOne.transform.position.y - transform.parent.position.y <= detectionSize && gameObjectLocationOne.transform.position.y - transform.parent.position.y >= -detectionSize &&
                gameObjectLocationOne.transform.position.z - transform.parent.position.z <= detectionSize && gameObjectLocationOne.transform.position.z - transform.parent.position.z >= -detectionSize)
            {
                LocationOneFirst = false;
            }
        }
        else
        {
            transform.parent.Translate((gameObjectLocationTwo.transform.position - transform.parent.position).normalized * Time.deltaTime * speed);

            if (gameObjectLocationTwo.transform.position.x - transform.parent.position.x <= detectionSize && gameObjectLocationTwo.transform.position.x - transform.parent.position.x >= -detectionSize &&
                gameObjectLocationTwo.transform.position.y - transform.parent.position.y <= detectionSize && gameObjectLocationTwo.transform.position.y - transform.parent.position.y >= -detectionSize &&
                gameObjectLocationTwo.transform.position.z - transform.parent.position.z <= detectionSize && gameObjectLocationTwo.transform.position.z - transform.parent.position.z >= -detectionSize)
            {
                LocationOneFirst = true;
            }
        }
    }

    
}
