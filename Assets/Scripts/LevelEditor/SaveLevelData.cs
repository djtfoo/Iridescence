using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SaveLevelData : MonoBehaviour {

    public GameObject terrain;
    public string fileDirectory = "LevelPrefab/";

    GameObject clonedTerrain;

    public void SaveTerrainData()
    {
        clonedTerrain = new GameObject();

        DeleteGridZero();   // delete grids that were not filled up
        GenerateEdgeData(); // generate edge data for pathfinding
        SavePrefab();   // save Terrain into a prefab
        Destroy(clonedTerrain);
    }

    private void DeleteGridZero()
    {
        //foreach (Transform child in terrain.transform)
        for (int i = terrain.transform.childCount - 1; i >= 0; --i)
        {
            Transform child = terrain.transform.GetChild(i);
            AssetInfo gridInfo = child.GetComponent<AssetInfo>();
            //if (gridInfo.GetID() == 0)
            //{
            //    DestroyImmediate(child.gameObject);
            //}
            //else
            //{
            //    DestroyImmediate(gridInfo);
            //}
            if (gridInfo.GetID() != 0)
            {
                Transform tile = (Transform)Instantiate(child, child.position, Quaternion.identity);
                tile.parent = clonedTerrain.transform;
                DestroyImmediate(tile.GetComponent<AssetInfo>());
            }
        }
    }

    private void CheckForDuplicate(OutlineEdge edge, List<OutlineEdge> list)
    {
        bool duplicateFound = false;
        for (int i = 0; i < list.Count; ++i)
        {
            //if (edge == list[i])
            if (edge.Equals(list[i]))
            {
                //OutlineEdge toShift = list[i];
                list.RemoveAt(i);
                //list.Insert(0, toShift);
                //++idx;
                duplicateFound = true;
                break;
            }
        }
        if (!duplicateFound)
            list.Add(edge);
    }

    private void GenerateEdgeData()
    {
        GenerateGrids gridsData = terrain.GetComponent<GenerateGrids>();
        float gridWidth = gridsData.gridWidth;
        float gridHeight = gridsData.gridHeight;

        List<OutlineEdge> edgeBackslash = new List<OutlineEdge>();      // slash \
        List<OutlineEdge> edgeForwardslash = new List<OutlineEdge>();   // slash /
        foreach (Transform child in clonedTerrain.transform)  // each grid
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
            CheckForDuplicate(edge1, edgeBackslash);
            CheckForDuplicate(edge2, edgeBackslash);
            CheckForDuplicate(edge3, edgeForwardslash);
            CheckForDuplicate(edge4, edgeForwardslash);
        }

        // add together outlines along the same line
        for (int i = edgeBackslash.Count - 1; i >= 0; --i)
        {
            for (int j = i - 1; j >= 0; --j)
                if (OutlineEdge.CanAddTogether(edgeBackslash[i], edgeBackslash[j]))
                {
                    //edgeBackslash[j].Add(edgeBackslash[i]);
                    edgeBackslash[j] = edgeBackslash[j] + edgeBackslash[i];
                    edgeBackslash.RemoveAt(i);
                    break;
                }
        }
        for (int i = edgeForwardslash.Count - 1; i >= 0; --i)
        {
            for (int j = i - 1; j >= 0; --j)
                if (OutlineEdge.CanAddTogether(edgeForwardslash[i], edgeForwardslash[j]))
                {
                    //edgeForwardslash[j].Add(edgeForwardslash[i]);
                    edgeForwardslash[j] = edgeForwardslash[j] + edgeForwardslash[i];
                    edgeForwardslash.RemoveAt(i);
                    break;
                }
        }

        SaveOutlineData(edgeBackslash, edgeForwardslash);  // save outline data to a txt file

        // add to OutlineEdgeData component
        //OutlineEdgeData edgeData = clonedTerrain.AddComponent<OutlineEdgeData>() as OutlineEdgeData;
        //for (int i = 0; i < edgeBackslash.Count; ++i)
        //{
        //    edgeData.AddBackslash(edgeBackslash[i]);
        //}
        //for (int i = 0; i < edgeForwardslash.Count; ++i)
        //{
        //    edgeData.AddForwardslash(edgeForwardslash[i]);
        //}
    }

    private void SaveOutlineData(List<OutlineEdge> edgeBackslash, List<OutlineEdge> edgeForwardslash)
    {
        // save to txt
        string outlineTxt = "";
        for (int i = 0; i < edgeBackslash.Count; ++i)
        {
            outlineTxt += edgeBackslash[i].ConvertToString(true);
        }

        for (int i = 0; i < edgeForwardslash.Count; ++i)
        {
            outlineTxt += edgeForwardslash[i].ConvertToString(false);
        }

        TxtHandler.WriteToTxt(outlineTxt, "Assets/Resources/OutlineData/testOutline.txt");

        // add to OutlineEdgeData component
        OutlineEdgeData edgeData = clonedTerrain.AddComponent<OutlineEdgeData>() as OutlineEdgeData;
        edgeData.outlineFile = Resources.Load("OutlineData/testOutline") as TextAsset;
    }

    private void SavePrefab()
    {
        GameObject prefab = null;
        prefab = Resources.Load<GameObject>(fileDirectory + "testPrefab");
        if (prefab) {
            PrefabUtility.ReplacePrefab(clonedTerrain, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }
        else {
            //prefab = new GameObject();
            PrefabUtility.CreatePrefab(fileDirectory + "testPrefab" + ".prefab", clonedTerrain);
        }

        //prefab.name = 
    }

}
