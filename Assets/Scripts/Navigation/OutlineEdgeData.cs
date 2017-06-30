using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class OutlineEdgeData : MonoBehaviour {

    public TextAsset outlineFile;

    List<OutlineEdge> edgesBackSlash = new List<OutlineEdge>();     // slash \
    List<OutlineEdge> edgesForwardSlash = new List<OutlineEdge>();  // slash /
    // split OutlineEdge[] into different arrays - one for each area sector

    // DEBUG - DRAW LINE
    int idx = 0;
    float timer = 0.5f;
    float duration = 0.5f;
    bool backslashComplete = false;

    public void AddBackslash(OutlineEdge edge/*int areaSector*/)
    {
        edgesBackSlash.Add(edge);

        //Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
    }
    public void AddForwardslash(OutlineEdge edge/*int areaSector*/)
    {
        edgesForwardSlash.Add(edge);

        //Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
    }

    // Getters
    public List<OutlineEdge> GetBackslashEdges()
    {
        return edgesBackSlash;
    }
    public List<OutlineEdge> GetForwardslashEdges()
    {
        return edgesForwardSlash;
    }

    // Use this for initialization
    void Awake () {
        // init edge data
        if (outlineFile) {
            string[] lines = TxtHandler.GetTxtLines(outlineFile);
            ParseEdgeData(lines);
        }
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void ParseEdgeData(string[] lines)
    {
        for (int i = 0; i < lines.Length; ++i)
        {
            if (lines[i] == "" || lines[i][0] == '#' || lines[i][0] == '\r')   // either blank or comment
                continue;

            // add data to list
            OutlineEdge newEdge = new OutlineEdge();
            string[] values = Regex.Split(lines[i], ",");

            int isBackslash = int.Parse(values[0]);
            newEdge.pt1.Set(float.Parse(values[1]), float.Parse(values[2]));
            newEdge.pt2.Set(float.Parse(values[3]), float.Parse(values[4]));
            newEdge.gradient = float.Parse(values[5]);
            newEdge.c_intersection = float.Parse(values[6]);

            if (isBackslash == 1) { // true
                newEdge.SetIsBackslash(true);
                AddBackslash(newEdge);
            }
            else {
                newEdge.SetIsBackslash(false);
                AddForwardslash(newEdge);
            }
        }
    }

}
