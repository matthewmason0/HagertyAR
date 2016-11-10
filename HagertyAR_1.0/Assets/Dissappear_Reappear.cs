using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dissappear_Reappear : MonoBehaviour {
    Color colorStart;
    Color colorEnd;
    float duration = 1f;
    bool fading = false;
	// Use this for initialization
	void Start () {
        Debug.Log("start");
        colorStart = GetComponent<Renderer>().material.color;
        colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0f);
    }

	void FadeOut ()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        fading = true;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].material.color = Color.Lerp(colorStart, colorEnd, t / duration);
        }
    }
    // Update is called once per frame
	void Update () {
        Debug.Log("waiting");
        if (Time.time > 10 && !fading)
        {
            FadeOut();
            Debug.Log("should be gone");
        }
    }
}
