using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

      
	}
	
	// Update is called once per frame
	void Update () {           
        if(Input.GetKey(KeyCode.E) && this.gameObject.activeInHierarchy == false)
        {
            MenuToggle(true);
        }
        else if (Input.GetKey(KeyCode.E) && this.gameObject.activeInHierarchy == true)
        {
            MenuToggle(false);
        }
	}

    void MenuToggle(bool state)
    {
        
    }

    void Settings()
    {
        
    }
}
