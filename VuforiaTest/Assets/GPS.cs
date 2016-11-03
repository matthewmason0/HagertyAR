using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GPS : MonoBehaviour {
    Vector2 RInit;
    Vector2 RCurrentPos;
    Vector2 FInit;
    Vector2 FCurrentPosition;

    public float scale;
    public bool FakingLocation;

    public void Start()
    {
        Input.location.Start(0.5f);
        Input.compass.enabled = true;

        FInit = Vector2.zero;
    }

    IEnumerator UpdatePosition()
    {
        if (FakingLocation == false)
        {
            if (Input.location.isEnabledByUser == false)
            {
                Debug.Log("Failed because User did not enable location");
            }

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }
            if (maxWait < 1)
            {
                Debug.Log("Initializing failed, try again");
                yield return null;
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("LocationInfo Servuse Status Failed!");
                yield return null;
            }
            else
            {
                SetLocation(Input.location.lastData.latitude, Input.location.lastData.longitude);
            }
        }
        else
        {
            SetLocation(100 + Time.time, 100 + Time.time);
        }
    }

    void SetLocation(float latitude, float longitude)
    {
        RCurrentPos = new Vector2(latitude, longitude);
        Vector2 delta = new Vector2(RCurrentPos.x - RInit.x, RCurrentPos.y - RInit.y);
        FCurrentPosition = delta * scale;
        this.transform.position = new Vector3(FCurrentPosition.x, 0, FCurrentPosition.y);
		GameObject.Find ("posText").GetComponent<Text> ().text = this.transform.position.x + " : " + this.transform.position.y + " : " + this.transform.position.z;
    }
}
