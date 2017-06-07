using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPathfinder : MonoBehaviour {

    OutlineEdgeData edgeData;
    Vector2 endPoint;
    float zVal;

    List<OutlineEdge> backslashEdges = new List<OutlineEdge>();
    List<OutlineEdge> forwardslashEdges = new List<OutlineEdge>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CalculatePath(Vector3 destination, ref List<Vector3> waypoints)
    {
        if (edgeData == null)
        {
            edgeData = GameObject.Find("Terrain").GetComponent<OutlineEdgeData>();
            backslashEdges = edgeData.GetBackslashEdges();
            forwardslashEdges = edgeData.GetForwardslashEdges();
        }

        this.endPoint = new Vector2(destination.x, destination.y);
        this.zVal = destination.z;

        OutlineEdge playerToDest = new OutlineEdge();   // OutlineEdge to be easier to do calculations
        playerToDest.SetOutline(this.transform.position, destination);

        // check for intersections - against one type of edge first - in this case, backslash first

        List<OutlineEdge> intersections = new List<OutlineEdge>();
        List<Vector2> intersectionPoints = new List<Vector2>();

        foreach (OutlineEdge edge in backslashEdges)
        {
            if (CheckIntersection(playerToDest, edge))
            {
                // there is intersection with this edge
                intersections.Add(edge);
                intersectionPoints.Add(CalculateIntersectionPoint(playerToDest, edge));
            }
        }

        foreach (OutlineEdge edge in forwardslashEdges)
        {
            if (CheckIntersection(playerToDest, edge))
            {
                // there is intersection with this edge
                intersections.Add(edge);
                intersectionPoints.Add(CalculateIntersectionPoint(playerToDest, edge));
            }
        }

        // sort intersections by distance to player so the entry & exit of an intersection are aligned by index
        if (intersections.Count > 0)
        {
            SortListByDistFromStart(ref intersections, ref intersectionPoints);

            // form path
            FindPath(intersections[0], ref waypoints);
        }

        //for (int i = 0; i < intersections.Count; ++i)
        //{
        //    Vector2 intersectPt = CalculateIntersectionPoint(intersections[i], intersectionsTwo[i]);
        //    waypoints.Add(intersectPt);
        //}

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
        // sort from edge nearest to destination to edge furthest from destination
        //Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        for (int i = 0; i < edges.Count; ++i)
        {
            float dist1 = (intersectionPts[i] - endPoint).sqrMagnitude;

            for (int j = 0; j < edges.Count - i; ++j)
            {
                float dist2 = (intersectionPts[j] - endPoint).sqrMagnitude;

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

    private void FindPath(OutlineEdge startingEdge, ref List<Vector3> waypoints)
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        List<Vector2> path1 = new List<Vector2>();
        List<Vector2> path2 = new List<Vector2>();

        path1.Add(startingEdge.pt1);
        path2.Add(startingEdge.pt2);

        OutlineEdge edgeOfPath1 = startingEdge;
        OutlineEdge edgeOfPath2 = startingEdge;

        bool run = true;
        bool findNextOutline = true;
        bool isPath1 = false;

        while (run)
        {
            // do a check
            OutlineEdge checkPath1 = new OutlineEdge();
            checkPath1.SetOutline(path1[path1.Count - 1], playerPos);
            if (IntersectionsCount(checkPath1) == 0)
            {
                isPath1 = true;
                break;
            }

            OutlineEdge checkPath2 = new OutlineEdge();
            checkPath2.SetOutline(path2[path2.Count - 1], playerPos);
            if (IntersectionsCount(checkPath2) == 0)
            {
                isPath1 = false;
                break;
            }

            // add next range of outlines
            if (findNextOutline)
            {
                FindNextOutlinePoint(ref path1, ref edgeOfPath1);
                FindNextOutlinePoint(ref path2, ref edgeOfPath2);
            }
            else
            {
                path1.Add(edgeOfPath1.GetOtherPointOfThisLine(path1[path1.Count - 1]));
                path2.Add(edgeOfPath2.GetOtherPointOfThisLine(path2[path2.Count - 1]));
            }
            findNextOutline = !findNextOutline;
        }

        if (isPath1) {
            for (int i = path1.Count - 1; i >= 0; --i) {
                waypoints.Add(new Vector3(path1[i].x, path1[i].y, zVal));
            }
        }
        else {
            for (int i = path2.Count - 1; i >= 0; --i) {
                waypoints.Add(new Vector3(path2[i].x, path2[i].y, zVal));
            }
        }


    }

    private int IntersectionsCount(OutlineEdge checkPath)
    {
        int intersectionsCount = 0;

        foreach (OutlineEdge edge in backslashEdges)
        {
            if (CheckIntersection(checkPath, edge)) {
                Vector2 intersectionPt = CalculateIntersectionPoint(checkPath, edge);
                if (!OutlineEdge.CheckTwoPointsAreEqual(intersectionPt, edge.pt1) &&
                    !OutlineEdge.CheckTwoPointsAreEqual(intersectionPt, edge.pt2))
                    ++intersectionsCount;
            }
        }
        foreach (OutlineEdge edge in forwardslashEdges)
        {
            if (CheckIntersection(checkPath, edge)) {
                Vector2 intersectionPt = CalculateIntersectionPoint(checkPath, edge);
                if (!OutlineEdge.CheckTwoPointsAreEqual(intersectionPt, edge.pt1) &&
                    !OutlineEdge.CheckTwoPointsAreEqual(intersectionPt, edge.pt2))
                    ++intersectionsCount;
            }
        }

        return intersectionsCount;
    }
    private void FindNextOutlinePoint(ref List<Vector2> path, ref OutlineEdge edgeOfThisPath)
    {
        foreach (OutlineEdge edge in backslashEdges)
        {
            if (edgeOfThisPath.Equals(edge))
                continue;

            if (edge.IsConnected(path[path.Count - 1]))
            {
                path.Add(edge.GetContinuousPoint(path[path.Count - 1]));
                edgeOfThisPath = edge;
                return;
            }
        }
        foreach (OutlineEdge edge in forwardslashEdges)
        {
            if (edgeOfThisPath.Equals(edge))
                continue;

            if (edge.IsConnected(path[path.Count - 1]))
            {
                path.Add(edge.GetContinuousPoint(path[path.Count - 1]));
                edgeOfThisPath = edge;
                return;
            }
        }
    }

}
