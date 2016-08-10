using UnityEngine;
using System.Collections;

public class GameObjectPositionReset : MonoBehaviour {

    private GameObject[] gameObjects;

    private Vector3[] gameObjectsStartLocation;

    private Quaternion[] gameObjectStartRotation;

    private int gameObjectWithTagCount;

    private Vector3 center;

    private int radius;

    void Awake()
    {
       

    }

    private void Test()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("CanRespawn");

        gameObjectWithTagCount = GameObject.FindGameObjectsWithTag("CanRespawn").Length;

        if (gameObjectWithTagCount != 0)
        {
            gameObjectsStartLocation = new Vector3[gameObjectWithTagCount];

            gameObjectStartRotation = new Quaternion[gameObjectWithTagCount];

            for (int i = 0; i < gameObjects.Length; i++)
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

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void GameObjectToStartLocation()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].transform.position = gameObjectsStartLocation[i];
            gameObjects[i].transform.rotation = gameObjectStartRotation[i];
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if(hitColliders[i].gameObject.tag == "CanRespawn")
                {
                    gameObjects[i] = hitColliders[i].gameObject;
                }
                
            }


            if (gameObjects != null && gameObjects.Length != 0)
            {
                gameObjectsStartLocation = new Vector3[gameObjects.Length];

                gameObjectStartRotation = new Quaternion[gameObjects.Length];

                for (int i = 0; i < gameObjects.Length; i++)
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

    }


}
