using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GPS : MonoBehaviour {
    Vector2 RealInit;
    Vector2 RealCurrentPos;
    Vector2 VirtualInit;
    Vector2 VirtualCurrentPos;

    public float scale;
    public bool FakingLocation;

	// Use this for initialization
	void Start () {
        Input.location.Start(0.5f);
        Input.compass.enabled = true;

        VirtualInit = Vector2.zero;
	}
	
	// Update is called once per frame
	IEnumerator UpdatePos () {
        if (Input.location.isEnabledByUser == false)
            Debug.Log("Failed because User did not enable location");

        int maxWait = 20;
        while (Input.location.status==LocationServiceStatus.Initializing && maxWait>0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if(maxWait<1)
        {
            Debug.Log("Initializing failed, try again");
            yield return null;
        }
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log
        }
	}
}
