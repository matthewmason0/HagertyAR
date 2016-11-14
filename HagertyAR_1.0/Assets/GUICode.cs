using UnityEngine;
using System.Collections;

public class GUICode : MonoBehaviour {
    private bool visible = false;
    private bool mainMenu = true;
    private bool searchRooms = false;
    private bool searchLockers = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI () {
        int guiW = Screen.width - 10;
        int guiH = Screen.height - 10;
        if (visible) //When the Menu is up
        {
            GUI.Box(new Rect(5, 5, guiW, guiH), "Menu");
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height - 30, 80, 20), "Dismiss"))
                visible = false;
            if(mainMenu && GUI.Button(new Rect(Screen.width - 55, 5, 50, 20),"Search for a Room")) //fix size
                searchRooms = true;
        }
        else //Menu is hidden
        {
            if (GUI.Button(new Rect(Screen.width-55, 5, 50, 20), "Menu"))
                visible = true;
                mainMenu = true;
        }
    }
}
