using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    private Transform levelPrefab;   // prefab of exported terrain from level editor
    //int levelCount;

    private Transform terrain = null;

    public string currLocationName = "";    // current location the player is at

    Camera mainCamera;

    // init level variables for other stuff
    // e.g. player pos

    public static LevelGenerator instance;

    private void Awake()
    {
        instance = GetComponent<LevelGenerator>();  // this

        // set main camera -- for setting background render
        mainCamera = Camera.main;
    }

    private void Start()
    {
        // READ from save file, which level/area the player is at; then load that prefab
        string levelPrefab = PlayerAction.instance.GetPlayerData().lastCheckpoint;
        if (levelPrefab == "")
            LoadLevel("crystallinecave", false);
        else
            LoadLevel(levelPrefab, false);
    }

    public void LoadLevel(string filename, bool transitFromElsewhere)
    {
        //DestroyTerrain();   // destroy existing terrain first
        GenerateTerrainFromPrefab(filename);    // generate next location's terrain

        if (transitFromElsewhere)   // player came from another level through a portal
        {
            foreach (Transform child in terrain)
            {
                if (child.tag == "Portal")
                {
                    if (child.GetChild(0).GetComponent<Portal>().connectedLocationName == currLocationName)
                    {
                        // at this point, player's velocity has not been reset yet. take a slight step forward
                        /// rationale: if player wishes to head back, can easily do so by clicking the arrow.
                        PlayerAction.instance.transform.position = child.position + PlayerAction.instance.GetVelocity() * 0.05f;
                        break;
                    }
                }
            }
        }
        else
            terrain.GetComponent<TerrainInfo>().SetPlayerPos();

        currLocationName = terrain.GetComponent<TerrainInfo>().locationName;
    }

    /// <summary>
    /// @desc Function to generate terrain from prefab
    /// </summary>
    /// <param name="filename"> name of prefab file inside Resources/LevelPrefab </param>
    private void GenerateTerrainFromPrefab(string filename)
    {
        if (terrain != null)
        {
            DestroyTerrain();
        }

        // reset GameHUD
        GameHUD.instance.ResetHUD();

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
        Destroy(terrain.gameObject);
    }

}
