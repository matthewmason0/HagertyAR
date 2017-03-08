using UnityEngine;
using System.Collections.Generic;

public class GUICode : MonoBehaviour
{
    public static bool intro = true;
    //menu bools
    private bool visible = false;
    private bool mainMenu = true;
    private bool searchRooms = false;
    private bool searchLockers = false;
    //room bools
    private string roomTextInput = string.Empty;
    private Room currentRoomIcon = null;
    private bool roomIconVisible = false;
    private bool roomHelpText = false;
    //lockers bools
    private string lockersTextInput = string.Empty;
    private Lockers currentLockersIcon;
    private bool lockersIconVisible = false;
    private bool lockersHelpText = false;
    //scaling
    private int native_width = 216;
    private int native_height = 384;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        //roomIcon animation
        if(roomIconVisible)
        {
            currentRoomIcon.Obj.transform.Rotate(0, 0, 60 * Time.deltaTime);
            float y = Mathf.Abs(0.05f * Mathf.Sin(1.2f * Time.time));
            currentRoomIcon.Obj.transform.localPosition = new Vector3(currentRoomIcon.Pos.x, currentRoomIcon.Pos.y + y, currentRoomIcon.Pos.z);
        }
        //lockersIcon animation
        if (lockersIconVisible)
        {
            currentLockersIcon.Obj.transform.Rotate(0, 0, 60 * Time.deltaTime);
            float y = Mathf.Abs(0.05f * Mathf.Sin(1.2f * Time.time));
            currentLockersIcon.Obj.transform.localPosition = new Vector3(currentLockersIcon.Pos.x, currentLockersIcon.Pos.y + y, currentLockersIcon.Pos.z);
        }
    }

    //reset moving icons and hidden second floor
    void stopSearching()
    {
        //reset the room icon if its moving
        if (currentRoomIcon != null)
        {
            currentRoomIcon.Obj.transform.localPosition = new Vector3(currentRoomIcon.Pos.x, currentRoomIcon.Pos.y, currentRoomIcon.Pos.z);
            currentRoomIcon.Obj.SetActive(false);
        }
        //reset the locker icon if its moving
        if(currentLockersIcon != null)
        {
            currentLockersIcon.Obj.transform.localPosition = new Vector3(currentLockersIcon.Pos.x, currentLockersIcon.Pos.y, currentLockersIcon.Pos.z);
            currentLockersIcon.Obj.SetActive(false);
        }
        currentRoomIcon = null;
        roomIconVisible = false;
        currentLockersIcon = null;
        lockersIconVisible = false;
        foreach (GameObject go in Dissappear_Reappear.secondFloor)
            go.SetActive(true);
    }

    void OnGUI()
    {
        //set up scaling
        float rx = Screen.width / (float)native_width;
        float ry = Screen.height / (float)native_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        //now create your GUI normally, as if you were in your native resolution
        //The GUI.matrix will scale everything automatically.
        int guiW = native_width - 10;
        int guiH = native_height - 10;
        if (intro)
        {
            GUI.Label(new Rect(5, 5, native_width - 10, native_height - 10), "Place the campus map in view of the camera to get started.");
        }
        else
        {
            
            if (visible) //When the Menu is up
            {
                //Background
                GUI.Box(new Rect(5, 5, guiW, guiH), "Menu");
                //Dismiss
                if (GUI.Button(new Rect(native_width / 2 - 40, native_height - 30, 80, 20), "Dismiss"))
                {
                    visible = false;
                    mainMenu = true;
                    searchRooms = false;
                    searchLockers = false;
                }
                //Search for a Room
                if (mainMenu && GUI.Button(new Rect(native_width / 2 - 60, native_height / 2 - 20, 120, 20), "Search for a Room"))
                {
                    searchRooms = true;
                    mainMenu = false;
                }
                if (searchRooms) //Room Search Screen
                {
                    //Textbox
                    roomTextInput = GUI.TextField(new Rect(native_width / 2 - 60, native_height / 2 - 20, 120, 20), roomTextInput);
                    if (roomHelpText)
                    {
                        GUI.Label(new Rect(native_width / 2 - 60, native_height / 2 - 70, 120, 65), "Be sure to use the format bldg-room, i.e. 7-109.");
                    }
                    //Back
                    if (GUI.Button(new Rect(native_width / 2 - 25, native_height / 2 + 5, 50, 20), "Back"))
                    {
                        searchRooms = false;
                        mainMenu = true;
                    }
                    //Go
                    if (GUI.Button(new Rect(native_width / 2 + 65, native_height / 2 - 20, 30, 20), "Go"))
                    {
                        stopSearching();
                        Room room = null;
                        bool found = false;
                        //search for room
                        foreach (Room r in Dissappear_Reappear.roomList)
                            if (r.Number.Contains(roomTextInput))
                            {
                                found = true;
                                room = r;
                            }
                        if (found)
                        {
                            mainMenu = true;
                            searchRooms = false;
                            visible = false;
                            currentRoomIcon = room;
                            currentRoomIcon.Obj.SetActive(true);
                            roomIconVisible = true;
                            roomHelpText = false;
                            //check floor
                            if (currentRoomIcon.Floor == 1) //first floor room
                                foreach (GameObject go in Dissappear_Reappear.secondFloor)
                                    go.SetActive(false);
                        }
                        else
                        {
                            roomHelpText = true;
                        }
                        roomTextInput = string.Empty;
                    }
                }
                //Search for Lockers
                if (mainMenu && GUI.Button(new Rect(native_width / 2 - 60, native_height / 2 + 5, 120, 20), "Search for Lockers"))
                {
                    searchLockers = true;
                    mainMenu = false;
                }
                if (searchLockers) //Locker Search Screen
                {
                    //Textbox
                    lockersTextInput = GUI.TextField(new Rect(native_width / 2 - 60, native_height / 2 - 20, 120, 20), lockersTextInput);
                    if (lockersHelpText)
                    {
                        GUI.Label(new Rect(native_width / 2 - 60, native_height / 2 - 70, 120, 65), "Be sure to type a valid locker number, i.e. 718");
                    }
                    //Back
                    if (GUI.Button(new Rect(native_width / 2 - 25, native_height / 2 + 5, 50, 20), "Back"))
                    {
                        searchLockers = false;
                        mainMenu = true;
                    }
                    //Go
                    if (GUI.Button(new Rect(native_width / 2 + 65, native_height / 2 - 20, 30, 20), "Go"))
                    {
                        stopSearching();
                        Lockers lockers = null;
                        bool found = false;
                        //search for locker
                        foreach (Lockers l in Dissappear_Reappear.lockersList)
                        {
                            int temp;
                            if (int.TryParse(lockersTextInput, out temp) && l.Lowest <= temp && l.Highest >= temp)
                            {
                                found = true;
                                lockers = l;
                            }
                        }
                        if (found)
                        {
                            mainMenu = true;
                            searchLockers = false;
                            visible = false;
                            currentLockersIcon = lockers;
                            currentLockersIcon.Obj.SetActive(true);
                            lockersIconVisible = true;
                            lockersHelpText = false;
                            //check floor
                            if (currentLockersIcon.Floor == 1) //first floor lockers
                                foreach (GameObject go in Dissappear_Reappear.secondFloor)
                                    go.SetActive(false);

                        }
                        else
                        {
                            lockersHelpText = true;
                        }
                        lockersTextInput = string.Empty;
                    }
                }
            }
            else //Menu is hidden
            {
                //Menu
                if (GUI.Button(new Rect(native_width - 55, 5, 50, 20), "Menu"))
                {
                    visible = true;
                    mainMenu = true;
                }
                if (roomIconVisible || lockersIconVisible)
                {
                    //Stop Searching
                    if (GUI.Button(new Rect(native_width - 115, native_height - 25, 110, 20), "Stop Searching"))
                    {
                        stopSearching();
                    }
                }
            }
        }
    }
}
