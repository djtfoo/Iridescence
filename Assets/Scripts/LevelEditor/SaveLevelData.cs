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
        DeleteGridZero();   // delete grids that were not filled up
        GenerateEdgeData(); // generate edge data for pathfinding
        SavePrefab();   // save Terrain into a prefab
    }

    void DeleteGridZero()
    {
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
    }

    void GenerateEdgeData()
    {
        OutlineEdgeData edgeData = terrain.GetComponent<OutlineEdgeData>();
        GenerateGrids gridsData = terrain.GetComponent<GenerateGrids>();
        float halfGridWidth = gridsData.gridWidth * 0.5f;
        float halfGridHeight = gridsData.gridHeight * 0.5f;

        foreach (Transform child in terrain.transform)  // each grid
        {
            // create the 4 outlines of the grid
            //  first 2 edges are \
            OutlineEdge edge1 = new OutlineEdge();
            edge1.SetOutline(new Vector2(child.position.x - halfGridWidth, child.position.y),
                new Vector2(child.position.x, child.position.y - halfGridHeight));
            OutlineEdge edge2 = new OutlineEdge();
            edge2.SetOutline(new Vector2(child.position.x, child.position.y + halfGridHeight),
                new Vector2(child.position.x + halfGridWidth, child.position.y));
            // next 2 edges are /
            OutlineEdge edge3 = new OutlineEdge();
            edge3.SetOutline(new Vector2(child.position.x - halfGridWidth, child.position.y),
                new Vector2(child.position.x, child.position.y + halfGridHeight));

            OutlineEdge edge4 = new OutlineEdge();
            edge4.SetOutline(new Vector2(child.position.x, child.position.y - halfGridHeight),
                new Vector2(child.position.x + halfGridWidth, child.position.y));

            // check if these outlines are the same as existing outlines

        }

        // add together outlines along the same line

        // DEBUG: draw out the lines one by one
        
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

}
