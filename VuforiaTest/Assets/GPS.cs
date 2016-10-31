﻿using UnityEngine;
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

	void Start () {
        Input.location.Start(0.5f);
        Input.compass.enabled = true;

        FInit = Vector2.zero;
	}
	
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
            
        }
	}
}
