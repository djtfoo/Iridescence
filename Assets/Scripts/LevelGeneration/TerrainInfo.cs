using UnityEngine;
using System.Collections;

/// <summary>
///  Component only for containing data regarding this level
/// </summary>
public class TerrainInfo : MonoBehaviour {

    public string locationName;     // name of this location
    public string background;   // name of the type of background render

    public void SetLocationName(string newLocationName)
    {
        locationName = newLocationName;
    }
    public void SetBackground(string newBackground)
    {
        background = newBackground;
    }

}
