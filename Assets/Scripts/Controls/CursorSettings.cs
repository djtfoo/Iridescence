using UnityEngine;
using System.Collections;

public class CursorSettings : MonoBehaviour {

    public CursorLockMode mode;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCursorState()
    {
        Cursor.lockState = mode;
    }


}
