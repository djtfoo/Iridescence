using UnityEngine;
using System.Collections;

public class InstantiateLevel : MonoBehaviour {

    Transform levelPrefab;   // prefab of exported terrain from level editor
    //int levelCount;

    Transform terrain = null;

    Camera mainCamera;

    // init level variables for other stuff
    // e.g. player pos

    private void Awake()
    {
        // READ from save file, which level/area the player is at; then load that prefab

        // set main camera -- for setting background render
        mainCamera = Camera.main;

        GenerateTerrainFromPrefab("testPrefab");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// @desc Function to generate terrain from prefab
    /// </summary>
    /// <param name="filename"> name of prefab file inside Resources/LevelPrefab </param>
    public void GenerateTerrainFromPrefab(string filename)
    {
        if (terrain == null)
            DestroyTerrain();

        levelPrefab = Resources.Load("LevelPrefab/" + filename, typeof(Transform)) as Transform;

        terrain = (Transform)Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        terrain.name = "Terrain";

        // set background render -- default is black if all fails
        string backgroundName = terrain.GetComponent<TerrainInfo>().background;
        foreach (Transform child in mainCamera.transform)
        {
            if (child.name == "Background_" + backgroundName)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// @desc Function to destroy an existing terrain
    /// </summary>
    private void DestroyTerrain()
    {
        DestroyImmediate(terrain);
    }

}
