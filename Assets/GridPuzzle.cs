using UnityEngine;
using System.Collections;

public class GridPuzzle : MonoBehaviour {


    private GameObject[] gameObjects;

    

    // Use this for initialization
    void Start()
    {
        if (transform.childCount != 0)
        {
            gameObjects = new GameObject[gameObject.transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                gameObjects[i] = transform.GetChild(i).gameObject;
                // add sript til alle gameobjects, med at de sender information når spilleren collidere??
                //gameObjects[i].AddComponent<CellBehaviour>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerOnObject(GameObject obj)
    {
        if(gameObject.tag == "GridOn")
        {
            //Lys op?
        }else
        {
            //kill player
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Show the path?=
        }
    }

    private void ShowPath()
    {
        //Do stuff with path
        //Lys alle GridOn tag op med hvid
        //Lys alle GridOff tag op med rød
        //4 sec lys op? // 

    }
}
