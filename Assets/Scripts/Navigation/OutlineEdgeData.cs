using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutlineEdgeData : MonoBehaviour {

    List<OutlineEdge> edges;
    // split OutlineEdge[] into different arrays - one for each area sector

    // DEBUG
    bool linesAdded = false;
    float duration = 10f;
    int idxToDraw = 0;

    public void AddToList(OutlineEdge edge/*int areaSector*/)
    {
        edges.Add(edge);
        linesAdded = true;

        Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
    }

	// Use this for initialization
	void Start () {
        edges = new List<OutlineEdge>();
    }
	
	// Update is called once per frame
	void Update () {
	
        //if (linesAdded)
        //{
        //    duration += Time.deltaTime;
        //    if (duration > 10f) {
        //        duration = 0f;
        //
        //        Debug.DrawLine(edges[idxToDraw].pt1, edges[idxToDraw].pt2, Color.red, 12f);
        //        ++idxToDraw;
        //        if (idxToDraw == edges.Count)
        //            idxToDraw = 0;
        //    }
        //}
    }

}
