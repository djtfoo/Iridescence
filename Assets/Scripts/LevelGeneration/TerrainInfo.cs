using UnityEngine;
using System.Collections;

/// <summary>
///  Component only for containing data regarding this level
/// </summary>
public class TerrainInfo : MonoBehaviour {

    // public so that it gets saved in the Prefab
    public string locationName;     // name of this location
    public string background;   // name of the type of background render
    public Vector3 playerPos;   // player's starting position - if not from portal / waypoint

    public void SetPlayerPos()
    {
#if !LEVELEDITOR
        // set player position
        PlayerAction.instance.transform.position = playerPos;
        PlayerAction.instance.GetComponent<DepthSort>().DoDepthSort();
#endif
    }

    public void SetLocationName(string newLocationName)
    {
        locationName = newLocationName;
    }
    public void SetBackground(string newBackground)
    {
        background = newBackground;
    }

}
