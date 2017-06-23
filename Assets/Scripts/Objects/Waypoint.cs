using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    [Tooltip("Name of the location this waypoint is located at")]
    public string locationName;    // show name of linked location when in traversing selection menu
    [Tooltip("Name of the prefab of the location this waypoint is in")]
    public string levelPrefabFilename; // name of the prefab of the level this waypoint connects to

    private bool hasHealedBefore;   // whether player has come to this checkpoint yet

	// Use this for initialization
	void Start () {

        hasHealedBefore = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HealPlayer()
    {
        PlayerData.RestoreHP();
        PlayerData.RestoreMP();

        hasHealedBefore = true;
    }

}
