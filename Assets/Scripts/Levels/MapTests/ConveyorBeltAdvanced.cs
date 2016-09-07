using UnityEngine;
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
    [SerializeField]
    private float timer = 1;
    private float counter = 0;
    private float lastTimeZ = 0;
    private bool timerReady = false;
    private float newTimeZ = 0;
    private gameObjectData lastObj;
    private bool lastObjectSpawned = false;

    private GameObject player;

    private bool playerOnConveyor = false;

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
        private float height;

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
        public float Height
        {
            get { return height; }
            set { height = value; }
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


    void FixedUpdate()
    {
        if(playerOnConveyor == true)
        {
            player.GetComponent<Rigidbody>().MovePosition(player.transform.position + transform.forward * Time.deltaTime * speed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerOnConveyor = true;
            player = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerOnConveyor = false;
        }
    }


    /// <summary>
    /// If the counter is under 0, then it will move a gameobject from notActive to the active list.
    /// It will take the gameobjects localposition, set parent to the conveyour belt transform, set the distance between start and rotationstart 
    /// and then it will make a new gameObjectData struct and take it to the active list and remove it from notActive.
    /// </summary>
    private void SpawnTheObjects()
    {

        if(timerReady == false)
        {
            counter = newTimeZ - lastTimeZ;
            lastTimeZ = newTimeZ;
            timerReady = true;
        }

        counter -= Time.deltaTime;

        if(notActive.Count == 0 && counter <= 0 && lastObjectSpawned == false)
        {
            Debug.Log("YO");
            active.Add(lastObj);
            lastTimeZ = 0;
            lastObjectSpawned = true;
        }

        if(notActive.Count != 0 && counter <= 0 && timerReady == true)
        {

            newTimeZ = notActive[0].transform.localPosition.z;
           
            if(lastObj.Obj != null)
            {
                active.Add(lastObj);
            }
            lastObj = GameObjectData(notActive[0]);
            notActive.RemoveAt(0);
            timerReady = false;
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
                if (Vector3.Distance(rotationList[i].Obj.transform.position, 
                    new Vector3(endGameObject.transform.position.x + rotationList[i].Lane, 
                    endGameObject.transform.position.y - rotationList[i].Height, endGameObject.transform.position.z)) <= 0.7f)
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

        active.Add(GameObjectData(obj));
    }

    private gameObjectData GameObjectData(GameObject obj)
    {
        gameObjectData god = new gameObjectData();
        god.Obj = obj;
        god.Lane = obj.transform.localPosition.x;
        god.Height = obj.transform.localPosition.y;

        god.Obj.transform.SetParent(transform);
        god.Obj.transform.localPosition = new Vector3(startGameObject.transform.localPosition.x + god.Lane,
                                                      startGameObject.transform.localPosition.y + god.Height,
                                                      startGameObject.transform.localPosition.z);

        distanceStartToRotationStart = Vector3.Distance(god.Obj.transform.localPosition, rotationStartGameObject.transform.localPosition);

        god.Distance = distanceStartToRotationStart + 0.01f;

        return god;
    }

    /// <summary>
    /// Maybe future plans. This is called whenever the player dies. It is a global method.
    /// </summary>
    public void OnPlayerRespawn()
    {
        Debug.Log("Event Called");
    }

}
