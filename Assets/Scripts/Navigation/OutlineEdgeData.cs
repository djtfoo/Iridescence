using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutlineEdgeData : MonoBehaviour {

    List<OutlineEdge> edgesBackSlash = new List<OutlineEdge>();     // slash \
    List<OutlineEdge> edgesForwardSlash = new List<OutlineEdge>();  // slash /
    // split OutlineEdge[] into different arrays - one for each area sector

    public void AddBackslash(OutlineEdge edge/*int areaSector*/)
    {
        edgesBackSlash.Add(edge);

        Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
    }
    public void AddForwardslash(OutlineEdge edge/*int areaSector*/)
    {
        edgesForwardSlash.Add(edge);

        Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
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
    void Start () {
        //edges = new List<OutlineEdge>();
    }
	
	// Update is called once per frame
	void Update () {

    }

}
