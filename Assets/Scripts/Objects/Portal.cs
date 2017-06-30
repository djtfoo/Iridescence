using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public string connectedLocationName;    // name of the location this portal connects to
    public string connectedPrefabName;  // name of the prefab of the level this portal connects to

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToNextLevel()
    {
        InstantiateLevel.instance.LoadLevel(connectedPrefabName);
    }

}
