using UnityEngine;
using System.Collections;

public class GUICode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI () {
        GUI.Box(new Rect(10,10,120,90),"GUI.Box");
        if (GUI.Button(new Rect(20, 40, 100, 20), "Open Camera")) {
            
        }
    }
}
