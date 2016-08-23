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


    private float timer = 1;
    private float counter = 0;

    //Addes senere. Hvis der skal være spawn i nærheden af det originale spawn position

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






    // Use this for initialization
    void Start ()
    {


        if (gameObjectsSpawn.transform.childCount != 0)
        {
            for (int i = 0; i < gameObjectsSpawn.transform.childCount; i++)
            {
                notActive.Add(gameObjectsSpawn.transform.GetChild(i).gameObject);
            }
            

        }
        else
        {
            Debug.Log("There is no children.");
        }

        
        //Instantiate(gameObjectsSpawn, startGameObject.transform.position, Quaternion.identity);
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
        GameObjectPositionReset.OnResetObjects += ResetMethod;
    }

    void OnDisable()
    {
        GameObjectPositionReset.OnResetObjects -= ResetMethod;
    }

   

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
                Destroy(gameObj);
                remove = false;
            }
        }
    }

    public void ResetMethod()
    {
        Debug.Log("Event Called");
    }

}
