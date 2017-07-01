using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    // reference to PlayerData
    private PlayerData playerData;

	// Use this for initialization
	void Start () {
        playerData = PlayerAction.instance.GetPlayerData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddToInventory()
    {
        // add to PlayerData

        // Destroy this item
        Destroy(this.gameObject);
    }

}
