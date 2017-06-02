using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutlineEdgeData : MonoBehaviour {

    List<OutlineEdge> edges = new List<OutlineEdge>();
    // split OutlineEdge[] into different arrays - one for each area sector

    public void AddToList(OutlineEdge edge/*int areaSector*/)
    {
        edges.Add(edge);

        Debug.Log("pt1:" + edge.pt1.x + " " + edge.pt1.y + " | pt2:" + edge.pt2.x + " " + edge.pt2.y);
    }

	// Use this for initialization
	void Start () {
        //edges = new List<OutlineEdge>();
    }
	
	// Update is called once per frame
	void Update () {
	
    }

}
