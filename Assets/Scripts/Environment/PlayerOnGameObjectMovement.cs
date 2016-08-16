using UnityEngine;
using System.Collections;

public class PlayerOnGameObjectMovement : MonoBehaviour {

    //GameObject Y scale need to be 1, since it will mess til Player controller
    [Header("Change the speed.")]
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private bool moveOnPlayerTouch = false;
    [Header("Empty GameObjects")]
    [SerializeField]
    private GameObject gameObjectLocationOne;
    [SerializeField]
    private GameObject gameObjectLocationTwo;
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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    /// <summary>
    /// When Player is leaving the collision area. Then gameobject will no longer be parent to Player.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }


    /// <summary>
    /// If moveOnPlayerTouch is true. Then when the Player is on the GameObject, then it will run GameObjectMove, that will make the GameObject move.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision)
    {
        if(moveOnPlayerTouch == true)
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


        //transform.position = new Vector3(Mathf.PingPong(Time.time, 3) + transform.position.x, transform.position.y, transform.position.z);

        if (LocationOneFirst == true)
        {
            transform.Translate((gameObjectLocationOne.transform.position - transform.position).normalized * Time.deltaTime * speed);

            if (gameObjectLocationOne.transform.position.x - transform.position.x <= 1 && gameObjectLocationOne.transform.position.x - transform.position.x >= -1 &&
                gameObjectLocationOne.transform.position.y - transform.position.y <= 1 && gameObjectLocationOne.transform.position.y - transform.position.y >= -1 &&
                gameObjectLocationOne.transform.position.z - transform.position.z <= 1 && gameObjectLocationOne.transform.position.z - transform.position.z >= -1)
            {
                LocationOneFirst = false;
            }
        }
        else
        {
            transform.Translate((gameObjectLocationTwo.transform.position - transform.position).normalized * Time.deltaTime * speed);

            if (gameObjectLocationTwo.transform.position.x - transform.position.x <= 1 && gameObjectLocationTwo.transform.position.x - transform.position.x >= -1 &&
                gameObjectLocationTwo.transform.position.y - transform.position.y <= 1 && gameObjectLocationTwo.transform.position.y - transform.position.y >= -1 &&
                gameObjectLocationTwo.transform.position.z - transform.position.z <= 1 && gameObjectLocationTwo.transform.position.z - transform.position.z >= -1)
            {
                LocationOneFirst = true;
            }
        }
    }

    
}
