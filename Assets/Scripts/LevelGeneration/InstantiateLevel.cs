using UnityEngine;
using System.Collections;

public class InstantiateLevel : MonoBehaviour {

    Transform levelPrefab;   // prefab of exported terrain from level editor
    int levelCount;

    Transform terrain;

    private void Awake()
    {
        levelCount = 0;
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
}
