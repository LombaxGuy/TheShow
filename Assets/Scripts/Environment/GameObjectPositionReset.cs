using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPositionReset : MonoBehaviour {


    //Remember to change gameObjects to either List version or Array. When using different methods.
    //private GameObject[] gameObjects;
    private List<GameObject> gameObjects;

    private Vector3[] gameObjectsStartLocation;

    private Quaternion[] gameObjectStartRotation;

    private int gameObjectWithTagCount;

    [SerializeField]
    private float size = 5;


    
    void Awake()
    {
        //FindGameObjectsInTheScene();
    }

    // Use this for initialization
    void Start ()
    {
        gameObjects = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //This is going to be removed, when testing is done.
        if(Input.GetKeyDown(KeyCode.K))
        {
            GameObjectToStartLocation();
        }
	}


    /// <summary>
    /// This method is to get all the objects to get to their start position and rotation.
    /// </summary>
    public void GameObjectToStartLocation()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.position = gameObjectsStartLocation[i];
            gameObjects[i].transform.rotation = gameObjectStartRotation[i];
        }
    }

    /// <summary>
    /// This method is getting all gameobjects with colliders and return them, within a range. 
    /// Its position for this box, is the empty gameobject.
    /// Range can be changed by placing a number in the editor where "size" is.
    /// </summary>
    private void FindGameObjectsOverlapBox()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, Vector3.one * size, Quaternion.identity);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log("Gameobject : " + hitColliders[i].tag);
            if (hitColliders[i].tag == "CanRespawn")
            {
                gameObjects.Add(hitColliders[i].gameObject);
            }

        }

        if (gameObjects.Count != 0)
        {
            gameObjectsStartLocation = new Vector3[gameObjects.Count];

            gameObjectStartRotation = new Quaternion[gameObjects.Count];

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjectsStartLocation[i] = gameObjects[i].transform.position;
                gameObjectStartRotation[i] = gameObjects[i].transform.rotation;

            }

            Debug.Log("Pos and Rotation is saved.");
        }
        else
        {
            Debug.Log("GameObjects with the tag canRespawn not found");
        }
    }



    /// <summary>
    /// This method get all the objects in the hierachy with the tag "CanRespawn"
    /// The gameObjects is an array. While testing the above method, it is a list.
    /// The method changes the size of the array, when finding new objects.
    /// </summary>
    private void FindGameObjectsInTheScene()
    {
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("CanRespawn"));

        gameObjectWithTagCount = GameObject.FindGameObjectsWithTag("CanRespawn").Length;

        if (gameObjectWithTagCount != 0)
        {
            gameObjectsStartLocation = new Vector3[gameObjectWithTagCount];

            gameObjectStartRotation = new Quaternion[gameObjectWithTagCount];

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjectsStartLocation[i] = gameObjects[i].transform.position;
                gameObjectStartRotation[i] = gameObjects[i].transform.rotation;
            }
        }
        else
        {
            Debug.Log("GameObjects with the tag canRespawn not found");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            FindGameObjectsOverlapBox();
        }

    }


}
