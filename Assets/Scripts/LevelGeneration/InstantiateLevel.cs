using UnityEngine;
using System.Collections;

public class InstantiateLevel : MonoBehaviour {

    Transform levelPrefab;   // prefab of exported terrain from level editor
    //int levelCount;

    Transform terrain = null;

    // init level variables for other stuff
    // e.g. player pos

    private void Awake()
    {

        // READ from save file, which level/area the player is at; then load that prefab

        levelPrefab = Resources.Load("LevelPrefab/testPrefab", typeof(Transform)) as Transform; // temp directory

        terrain = (Transform)Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        terrain.name = "Terrain";
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
    public void GenerateTerrainFromPrefab(string filepath)
    {
        if (terrain == null)
            DestroyTerrain();

        levelPrefab = Resources.Load("LevelPrefab/" + filepath, typeof(Transform)) as Transform;

        terrain = (Transform)Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
        terrain.name = "Terrain";
    }

    /// <summary>
    /// @desc Function to destroy an existing terrain
    /// </summary>
    private void DestroyTerrain()
    {
        DestroyImmediate(terrain);
    }

}
