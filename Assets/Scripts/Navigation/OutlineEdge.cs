using UnityEngine;
using System.Collections;

struct OutlineEdge
{
    // Y = mX + c  as variables
    public Vector2 pt1, pt2;   // start & end points that define the line
    public float gradient;
    public float c_intersection;

    private void CalculateGradient()
    {
        // gradient = (y1 - y0) / (x1 - x0)
        gradient = (pt1.y - pt2.y) / (pt1.x - pt2.x);
    }

    private void CalculateCIntersection()
    {
        // derived from y0 = m(x0) + C; gradient = (y1 - y0) / (x1 - x0)
        // where x0 and y0 are values of x and y when x = 0
        // and x1 and y1 are values of x and y of any coordinate along the line
        Vector2 ptToUse; // for the calculation
        if (pt1.x == 0)
            ptToUse = pt1;
        else
            ptToUse = pt2;

        c_intersection = ptToUse.y - (gradient * ptToUse.x);
    }

    // comparison - to check for deleting same edges
    public static bool operator ==(OutlineEdge edge1, OutlineEdge edge2)
    {
        if ((edge1.pt1 == edge2.pt1) && (edge1.pt2 == edge2.pt2) &&
            (edge1.gradient == edge2.gradient) &&
            (edge1.c_intersection == edge2.c_intersection))
            return true;

        return false;
    }
    public static bool operator !=(OutlineEdge edge1, OutlineEdge edge2)
    {
        if ((edge1.pt1 != edge2.pt1) || (edge1.pt2 != edge2.pt2) ||
            (edge1.gradient != edge2.gradient) ||
            (edge1.c_intersection != edge2.c_intersection))
            return false;

        return true;
    }

    // add together - when combining to form the outline shape
    public static bool CanAddTogether(OutlineEdge edge1, OutlineEdge edge2)
    {
        //if (edge1.gradient == edge2.gradient &&
        //    (edge1.pt1 == edge2.pt1 || edge1.pt1 == edge2.pt2 ||
        //    edge1.pt2 == edge2.pt1 || edge1.pt2 == edge2.pt2))
        //    return true;

        // if same c_intersection, they lie on the same line
        if (edge1.c_intersection == edge2.c_intersection)
            return true;

        return false;
    }
    public void Add(OutlineEdge other)
    {
        // assume gradient is the same already and they can be added together.
        // conditions for adding together:
        // gradients are the same; they share 1 common point
        if (this.pt1 == other.pt1)
            this.pt1 = other.pt2;
        else if (this.pt2 == other.pt1)
            this.pt2 = other.pt2;
        else if (this.pt1 == other.pt2)
            this.pt1 = other.pt1;
        else //if (this.pt2 == other.pt2)
            this.pt2 = other.pt1;
    }

}