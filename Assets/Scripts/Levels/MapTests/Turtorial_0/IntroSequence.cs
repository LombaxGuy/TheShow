using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {
    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private GameObject trigger;
    [SerializeField]
    private GameObject door;

    public static bool lightEntered = false;
    [SerializeField]
    public static float timer;
    [SerializeField]
    public static float timeInSeconds;
    private float offtime;
    [SerializeField]
    private int stage;
    private int stageSaved;



    // Use this for initialization
    void Start () {
        manager = GameObject.Find("WorldManager");

        manager.GetComponent<SubtitleControl>().StartSub("sub1", 3);
        stage = 1;

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;



        if (timer > 4 && stage == 1)
        {
            manager.GetComponent<SubtitleControl>().StartSub("sub2",3);

            stage = stage + 1;
        }

        if (timer > 8 && stage == 2)
        {
            manager.GetComponent<SubtitleControl>().StartSub("sub3", 3);
            manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use Wasd to move around", 3);
            stage = stage + 1;
            stageSaved = stage;
            trigger.SetActive(true);
        }

        if(timer > 26 && stage == 3 && !lightEntered)
        {
            manager.GetComponent<SubtitleControl>().StartSub("Test1", 3);

            
            stage = stage + 1;
            stageSaved = stage;

        }

        if (timer > 40 && stage == 4 && !lightEntered)
        {
            manager.GetComponent<SubtitleControl>().StartSub("Test2", 3);
            
            
            stage = stage + 1;
            stageSaved = stage;
        }

        if (lightEntered)
        {
            if (timer > timeInSeconds && stage == stageSaved)
            {
                offtime = timeInSeconds;
                manager.GetComponent<SubtitleControl>().StartSub("sub4", 3);
                manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use space to jump", 3);
                stage = stage + 1;
            }



            if (timer > 4 + timeInSeconds && stage == stageSaved + 1)
            {
                manager.GetComponent<SubtitleControl>().StartSub("sub5", 3);
                manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use control to crouch", 3);
                stage = stage + 1;
            }

            if (timer > 8 + timeInSeconds && stage == stageSaved + 2)
            {
                manager.GetComponent<SubtitleControl>().StartSub("sub6", 3);
                //manager.GetComponent<Tooltip>().DisplayTooltipForSeconds("Use space to jump", 3);                
                stage = stage + 1;
                door.SetActive(false);
            }
        }
    }
}
