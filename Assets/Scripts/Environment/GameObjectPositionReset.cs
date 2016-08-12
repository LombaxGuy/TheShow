using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPositionReset : MonoBehaviour {

    private List<GameObject> gameObjects;

    private Vector3[] gameObjectsStartLocation;

    private Quaternion[] gameObjectStartRotation;

    private Vector3[] gameObjectsVelocity;

    private Vector3[] gameObjectsAngularVelocity;

    private int gameObjectWithTagCount;

    // Use this for initialization
    void Start ()
    {
        gameObjects = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    /// <summary>
    /// OverlapBox is created with the BoxCollider information in the param. 
    /// It takes all the colliders in the area and place it in the gameObjects list, if they have the canRespawn tag.
    /// If there is already something in the list. It clears the list and insert new gameobjects in it.
    /// When the list contains gameobjects its position and rotation will be stored in 2 arrays. One with Vector3 and one with Quaternion.
    /// </summary>
    /// <param name="col"></param>
    public void UpdateListWithGameObjects(BoxCollider col)
    {
        Collider[] hitColliders = Physics.OverlapBox(col.transform.position + col.center, new Vector3(col.size.x, col.size.y, col.size.z) / 2, Quaternion.identity);

        if (gameObjects.Count == 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log("Gameobject : " + hitColliders[i].tag);

                if (hitColliders[i].tag == "CanRespawn")
                {
                    gameObjects.Add(hitColliders[i].gameObject);
                    Debug.Log("-> " + hitColliders[i].gameObject.GetComponent<Rigidbody>().velocity);
                }

            }
        }
        else
        {
            gameObjects.Clear();

            Debug.Log("gameObjects list has been cleared");

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log("Gameobject : " + hitColliders[i].tag);

                if (hitColliders[i].tag == "CanRespawn")
                {
                    gameObjects.Add(hitColliders[i].gameObject);
                }

            }

        }

        if (gameObjects.Count != 0)
        {
            gameObjectsStartLocation = new Vector3[gameObjects.Count];

            gameObjectStartRotation = new Quaternion[gameObjects.Count];

            gameObjectsVelocity = new Vector3[gameObjects.Count];

            gameObjectsAngularVelocity = new Vector3[gameObjects.Count];

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjectsStartLocation[i] = gameObjects[i].transform.position;
                gameObjectStartRotation[i] = gameObjects[i].transform.rotation;
                gameObjectsVelocity[i] = gameObjects[i].GetComponent<Rigidbody>().velocity;
                gameObjectsAngularVelocity[i] = gameObjects[i].GetComponent<Rigidbody>().angularVelocity;
                Debug.Log("Object : " + i + " position: " + gameObjects[i].transform.position + " Rotation: " + gameObjects[i].transform.rotation + " Velocity: " + gameObjects[i].GetComponent<Rigidbody>().velocity + " angularVelocity: " + gameObjects[i].GetComponent<Rigidbody>().angularVelocity);

            }

            Debug.Log("Position and Rotation is saved.");
        }
        else
        {
            Debug.Log("GameObjects with the tag canRespawn not found");
        }



    }

    /// <summary>
    /// This method is to get all the objects to get to their start position and rotation.
    /// </summary>
    public void GameObjectToStartLocation()
    {
        if(gameObjects.Count != 0)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = gameObjectsStartLocation[i];
                gameObjects[i].transform.rotation = gameObjectStartRotation[i];
                gameObjects[i].GetComponent<Rigidbody>().velocity = gameObjectsVelocity[i];
                gameObjects[i].GetComponent<Rigidbody>().angularVelocity = gameObjectsAngularVelocity[i];
            }
        }

    }




}
