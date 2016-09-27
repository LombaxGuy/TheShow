using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CellBehaviour))]
public class CellBehaviourEditor : Editor {
    
    private GameObject[] tempDoors;

    private bool[] AllowedToOpen;

    public override void OnInspectorGUI()
    {

        CellBehaviour cellBehaviour = (CellBehaviour)target;
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Insert doors in the room:", EditorStyles.boldLabel);

        if(cellBehaviour.Doors.Length == 4)
        {
            for (int i = 0; i < cellBehaviour.Doors.Length; i++)
            {
                cellBehaviour.Doors[i] = (GameObject)EditorGUILayout.ObjectField("Door " + i + ":", cellBehaviour.Doors[i], typeof(GameObject), true);
            }
        }else
        {
            if(tempDoors == null)
            {
                tempDoors = new GameObject[4];
            }
            for (int i = 0; i < tempDoors.Length; i++)
            {
                tempDoors[i] = (GameObject)EditorGUILayout.ObjectField("Door " + i + ":", tempDoors[i], typeof(GameObject), true);
            }
        }

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Check the one that is allowed to open:", EditorStyles.boldLabel);



        if(cellBehaviour.AllowedToOpen.Length == 4)
        {
            for (int i = 0; i < cellBehaviour.AllowedToOpen.Length; i++)
            {
                cellBehaviour.AllowedToOpen[i] = (bool)EditorGUILayout.Toggle("Door " + i + ":", cellBehaviour.AllowedToOpen[i]);
            }
        }
        else
        {
            if(AllowedToOpen == null)
            {
                AllowedToOpen = new bool[4];
            }

            for (int i = 0; i < AllowedToOpen.Length; i++)
            {
                AllowedToOpen[i] = (bool)EditorGUILayout.Toggle("Door " + i + ":", AllowedToOpen[i]);
            }
        }


        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Time stuck in the room:", EditorStyles.boldLabel);
        cellBehaviour.TimeTrapped = EditorGUILayout.FloatField(cellBehaviour.TimeTrapped);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Toggle death room:", EditorStyles.boldLabel);
        cellBehaviour.DeathCell = EditorGUILayout.Toggle(cellBehaviour.DeathCell);

        if (cellBehaviour.DeathCell == true)
        {
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Pick between death events for the room:", EditorStyles.boldLabel);
            
            cellBehaviour.DeathEvent = (CellBehaviour.DeathWay)EditorGUILayout.EnumPopup(cellBehaviour.DeathEvent);
            EditorGUILayout.LabelField("");

            switch (cellBehaviour.DeathEvent)
            {
                case CellBehaviour.DeathWay.GAS:
                    EditorGUILayout.LabelField("Insert GAS particle prefab:", EditorStyles.boldLabel);
                    cellBehaviour.GasObject = (GameObject)EditorGUILayout.ObjectField("Gas Particle Object:", cellBehaviour.GasObject, typeof(GameObject), false);
                    EditorGUILayout.LabelField("Time it takes to kill the player:", EditorStyles.boldLabel);
                    cellBehaviour.TimeBeforeDeath = EditorGUILayout.FloatField(cellBehaviour.TimeBeforeDeath);
                    break;
                case CellBehaviour.DeathWay.FIRE:
                    EditorGUILayout.LabelField("---COMING SOON---", EditorStyles.boldLabel);
                    break;
                case CellBehaviour.DeathWay.ROOF:
                    EditorGUILayout.LabelField("---COMING SOON---", EditorStyles.boldLabel);
                    break;
                case CellBehaviour.DeathWay.SPIKES:
                    EditorGUILayout.LabelField("---COMING SOON---", EditorStyles.boldLabel);
                    break;
                case CellBehaviour.DeathWay.WATER:
                    EditorGUILayout.LabelField("---COMING SOON---", EditorStyles.boldLabel);
                    break;
                case CellBehaviour.DeathWay.STUCK:
                    EditorGUILayout.LabelField("---COMING SOON---", EditorStyles.boldLabel);
                    break;
                default:
                    break;
            }
        }else
        {
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Toggle starting room:", EditorStyles.boldLabel);
            cellBehaviour.StartingRoom = EditorGUILayout.Toggle(cellBehaviour.StartingRoom);
            if(cellBehaviour.StartingRoom == true)
            {
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Pick starting door:", EditorStyles.boldLabel);
                cellBehaviour.DoorStartOpen = EditorGUILayout.IntField(cellBehaviour.DoorStartOpen);
            }

        }


        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Insert soundObject:", EditorStyles.boldLabel);
        cellBehaviour.SoundObject = (GameObject)EditorGUILayout.ObjectField("SoundObject:", cellBehaviour.SoundObject, typeof(GameObject), true);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Insert sound:", EditorStyles.boldLabel);
        cellBehaviour.Clip = (AudioClip)EditorGUILayout.ObjectField("Sound:", cellBehaviour.Clip, typeof(AudioClip), false);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Insert confetti particle:", EditorStyles.boldLabel);
        cellBehaviour.ParticleObject = (GameObject)EditorGUILayout.ObjectField("Confetti:", cellBehaviour.ParticleObject, typeof(GameObject), true);



        if (GUI.changed)
        {
            if(cellBehaviour.Doors.Length != 4)
            {
                cellBehaviour.Doors = tempDoors;
            }
            EditorUtility.SetDirty(target);
        }


    }
    
 

// Use this for initialization
void Start ()
    {
        tempDoors = new GameObject[4];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
