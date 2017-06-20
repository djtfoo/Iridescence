using UnityEngine;
using System.Collections;

// toggle whether an object to be active or inactive
public class ToggleActive : MonoBehaviour {

    public GameObject objectToToggle;

	// Use this for initialization
	void Start () {
        if (objectToToggle == null)
            objectToToggle = this.gameObject;

        objectToToggle.SetActive(false);
    }
	
	public void Toggle()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }

    public void SetActive(bool active)
    {
        objectToToggle.SetActive(active);
    }

}
