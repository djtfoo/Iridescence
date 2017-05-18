using UnityEngine;
using System.Collections;

public class LoadResources : MonoBehaviour {

    Transform[] resources;
    public string path; // directory in the Resources folder to load

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadAll()
    {
        if (resources.Length == 0)
            resources = Resources.LoadAll<Transform>(path);

        // generate each tile as a button to change user's ghost tile
        // set the listener to call function

        // when press button close, unload all resources
    }

    public void UnloadResources()
    {

    }

}
