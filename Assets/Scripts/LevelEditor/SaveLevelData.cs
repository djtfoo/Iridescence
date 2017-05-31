using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

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

    void CheckForDuplicate(OutlineEdge edge, List<OutlineEdge> list, ref int idx)
    {
        bool duplicateFound = false;
        for (int i = idx; i < list.Count; ++i)
        {
            if (edge == list[i])
            {
                OutlineEdge toShift = list[i];
                list.RemoveAt(i);
                list.Insert(i, toShift);
                ++idx;
                duplicateFound = true;
                break;
            }
        }
        if (!duplicateFound)
            list.Add(edge);
    }

    void GenerateEdgeData()
    {
        GenerateGrids gridsData = terrain.GetComponent<GenerateGrids>();
        float gridWidth = gridsData.gridWidth;
        float gridHeight = gridsData.gridHeight;

        List<OutlineEdge> edgeBackslash = new List<OutlineEdge>();      // slash \
        int backslashDoneIdx = 0;   // index for which edges have a duplicate found alr
        List<OutlineEdge> edgeForwardslash = new List<OutlineEdge>();   // slash /
        int forwardslashDoneIdx = 0;    // index for which edges have a duplicate found alr
        foreach (Transform child in terrain.transform)  // each grid
        {
            // create the 4 outlines of the grid
            //  first 2 edges are \
            OutlineEdge edge1 = new OutlineEdge();
            edge1.SetOutline(new Vector2(child.position.x - gridWidth, child.position.y),
                new Vector2(child.position.x, child.position.y - gridHeight));
            OutlineEdge edge2 = new OutlineEdge();
            edge2.SetOutline(new Vector2(child.position.x, child.position.y + gridHeight),
                new Vector2(child.position.x + gridWidth, child.position.y));

            // next 2 edges are /
            OutlineEdge edge3 = new OutlineEdge();
            edge3.SetOutline(new Vector2(child.position.x - gridWidth, child.position.y),
                new Vector2(child.position.x, child.position.y + gridHeight));
            OutlineEdge edge4 = new OutlineEdge();
            edge4.SetOutline(new Vector2(child.position.x, child.position.y - gridHeight),
                new Vector2(child.position.x + gridWidth, child.position.y));

            // check if these outlines are the same as existing outlines
            CheckForDuplicate(edge1, edgeBackslash, ref backslashDoneIdx);
            CheckForDuplicate(edge2, edgeBackslash, ref backslashDoneIdx);
            CheckForDuplicate(edge3, edgeForwardslash, ref forwardslashDoneIdx);
            CheckForDuplicate(edge4, edgeForwardslash, ref forwardslashDoneIdx);
        }

        // add together outlines along the same line
        for (int i = edgeBackslash.Count - 1; i >= 0; --i)
        {
            for (int j = i - 1; j >= 0; --j)
                if (OutlineEdge.CanAddTogether(edgeBackslash[i], edgeBackslash[j]))
                {
                    edgeBackslash[j].Add(edgeBackslash[i]);
                    edgeBackslash.RemoveAt(i);
                    break;
                }
        }
        for (int i = edgeForwardslash.Count - 1; i >= 0; --i)
        {
            for (int j = i - 1; j >= 0; --j)
                if (OutlineEdge.CanAddTogether(edgeForwardslash[i], edgeForwardslash[j]))
                {
                    edgeForwardslash[j].Add(edgeForwardslash[i]);
                    edgeForwardslash.RemoveAt(i);
                    break;
                }
        }

        // add to OutlineEdgeData component
        OutlineEdgeData edgeData = terrain.GetComponent<OutlineEdgeData>();
        for (int i = 0; i < edgeBackslash.Count; ++i)
        {
            edgeData.AddToList(edgeBackslash[i]);
        }
        for (int i = 0; i < edgeForwardslash.Count; ++i)
        {
            edgeData.AddToList(edgeForwardslash[i]);
        }

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
