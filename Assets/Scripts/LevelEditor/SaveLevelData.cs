using UnityEngine;
using System.Collections;
using UnityEditor;

public class SaveLevelData : MonoBehaviour {

    public GameObject terrain;
    public string fileDirectory = "LevelPrefab/";

    //GameObject savedTerrain;

    public void SaveTerrainData()
    {
        //savedTerrain = terrain;

        //foreach (Transform child in terrain.transform)
        for (int i = terrain.transform.childCount - 1; i >= 0; --i)
        {
            Transform child = terrain.transform.GetChild(i);
            AssetInfo gridInfo = child.GetComponent<AssetInfo>();
            if (gridInfo.GetID() == 0)
            {
                DestroyImmediate(child.gameObject);
            }
            else
            {
                DestroyImmediate(gridInfo);
            }
        }

        SavePrefab();
        GenerateEdgeData(); // generate edge data for pathfinding
    }

    void SavePrefab()
    {
        GameObject prefab = null;
        prefab = Resources.Load<GameObject>(fileDirectory + "testPrefab");
        if (prefab) {
            PrefabUtility.ReplacePrefab(terrain, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }
        else {
            prefab = new GameObject();
            PrefabUtility.CreatePrefab(fileDirectory + "testPrefab" + ".prefab", terrain);
        }

        //prefab.name = 
    }

    void GenerateEdgeData()
    {

    }

}
