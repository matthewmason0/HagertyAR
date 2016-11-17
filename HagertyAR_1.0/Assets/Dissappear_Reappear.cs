using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class Dissappear_Reappear : MonoBehaviour {
    private struct Room
    {
        public string Number;
        public GameObject Obj;

        public Room(string n, GameObject o)
        {
            Number = n;
            Obj = o;
        }
    }

    private List<Room> roomList = new List<Room>();

    Color colorStart;
    Color colorEnd;
    float duration = 1f;
    bool visible = true;
	// Use this for initialization
	void Start () {
        //hide unnamed icons
        GameObject[] hiddenIcons = GameObject.FindGameObjectsWithTag("Hide");
        Debug.Log(hiddenIcons.Length+" icons to hide");
        foreach (GameObject go in hiddenIcons)
            go.SetActive(false);

        //create list of named icons
        string[] numbers = new string[] {"Room 6-123", "Room 6-125", "Room 6-111", "Room 6-108", "Room 6-117B", "Room 6-117", "Room 6-124", "Room 6-103", "Room 6-116", "Room 6-114", "Room 6-100C", "Room 6-100", "Room 6-100A", "Room 6-100B", "Room 6-112", "Room 6-109", "Room 6-106A", "Room 6-106", "Room 6-102", "Room 6-101", "Room 6-113", "Room 7-102", "Room 7-107", "Room 7-111", "Room 7-112", "Room 7-110", "Room 7-103", "Room 7-100", "Room 7-114", "Room 7-117", "Room 7-108", "Room 7-124", "Room 7-124C", "Room 7-109", "Mechanical: 7-121", "Mechanical: 7-106", "Boys Restroom 7-118", "Boys Restroom 7-104", "Girls Restroom 7-119", "Girls Restroom 7-105", "Room 7-125", "Room 7-126", "Room 7-122A", "Room 7-122", "Room 7-120", "Room 7-116", "Room 7-115", "Room 7-100C", "Room 7-101", "Room 7-100B", "Room 7-100A"};
        foreach (string number in numbers)
	        roomList.Add(new Room(number, GameObject.Find(number)));

        //hide named icons
        foreach (Room room in roomList)
            room.Obj.SetActive(false);
}

	
    // Update is called once per frame
	void Update () {
        if(Time.time > 10f && visible)
        {
            //GameObject.Find("ImageTarget/Bldg3SecondFloor").SetActive(false);
            //Debug.Log("Bldg3SecondFloor hidden");
            //visible = false;
        }
        
    }
}
