using UnityEngine;

public class Room {
    public string Number;
    public int Floor;
    public GameObject Obj;
    public Vector3 Pos;

    public Room(string Number, GameObject Obj, Vector3 Pos)
    {
        this.Number = Number;
        //check floor
        string[] temp = Number.Split('-');
        if (!temp[0].Contains("Portable") && temp[1].Substring(0, 1).Equals("1")) //first floor room
            this.Floor = 1;
        else //second floor room
            this.Floor = 2;
        this.Obj = Obj;
        this.Pos = Pos;
    }
}
