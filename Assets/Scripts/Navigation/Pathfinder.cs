using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// NO LONGER IN USE
public class Pathfinder : MonoBehaviour {

    OutlineEdgeData edgeData;
    
    // Use this for initialization
    void Start () {
        //edgeData = GameObject.Find("Terrain").GetComponent<OutlineEdgeData>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // should there be a gap within a straight-line path,
    // there will be 2 intersections per gap: an entry point, and an exit point
    // the intersection points will usually (but not always) be on opposite types of edges
    public void CalculatePath(Vector3 destination, ref List<Vector3> waypoints)
    {
        if (edgeData == null)
            edgeData = GameObject.Find("Terrain").GetComponent<OutlineEdgeData>();

        OutlineEdge playerToDest = new OutlineEdge();   // OutlineEdge to be easier to do calculations
        playerToDest.SetOutline(this.transform.position, destination);

        // check for intersections - against one type of edge first - in this case, backslash first
        List<OutlineEdge> backslashEdges = edgeData.GetBackslashEdges();
        List<OutlineEdge> intersections = new List<OutlineEdge>();
        List<Vector2> intersectionPoints = new List<Vector2>();

        foreach (OutlineEdge edge in backslashEdges)
        {
            //if (OutlineEdge.IsSameGradient(edge, playerToDest))
            //    continue;
            //
            //// calculate intersection point (assuming line is infinite)
            //// here, line1 = playerToDest; line2 = edge
            //Vector2 intersectPt = CalculateIntersectionPoint(playerToDest, edge);
            //
            //// check whether intersection point is within both line segments
            //// line1: playerToDest
            //if (!CheckPointWithinLineSegment(playerToDest, intersectPt))
            //    continue;
            //
            //// line2: edge in for loop
            //if (!CheckPointWithinLineSegment(edge, intersectPt))
            //    continue;

            if (CheckIntersection(playerToDest, edge)) {
                // there is intersection with this edge
                intersections.Add(edge);
                intersectionPoints.Add(CalculateIntersectionPoint(playerToDest, edge));
            }
        }
        // [TEMPORARY!!] only check against other list if there are intersections
        // there may be cases where intersections are only of one edge type
        if (intersections.Count != 0)   // [TEMPORARY!!]
        {
            List<OutlineEdge> forwardslashEdges = edgeData.GetForwardslashEdges();
            List<OutlineEdge> intersectionsTwo = new List<OutlineEdge>();
            List<Vector2> intersectionTwoPoints = new List<Vector2>();
            foreach (OutlineEdge edge in forwardslashEdges)
            {
                if (CheckIntersection(playerToDest, edge)) {
                    // there is intersection with this edge
                    intersectionsTwo.Add(edge);
                    intersectionTwoPoints.Add(CalculateIntersectionPoint(playerToDest, edge));
                }
            }

            // sort intersections by distance to player so the entry & exit of an intersection are aligned by index
            if (intersections.Count > 1) {
                SortListByDistFromStart(ref intersections, ref intersectionPoints);
                SortListByDistFromStart(ref intersectionsTwo, ref intersectionTwoPoints);
            }

            // ASSUME ONLY 1 INTERSECTION FOR NOW
            for (int i = 0; i < intersections.Count; ++i)
            {
                Vector2 intersectPt = CalculateIntersectionPoint(intersections[i], intersectionsTwo[i]);
                waypoints.Add(new Vector3(intersectPt.x, intersectPt.y, destination.z));
            }
            

        }   // end of if intersections.Count == 0

        waypoints.Add(destination);     // final waypoint
    }

    // return true if there is intersection
    private bool CheckIntersection(OutlineEdge movementLine, OutlineEdge edge)
    {
        if (OutlineEdge.IsSameGradient(edge, movementLine))
            return false;

        // calculate intersection point (assuming line is infinite)
        // here, line1 = playerToDest; line2 = edge
        Vector2 intersectPt = CalculateIntersectionPoint(movementLine, edge);

        // check whether intersection point is within both line segments
        // line1: playerToDest
        if (!CheckPointWithinLineSegment(movementLine, intersectPt))
            return false;

        // line2: edge in for loop
        if (!CheckPointWithinLineSegment(edge, intersectPt))
            return false;

        return true;
    }

    private Vector2 CalculateIntersectionPoint(OutlineEdge line1, OutlineEdge line2)
    {
        // formula to get intersection point (assuming line is infinite):
        // xCoord = (c2 - c1) / (m1 - m2)
        // yCoord = m1 * xCoord + c1
        Vector2 intersectPt = new Vector2();
        intersectPt.x = (line2.c_intersection - line1.c_intersection) / (line1.gradient - line2.gradient);
        intersectPt.y = line1.gradient * intersectPt.x + line1.c_intersection;

        return intersectPt;
    }

    private bool CheckPointWithinLineSegment(OutlineEdge line, Vector3 point)
    {
        float minX, maxX, minY, maxY;
        if (line.pt1.x > line.pt2.x)
        {
            minX = line.pt2.x;
            maxX = line.pt1.x;
        }
        else
        {
            minX = line.pt1.x;
            maxX = line.pt2.x;
        }
        if (line.pt1.y > line.pt2.y)
        {
            minY = line.pt2.y;
            maxY = line.pt1.y;
        }
        else
        {
            minY = line.pt1.y;
            maxY = line.pt2.y;
        }

        if (point.x >= minX && point.x <= maxX &&
            point.y >= minY && point.y <= maxY)
            return true;

        return false;
    }

    private void SortListByDistFromStart(ref List<OutlineEdge> edges, ref List<Vector2> intersectionPts)
    {
        // sort from least to most
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        for (int i = 0; i < edges.Count; ++i)
        {
            float dist1 = (intersectionPts[i] - playerPos).sqrMagnitude;

            for (int j = 0; j < edges.Count - i; ++j)
            {
                float dist2 = (intersectionPts[j] - playerPos).sqrMagnitude;

                if (dist1 < dist2)  // ascending
                {
                    // swap
                    OutlineEdge tempEdge = edges[i];
                    edges[i] = edges[j];
                    edges[j] = tempEdge;

                    Vector2 tempPos = intersectionPts[i];
                    intersectionPts[i] = intersectionPts[j];
                    intersectionPts[j] = tempPos;
                }
            }
        }
    }

}
