using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveLevelData : MonoBehaviour {

    public GameObject terrain;  // terrain to be saved

    [Tooltip("Retrieve filename from input field text")]
    public Text locationName;
    [Tooltip("Retrieve background Render from dropdown label")]
    public Text backgroundRender;
    [Tooltip("Retrieve filename from input field text")]
    public Text fileName;

    public string fileDirectory = "LevelPrefab/";

    private GameObject clonedTerrain;

    public void SaveTerrainData()
    {
        if (fileName.text == "")
            return;

        TerrainInfo terrainInfo = terrain.GetComponent<TerrainInfo>();
        terrainInfo.SetLocationName(locationName.text);
        terrainInfo.SetBackground(backgroundRender.text);

        clonedTerrain = new GameObject();
        TerrainInfo terrainInfoClone = terrain.AddComponent<TerrainInfo>();
        terrainInfoClone.SetLocationName(locationName.text);
        terrainInfoClone.SetBackground(backgroundRender.text);

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
            if (gridInfo.GetGOType() != GO_TYPE.GO_TERRAIN || gridInfo.GetID() != 0)
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
            edge1.SetIsBackslash(true);
            OutlineEdge edge2 = new OutlineEdge();
            edge2.SetOutline(new Vector2(child.position.x, child.position.y + gridHeight),
                new Vector2(child.position.x + gridWidth, child.position.y));
            edge2.SetIsBackslash(true);

            // next 2 edges are /
            OutlineEdge edge3 = new OutlineEdge();
            edge3.SetOutline(new Vector2(child.position.x - gridWidth, child.position.y),
                new Vector2(child.position.x, child.position.y + gridHeight));
            edge3.SetIsBackslash(false);
            OutlineEdge edge4 = new OutlineEdge();
            edge4.SetOutline(new Vector2(child.position.x, child.position.y - gridHeight),
                new Vector2(child.position.x + gridWidth, child.position.y));
            edge4.SetIsBackslash(false);

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

        string filename = fileName.text;

        TxtHandler.WriteToTxt(outlineTxt, "Assets/Resources/OutlineData/" + filename + ".txt");

        // add to OutlineEdgeData component
        OutlineEdgeData edgeData = clonedTerrain.AddComponent<OutlineEdgeData>() as OutlineEdgeData;
        edgeData.outlineFile = Resources.Load("OutlineData/" + filename) as TextAsset;
    }

    private void SavePrefab()
    {
        GameObject prefab = null;
        //string filename = "testPrefab";
        string filename = fileName.text;
        prefab = Resources.Load<GameObject>(fileDirectory);
        if (prefab) {
#if UNITY_EDITOR
            PrefabUtility.ReplacePrefab(clonedTerrain, prefab, ReplacePrefabOptions.ConnectToPrefab);
#endif
        }
        else {
#if UNITY_EDITOR
            //prefab = new GameObject();
            PrefabUtility.CreatePrefab(fileDirectory + filename + ".prefab", clonedTerrain);
#endif
        }

        //prefab.name = 
    }

}
