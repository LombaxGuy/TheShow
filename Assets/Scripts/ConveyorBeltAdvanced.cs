using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBeltAdvanced : MonoBehaviour {


    [SerializeField]
    private GameObject topTriggerGameObject;
    [SerializeField]
    private GameObject rotationStartGameObject;
    [SerializeField]
    private GameObject startGameObject;
    [SerializeField]
    private GameObject endGameObject;
    [SerializeField]
    private GameObject gameObjectsSpawn;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float rotationSpeed = 150;
    [SerializeField]
    private float distanceStartToRotationStart;

    //"bane" er lavet uden for mappet så playeren ikke kan se det. De vil komme ind på båndet efter tid. Info om hvor den skal spawn er på objecter.
    [Header("Add GameObjects to the notActive")]
    [SerializeField]
    private List<GameObject> notActive;
    [SerializeField]
    private List<GameObject> active;


    private float timer = 2;
    private float counter = 0;


    // Use this for initialization
    void Start ()
    {

        distanceStartToRotationStart = (startGameObject.transform.position - rotationStartGameObject.transform.position).magnitude;
        Debug.Log("Distance: " + distanceStartToRotationStart);
        //Instantiate(gameObjectsSpawn, startGameObject.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotateGameObject();
        notActiveListSpawn();
        activeListUpdate();
	}

    private void activeListUpdate()
    {

        if(active.Count == 0)
        {
            for (int i = 0; i < active.Count; i++)
            {
                if ((active[i].transform.position - rotationStartGameObject.transform.position).magnitude <= 1)
                {
                    active[i].transform.SetParent(rotationStartGameObject.transform);
                }
                else
                {
                    active[i].transform.Translate(transform.forward * Time.deltaTime * speed);
                }
            }
        }


    }

    private void notActiveListSpawn()
    {
        if(notActive.Count != 0)
        {
            if (counter <= 0)
            {
                active.Add(notActive[Random.Range(0, notActive.Count)]);
                //Instantiate(notActive[Random.Range(0, notActive.Count)], startGameObject.transform.position, Quaternion.identity);
                counter = timer;
            }
            counter -= Time.deltaTime;
        }

       
    }

    private void RotateGameObject()
    {
        rotationStartGameObject.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    }

}
