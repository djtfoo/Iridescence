using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    //[Tooltip("Name of the location this waypoint is located at")]
    //public string locationName;    // show name of linked location when in traversing selection menu
    [Tooltip("Name of the prefab of the location this waypoint is in")]
    public string levelPrefabFilename; // name of the prefab of the level this waypoint connects to

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

    public void HealPlayer()
    {
        if (firstTimeHealing)
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

}
