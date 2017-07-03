using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    //[Tooltip("Name of the location this waypoint is located at")]
    //public string locationName;    // show name of linked location when in traversing selection menu
    [Tooltip("Name of the prefab of the location this waypoint is in")]
    public string levelPrefabFilename; // name of the levelprefab this waypoint is in, thus the location for other waypoints to connect to

    // first time healing
    public GameObject firstTimeHealingIndicator; // indicator whether player has healed at this waypoint yet
    private bool firstTimeHealing;  // whether player has come healed at this checkpoint yet

	// Use this for initialization
	void Start () {

        firstTimeHealing = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    ///  Set checkpoint & heal player if possible
    /// </summary>
    public void InteractWithWaypoint()
    {
        SetLastCheckpoint();
        if (firstTimeHealing)
        {
            HealPlayer();   // this function will check whether player can be healed
        }
    }

    /// <summary>
    ///  When player approaches this waypoint, set this as player's last visited checkpoint
    /// </summary>
    private void SetLastCheckpoint()
    {
        PlayerAction.instance.GetPlayerData().lastCheckpoint = levelPrefabFilename;
    }

    /// <summary>
    ///  When player approaches this waypoint, heal the player if not yet done so
    /// </summary>
    private void HealPlayer()
    {
        if (!PlayerAction.instance.GetPlayerData().IsAtMaxHP() || !PlayerAction.instance.GetPlayerData().IsAtMaxMP())
        {
            PlayerAction.instance.GetPlayerData().FullRestoreHP();
            PlayerAction.instance.GetPlayerData().FullRestoreMP();

            firstTimeHealing = false;

            Destroy(firstTimeHealingIndicator);
        }
    }

}
