using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBeltAdvanced : MonoBehaviour
{

    enum Direction { FORWARD, BACK, LEFT, RIGHT, UP, DOWN }
    enum ObjectMode { SPAWNING, MOVING, ROTATING, RESETTING }

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
    private float endDistance;
    private float distanceStartToRotationStart = 1;

    //[SerializeField]
    //private List<GameObject> notActive;
    //[SerializeField]
    //private List<gameObjectData> notActive;
    [SerializeField]
    private List<gameObjectData> active;
    //[SerializeField]
    //private List<gameObjectData> rotationList;
    //[SerializeField]
    //private Direction dir = Direction.FORWARD;

    //private int gameObjectCount;
    //private int gameObjectCounter = 0;
    //[SerializeField]
    //private float timer = 1;
    //private float counter = 0;
    //private float lastTimeZ = 0;
    //private bool timerReady = false;
    //private float newTimeZ = 0;
    //private gameObjectData lastObj;
    //private bool lastObjectSpawned = false;
    //private float lastTime = 0;
    [Tooltip("Change the rotation direction, for the rotating gameobject. It is picking between upside down or normal.")]
    [SerializeField]
    private bool rotationDir;

    private GameObject player;

    private bool playerOnConveyor = false;

    [System.Serializable]
    struct gameObjectData
    {
        private GameObject obj;
        private float distance;
        private float lane;
        private float height;
        private float timeToSpawn;
        private Vector3 oldPos;
        private Quaternion oldRot;
        private ObjectMode objMode;

        public float TimeToSpawn
        {
            get { return timeToSpawn; }
            set { timeToSpawn = value; }
        }

        public ObjectMode ObjMode
        {
            get { return objMode; }
            set { objMode = value; }
        }

        public Vector3 OldPos
        {
            get { return oldPos; }
            set { oldPos = value; }
        }

        public Quaternion OldRot
        {
            get { return oldRot; }
            set { oldRot = value; }
        }

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
    /// Run though all child objects in the gameObjectsSpawn, and add it to the active list. It will then set timers for each object.
    /// </summary>

    // Use this for initialization
    void Start()
    {

        if (gameObjectsSpawn.transform.childCount != 0)
        {
            for (int i = 0; i < gameObjectsSpawn.transform.childCount; i++)
            {
                active.Add(GameObjectStart(gameObjectsSpawn.transform.GetChild(i).gameObject));
            }
        }
        else
        {
            Debug.Log("There is no child objects");
        }

        SetSpawnTimers();

    }


    // Update is called once per frame
    void Update()
    {
        //RotateGameObject();
        //SpawnTheObjects();
        //ActiveListUpdate();
        UpdateList();
        Rotate();
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
        if (playerOnConveyor == true)
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
        else if (collision.transform.tag == "PickUp")
        {
            collision.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
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
    /// When there is something in the active list, then the objects will run one of the ObjectMode.
    /// </summary>
    private void UpdateList()
    {
        if (active.Count != 0)
        {

            for (int i = 0; i < active.Count; i++)
            {
                switch (active[i].ObjMode)
                {
                    case ObjectMode.SPAWNING:
                        active[i] = ObjectSpawning(active[i]);
                        break;
                    case ObjectMode.MOVING:
                        active[i] = ObjectMoving(active[i]);
                        break;
                    case ObjectMode.ROTATING:
                        active[i] = ObjectRotating(active[i]);
                        break;
                    case ObjectMode.RESETTING:
                        active[i] = ObjectReset(active[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Setting time for objects in the active list. Its local z will be its time to spawn.
    /// </summary>
    private void SetSpawnTimers()
    {
        for (int i = 0; i < active.Count; i++)
        {
            gameObjectData temp = active[i];
            temp.TimeToSpawn = active[i].OldPos.z;
            active[i] = temp;
        }  
    }


    /// <summary>
    /// This return method is to setup gameobject so its ready to move, if TimeToSpawn is under 0. Setup is getting local pos and rot for future, lane, height, GameObjectReset, distance.
    /// </summary>
    /// <param name="god"></param>
    /// <returns></returns>
    private gameObjectData ObjectSpawning(gameObjectData god)
    {
        god.TimeToSpawn -= Time.deltaTime;

        if(god.TimeToSpawn <= 0)
        {
            god.Lane = god.Obj.transform.localPosition.x;
            god.Height = god.Obj.transform.localPosition.y;
            god = GameObjectReset(god);

            distanceStartToRotationStart = Vector3.Distance(god.Obj.transform.localPosition, rotationStartGameObject.transform.localPosition);

            god.Distance = distanceStartToRotationStart + 0.01f;
            god.ObjMode = ObjectMode.MOVING;
        }

        return god;
    }

    /// <summary>
    /// Moves god objects if not the distance is under god.Distance. Then it will change parent to rotation gameobject and god.ObjMode to ROTATING.
    /// </summary>
    /// <param name="god"></param>
    /// <returns></returns>
    private gameObjectData ObjectMoving(gameObjectData god)
    {
        if (Vector3.Distance(god.Obj.transform.localPosition, rotationGameObject.transform.localPosition) <= god.Distance)
        {
            god.Obj.transform.SetParent(rotationGameObject.transform);
            god.ObjMode = ObjectMode.ROTATING;
        }
        else
        {
            god.Obj.transform.Translate(transform.forward * Time.deltaTime * speed);
        }

        return god;
    }
    /// <summary>
    /// This method get the distance between the god and endGameObject. If under endDistance then it will go in RESETTING ObjMode. Return is replace the old god
    /// </summary>
    /// <param name="god"></param>
    /// <returns></returns>
    private gameObjectData ObjectRotating(gameObjectData god)
    {
        if (Vector3.Distance(god.Obj.transform.position, endGameObject.transform.position) <= endDistance)
        {
            god.ObjMode = ObjectMode.RESETTING;
        }
        return god;
    }
    /// <summary>
    /// This method resets the gameobject, so its starts at the beginning again. Just like a loop. return is for replacing "old" gameObjectData.
    /// </summary>
    /// <param name="god"></param>
    /// <returns></returns>
    private gameObjectData ObjectReset(gameObjectData god)
    {
        god = GameObjectReset(god);
        god.Obj.transform.localRotation = god.OldRot;
        god.ObjMode = ObjectMode.MOVING;
        return god;
    }
    /// <summary>
    /// Rotating the rotating gameobject
    /// </summary>
    private void Rotate()
    {
        if (rotationDir == true)
        {
            rotationGameObject.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
        else
        {
            rotationGameObject.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
        }

    }
    /// <summary>
    /// Getting locals for the future
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private gameObjectData GameObjectStart(GameObject obj)
    {
        gameObjectData god = new gameObjectData();

        god.Obj = obj;
        god.OldPos = obj.transform.localPosition;
        god.OldRot = obj.transform.localRotation;

        return god;
    }

    /// <summary>
    /// The god will get a new parent and new position.
    /// </summary>
    /// <param name="god"></param>
    /// <returns></returns>
    private gameObjectData GameObjectReset(gameObjectData god)
    {
        god.Obj.transform.SetParent(transform);
        god.Obj.transform.localPosition = new Vector3(startGameObject.transform.localPosition.x + god.Lane,
                                                      startGameObject.transform.localPosition.y + god.Height,
                                                      startGameObject.transform.localPosition.z);
        return god;
    }

    //OLD CODE

    /// <summary>
    /// If the counter is under 0, then it will move a gameobject from notActive to the active list.
    /// It will take the gameobjects localposition, set parent to the conveyour belt transform, set the distance between start and rotationstart 
    /// and then it will make a new gameObjectData struct and take it to the active list and remove it from notActive.
    /// </summary>
    //private void SpawnTheObjects()
    //{

    //    if (timerReady == false)
    //    {
    //        counter = newTimeZ - lastTimeZ;
    //        lastTimeZ = newTimeZ;
    //        timerReady = true;
    //    }

    //    counter -= Time.deltaTime;

    //    if (notActive.Count == 0 && counter <= 0 && lastObjectSpawned == false)
    //    {
    //        active.Add(lastObj);
    //        lastTimeZ = 0;
    //        lastObjectSpawned = true;
    //    }

    //    if (notActive.Count != 0 && counter <= 0 && timerReady == true)
    //    {

    //        //newTimeZ = notActive[0].transform.localPosition.z;

    //        newTimeZ = notActive[0].OldPos.z;

    //        if (lastObj.Obj != null)
    //        {
    //            active.Add(lastObj);
    //        }
    //        lastObj = GameObjectData(notActive[0]);
    //        notActive.RemoveAt(0);
    //        timerReady = false;
    //    }
    //}

    /// <summary>
    /// When there is something in the active list, then it will move the gameObjects untill it reach its distination.
    /// When it reaches its distination, the gameObject will get a new parent. 
    /// The parent is the rotation gameObject. 
    /// The gameObject will then get added to the rotation list and removed from active list.
    /// </summary>

    //private void ActiveListUpdate()
    //{

    //    if (active.Count != 0)
    //    {
    //        gameObjectData objRemove = active[0];
    //        bool removeTime = false;

    //        for (int i = 0; i < active.Count; i++)
    //        {
    //            if (Vector3.Distance(active[i].Obj.transform.localPosition, rotationGameObject.transform.localPosition) <= active[i].Distance)
    //            {
    //                active[i].Obj.transform.SetParent(rotationGameObject.transform);
    //                rotationList.Add(active[i]);
    //                objRemove = active[i];
    //                removeTime = true;

    //            }
    //            else
    //            {
    //                active[i].Obj.transform.Translate(transform.forward * Time.deltaTime * speed);
    //            }
    //        }
    //        if (removeTime == true)
    //        {
    //            removeTime = false;
    //            active.Remove(objRemove);
    //        }
    //    }
    //}


    /// <summary>
    /// Rotates the rotationGameObject.
    /// Get all the distance information for the gameObjects in the rotationList.
    /// If the distance is under 0.7 then it will get removed from the rotationList and runs the LoopGameObjects method.
    /// For every gameobject in the rotatonList will change parent 2 times for each loop of the method. 
    /// </summary>
    //private void RotateGameObject()
    //{
    //    if (rotationDir == true)
    //    {
    //        rotationGameObject.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    //    }
    //    else
    //    {
    //        rotationGameObject.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
    //    }


    //    if (rotationList.Count != 0)
    //    {

    //        gameObjectData objectData = rotationList[0];
    //        bool remove = false;

    //        for (int i = 0; i < rotationList.Count; i++)
    //        {

    //            if (Vector3.Distance(rotationList[i].Obj.transform.position, endGameObject.transform.position) <= endDistance)
    //            {
    //                objectData = rotationList[i];
    //                remove = true;
    //            }

    //            rotationList[i].Obj.transform.SetParent(rotationGameObject.transform);
    //        }

    //        if (remove == true)
    //        {
    //            objectData.Obj.transform.parent = null;
    //            rotationList.Remove(objectData);
    //            LoopGameObjects(objectData);
    //            remove = false;
    //        }
    //    }
    //}

    /// <summary>
    /// The obj will get a new parent, "gameObjectsSpawn" transform. Then it will change the localPosition and localRotation to the original position and rotation.
    /// It will then get added to the notActive list. So the gameObject can get spawned again.
    /// gameObjectCount is changed to how many gameObjects there will be on the conveyour belt. 
    /// gameObjectCounter is to get the obj real saved localPosition and localRotation. 
    /// </summary>
    /// <param name="obj"></param>

    //private void LoopGameObjects(gameObjectData god)
    //{
    //    if (gameObjectCounter >= gameObjectCount)
    //    {
    //        gameObjectCounter = 0;
    //    }

    //    god = GameObjectReset(god);
    //    god.Obj.transform.localRotation = god.OldRot;

    //    gameObjectCounter++;

    //    active.Add(god);
    //}


    /// <summary>
    /// This method gets the GameObject obj data. Where should the object spawn,, skal laves om til bedre form for kode.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>

    //private gameObjectData GameObjectData(gameObjectData god)
    //{
    //    god.Lane = god.Obj.transform.localPosition.x;
    //    god.Height = god.Obj.transform.localPosition.y;
    //    god = GameObjectReset(god);

    //    distanceStartToRotationStart = Vector3.Distance(god.Obj.transform.localPosition, rotationStartGameObject.transform.localPosition);

    //    god.Distance = distanceStartToRotationStart + 0.01f;

    //    return god;
    //}

    /// <summary>
    /// Maybe future plans. This is called whenever the player dies. It is a global method.
    /// </summary>
    public void OnPlayerRespawn()
    {
        Debug.Log("Event Called");
    }

}
