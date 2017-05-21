using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssignSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // set this asset to be the ghost object
    public void SetSelection()
    {
        LevelEditorManager.leManager.SetGhostObject(this.GetComponent<Image>().sprite, this.GetComponent<AssetInfo>().GetGOType(), this.GetComponent<AssetInfo>().GetID());
        GameObject.Find("DropdownList").GetComponent<CloseDropdown>().Close();
    }

}
