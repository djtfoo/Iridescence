using UnityEngine;
using System.Collections;

public class HUDElementBar : MonoBehaviour {
    
    public Transform element1;  // bar for element 1
    public Transform element2;  // bar for element 2

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Element1Changed(float newVal, float maxVal)
    {
        element1.localScale = new Vector3(-newVal / maxVal, 1f, 1f);
    }

    public void Element2Changed(float newVal, float maxVal)
    {
        element2.localScale = new Vector3(newVal / maxVal, 1f, 1f);
    }

}
