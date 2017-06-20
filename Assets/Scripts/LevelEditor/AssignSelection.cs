using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssignSelection : MonoBehaviour {

    // set this asset from a list to be the ghost object
    public void SetSelection()
    {
        LevelEditorManager.leManager.SetGhostObject(this.GetComponent<Image>().sprite, this.GetComponent<AssetInfo>().GetGOType(), this.GetComponent<AssetInfo>().GetID());
        GameObject.Find("DropdownList").GetComponent<CloseDropdown>().Close();
    }

    // set waypoint to be the ghost object
    public void SetWaypointSelection()
    {
        LevelEditorManager.leManager.SetGhostObject(this.GetComponent<Image>().sprite, GO_TYPE.GO_WAYPOINT, -1);     // non-tiles are -1
    }

}
