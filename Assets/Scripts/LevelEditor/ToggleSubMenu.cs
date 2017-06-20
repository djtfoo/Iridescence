using UnityEngine;
using System.Collections;

public class ToggleSubMenu : MonoBehaviour {

    public GameObject subMenu;

	// Use this for initialization
	void Start () {
        if (subMenu == null)
            subMenu = this.gameObject;

        subMenu.SetActive(false);
    }
	
	public void Toggle()
    {
        subMenu.SetActive(!subMenu.activeSelf);
    }

}
