using UnityEngine;

public class Lockers {
    public int Lowest;
    public int Highest;
    public int Floor;
    public GameObject Obj;
    public Vector3 Pos;

    public Lockers(int Lowest, int Highest, int Floor, GameObject Obj, Vector3 Pos)
    {
        this.Lowest = Lowest;
        this.Highest = Highest;
        this.Floor = Floor;
        this.Obj = Obj;
        this.Pos = Pos;
    }
}
