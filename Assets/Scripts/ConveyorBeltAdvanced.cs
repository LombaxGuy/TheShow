﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBeltAdvanced : MonoBehaviour {


    enum Direction { FORWARD, BACK, LEFT, RIGHT, UP, DOWN }

    [SerializeField]
    private GameObject rotationStartGameObject;
    [SerializeField]
    private GameObject startGameObject;
    [SerializeField]
    private GameObject endGameObject;
    [SerializeField]
    private GameObject rotationGameObject;

    [SerializeField]
    private GameObject gameObjectsSpawn;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float rotationSpeed = 100;
    [SerializeField]
    private float distanceStartToRotationStart = 1;

    //"bane" er lavet uden for mappet så playeren ikke kan se det. De vil komme ind på båndet efter tid. Info om hvor den skal spawn er på objecter.
    [Header("Add GameObjects to the notActive")]
    [SerializeField]
    private List<GameObject> notActive;
    [SerializeField]
    private List<gameObjectData> active;
    [SerializeField]
    private List<gameObjectData> rotationList;
    [SerializeField]
    private Direction dir = Direction.FORWARD;

    private List<originalObjectPosAndRot> copyNotActive;

    private int gameObjectCount;
    private int gameObjectCounter = 0;
    private float timer = 1;
    private float counter = 0;

    struct originalObjectPosAndRot
    {
        private Vector3 objPos;
        private Quaternion objRot;

        public Vector3 ObjPos
        {
            get { return objPos; }
            set { objPos = value; }
        }

        public Quaternion ObjRot
        {
            get { return objRot; }
            set { objRot = value; }
        }

    }

    [System.Serializable]
     struct gameObjectData
    {
        private GameObject obj;
        private float distance;
        private float lane;

        public GameObject Obj
        {
            get { return obj; }
            set { obj = value; }
        }

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public float Lane
        {
            get { return lane; }
            set { lane = value; }
        }
    }




    /// <summary>
    /// All child gameObjects in gameObjectsSpawn will get added to the notActive list. 
    /// the notActive list gameobjects position and rotation will get saved down to the copyNotActive listen. It is a list with a struct in it.
    /// </summary>

    // Use this for initialization
    void Start ()
    {

        copyNotActive = new List<originalObjectPosAndRot>();

        if (gameObjectsSpawn.transform.childCount != 0)
        {
            for (int i = 0; i < gameObjectsSpawn.transform.childCount; i++)
            {
                notActive.Add(gameObjectsSpawn.transform.GetChild(i).gameObject);
            }
            

        }
        else
        {
            Debug.Log("There is no child objects");
        }

        gameObjectCount = notActive.Count;

        for (int i = 0; i < notActive.Count; i++)
        {
            originalObjectPosAndRot temp = new originalObjectPosAndRot();
            temp.ObjPos = notActive[i].transform.localPosition;
            temp.ObjRot = notActive[i].transform.localRotation;
            copyNotActive.Add(temp);
        }
        

	}

	
    
    	// Update is called once per frame
	void Update ()
    {
        RotateGameObject();
        SpawnTheObjects();
        ActiveListUpdate();
	}


    void OnEnable()
    {
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDisable()
    {
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

   /// <summary>
   /// Controls the players movement.
   /// </summary>
   /// <param name="collision"></param>

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            switch (dir)
            {
                case Direction.FORWARD:
                    collision.rigidbody.MovePosition(collision.transform.position + transform.forward * Time.deltaTime * 2);
                    break;
                case Direction.BACK:
                    collision.rigidbody.MovePosition(collision.transform.position + -transform.forward * Time.deltaTime * 2);
                    break;
                case Direction.LEFT:
                    collision.rigidbody.MovePosition(collision.transform.position + -transform.right * Time.deltaTime * 2);
                    break;
                case Direction.RIGHT:
                    collision.rigidbody.MovePosition(collision.transform.position + transform.right * Time.deltaTime * 2);
                    break;
                case Direction.UP:
                    collision.rigidbody.MovePosition(collision.transform.position + transform.up * Time.deltaTime * 2);
                    break;
                case Direction.DOWN:
                    collision.rigidbody.MovePosition(collision.transform.position + -transform.up * Time.deltaTime * 2);
                    break;
                default:
                    break;
            }
        }
    }


    /// <summary>
    /// If the counter is under 0, then it will move a gameobject from notActive to the active list.
    /// It will take the gameobjects localposition, set parent to the conveyour belt transform, set the distance between start and rotationstart 
    /// and then it will make a new gameObjectData struct and take it to the active list and remove it from notActive.
    /// </summary>
    private void SpawnTheObjects()
    {

        counter -= Time.deltaTime;

        if(notActive.Count != 0 && counter <= 0)
        {
            counter = timer; //Skal laves om

            gameObjectData god = new gameObjectData();
            god.Lane = notActive[0].transform.localPosition.x;
            notActive[0].transform.SetParent(transform);
            notActive[0].transform.localPosition = new Vector3(startGameObject.transform.localPosition.x + god.Lane,
                                                          startGameObject.transform.localPosition.y,
                                                          startGameObject.transform.localPosition.z);

            distanceStartToRotationStart = Vector3.Distance(notActive[0].transform.localPosition, rotationStartGameObject.transform.localPosition);
            Debug.Log(Vector3.Distance(notActive[0].transform.localPosition, rotationStartGameObject.transform.localPosition));
            
            god.Obj = notActive[0];
            god.Distance = distanceStartToRotationStart + 0.01f;

            active.Add(god);

            notActive.RemoveAt(0);
        }
    }

    /// <summary>
    /// When there is something in the active list, then it will move the gameObjects untill it reach its distination.
    /// When it reaches its distination, the gameObject will get a new parent. 
    /// The parent is the rotation gameObject. 
    /// The gameObject will then get added to the rotation list and removed from active list.
    /// </summary>

    private void ActiveListUpdate()
    {

        if(active.Count != 0)
        {

            gameObjectData objRemove = active[0];
            bool removeTime = false;

            for (int i = 0; i < active.Count; i++)
            {
                if (Vector3.Distance(active[i].Obj.transform.localPosition, rotationGameObject.transform.localPosition) <= active[i].Distance)
                {
                    active[i].Obj.transform.SetParent(rotationGameObject.transform);
                    rotationList.Add(active[i]);
                    objRemove = active[i];
                    removeTime = true;
                    
                }
                else
                {
                    active[i].Obj.transform.Translate(transform.forward * Time.deltaTime * speed);
                }


            }
            if(removeTime == true)
            {
                removeTime = false;
                active.Remove(objRemove);
            }
            
        }


    }


    /// <summary>
    /// Rotates the rotationGameObject.
    /// Get all the distance information for the gameObjects in the rotationList.
    /// If the distance is under 0.7 then it will get removed from the rotationList and runs the LoopGameObjects method.
    /// For every gameobject in the rotatonList will change parent 2 times for each loop of the method. 
    /// </summary>
    private void RotateGameObject()
    {
        rotationGameObject.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);

        if(rotationList.Count != 0)
        {
            

            gameObjectData objectData = rotationList[0];
            bool remove = false;

            for (int i = 0; i < rotationList.Count; i++)
            {
                rotationList[i].Obj.transform.SetParent(transform);
                if (Vector3.Distance(rotationList[i].Obj.transform.localPosition, 
                    new Vector3(endGameObject.transform.localPosition.x + rotationList[i].Lane, 
                    endGameObject.transform.localPosition.y, endGameObject.transform.localPosition.z)) <= 0.7f)
                {
                    objectData = rotationList[i];
                    remove = true;
                }
                rotationList[i].Obj.transform.SetParent(rotationGameObject.transform);
            }

            if (remove == true)
            {
                objectData.Obj.transform.parent = null;
                GameObject gameObj = objectData.Obj;
                rotationList.Remove(objectData);
                LoopGameObjects(gameObj);
                remove = false;
            }
        }
    }

    /// <summary>
    /// The obj will get a new parent, "gameObjectsSpawn" transform. Then it will change the localPosition and localRotation to the original position and rotation.
    /// It will then get added to the notActive list. So the gameObject can get spawned again.
    /// gameObjectCount is changed to how many gameObjects there will be on the conveyour belt. 
    /// gameObjectCounter is to get the obj real saved localPosition and localRotation. 
    /// </summary>
    /// <param name="obj"></param>

    private void LoopGameObjects(GameObject obj)
    {
        if(gameObjectCounter >= gameObjectCount)
        {
            gameObjectCounter = 0;
        }

        obj.transform.SetParent(gameObjectsSpawn.transform);
        obj.transform.localPosition = copyNotActive[gameObjectCounter].ObjPos;
        obj.transform.localRotation = copyNotActive[gameObjectCounter].ObjRot;

        gameObjectCounter++;
        notActive.Add(obj);
    }

    /// <summary>
    /// Maybe future plans. This is called whenever the player dies. It is a global method.
    /// </summary>
    public void OnPlayerRespawn()
    {
        Debug.Log("Event Called");
    }

}
