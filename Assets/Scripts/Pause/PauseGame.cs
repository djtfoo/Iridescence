using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    static bool isPaused = false;
    PauseObject[] objects = new PauseObject[0];

    //public GameObject pauseCanvasPrefab;
    GameObject pauseCanvas;

    // Use this for initialization
    void Start () {
        //pauseCanvas = (GameObject)Instantiate(pauseCanvasPrefab, Vector3.zero, Quaternion.identity);
        pauseCanvas = this.transform.GetChild(0).gameObject;
        pauseCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPause(bool pause)
    {
        isPaused = pause;

        if (objects.Length == 0)
            objects = FindObjectsOfType(typeof(PauseObject)) as PauseObject[];

        foreach (PauseObject setPause in objects)
        {
            setPause.SetObjectToPause(pause);
        }

        pauseCanvas.SetActive(pause);
    }

}
