using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CellBehaviour))]
public class CellBehaviourEditor : Editor {


    public override void OnInspectorGUI()
    {
        CellBehaviour myScript = (CellBehaviour)target;
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
