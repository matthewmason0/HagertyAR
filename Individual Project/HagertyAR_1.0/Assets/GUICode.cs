using UnityEngine;
using System.Collections.Generic;

public class GUICode : MonoBehaviour
{
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
	[SerializeField]
	private int fontSize = 20;
    private int native_width = 1080; //was 216
    private int native_height = 1920; //was 384

    public enum State
    {
        INTRO,
        CLOSED,
        OPEN,
        ROOM,
        LOCKER
    }
    public static State state = State.INTRO;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //roomIcon animation
        if (roomIconVisible)
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

    void OnGUI()
    {
        //set up scaling
		GUI.skin.button.fontSize = fontSize;
		GUI.skin.box.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
		GUI.skin.textField.fontSize = fontSize;
        float rx = Screen.width / (float)native_width;
        float ry = Screen.height / (float)native_height;
        float x_to_y = rx / ry;
        float y_to_x = ry / rx;
        float extrax = (Screen.width - (Screen.width / x_to_y)) / 2;
        float extray = (Screen.height - (Screen.height / y_to_x)) / 2;
        if (rx > ry) //screen is too short
        {
            rx = ry;
            extray = 0;
        }
        else         //screen is too tall
        {
            ry = rx;
            extrax = 0;
        }
        GUI.matrix = Matrix4x4.TRS(new Vector3(extrax, extray, 0), Quaternion.identity, new Vector3(rx, ry, 1));
        //gui variables
        int guiW = native_width - 50;
        int guiH = native_height - 50;

        switch (state)
        {

            case State.INTRO:
                GUI.Label(new Rect(25, 25, native_width - 50, native_height - 50), "Place the campus map in view of the camera to get started.");
                break;


            case State.CLOSED:
                //Menu
                if (GUI.Button(new Rect(native_width - 275, 25, 250, 100), "Menu"))
                    state = State.OPEN;
                if (roomIconVisible || lockersIconVisible)
                    //Stop Searching
                    if (GUI.Button(new Rect(native_width - 575, native_height - 125, 550, 100), "Stop Searching"))
                        stopSearching();
                break;


            case State.OPEN:
                //Background
                GUI.Box(new Rect(25, 25, guiW, guiH), "Menu");
                //Search for a Room
                if (GUI.Button(new Rect(native_width / 2 - 300, native_height / 2 - 100, 600, 100), "Search for a Room"))
                    state = State.ROOM;
                //Search for Lockers
                if (GUI.Button(new Rect(native_width / 2 - 300, native_height / 2 + 25, 600, 100), "Search for Lockers"))
                    state = State.LOCKER;
                break;


            case State.ROOM:
                //Textbox
                roomTextInput = GUI.TextField(new Rect(native_width / 2 - 300, native_height / 2 - 100, 600, 100), roomTextInput);
                //Display help text if query not found
                if (roomHelpText)
                    GUI.Label(new Rect(native_width / 2 - 300, native_height / 2 - 350, 600, 325), "Be sure to use the format bldg-room, i.e. 7-109.");
                //Back
                if (GUI.Button(new Rect(native_width / 2 - 125, native_height / 2 + 25, 250, 100), "Back"))
                    state = State.OPEN;
                //Go
                if (GUI.Button(new Rect(native_width / 2 + 325, native_height / 2 - 100, 150, 100), "Go"))
                {
                    stopSearching();
                    searchRooms();
                }
                break;


            case State.LOCKER:
                //Textbox
                lockersTextInput = GUI.TextField(new Rect(native_width / 2 - 300, native_height / 2 - 100, 600, 100), lockersTextInput);
                //Display help text if query not found
                if (lockersHelpText)
                    GUI.Label(new Rect(native_width / 2 - 300, native_height / 2 - 350, 600, 325), "Be sure to type a valid locker number, i.e. 718");
                //Back
                if (GUI.Button(new Rect(native_width / 2 - 125, native_height / 2 + 25, 250, 100), "Back"))
                    state = State.OPEN;
                //Go
                if (GUI.Button(new Rect(native_width / 2 + 325, native_height / 2 - 100, 150, 100), "Go"))
                {
                    stopSearching();
                    searchLockers();
                }
                break;

        }

        //Dismiss
        if (state != State.CLOSED)
            if (GUI.Button(new Rect(native_width / 2 - 200, native_height - 150, 400, 100), "Dismiss"))
                state = State.CLOSED;
    }

    void searchRooms()
    {
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
            state = State.CLOSED;
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
            roomHelpText = true;
        roomTextInput = string.Empty;
    }

    void searchLockers()
    {
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
            state = State.CLOSED;
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
            lockersHelpText = true;
        lockersTextInput = string.Empty;
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
        if (currentLockersIcon != null)
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
}