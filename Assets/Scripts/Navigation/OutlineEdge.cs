using UnityEngine;
using System.Collections;

public struct OutlineEdge
{
    // Y = mX + c  as variables
    public Vector2 pt1, pt2;   // start & end points that define the line
    public float gradient;
    public float c_intersection;

    static float forgivenessValue = 0.01f;

    public void SetOutline(Vector2 pt1, Vector2 pt2)
    {
        this.pt1 = pt1;
        this.pt2 = pt2;
        CalculateGradient();
        CalculateCIntersection();
    }

    private void CalculateGradient()
    {
        // can only be used when 2 points are defined

        // gradient = (y1 - y0) / (x1 - x0)
        gradient = (pt1.y - pt2.y) / (pt1.x - pt2.x);
    }

    private void CalculateCIntersection()
    {
        // can only be used when gradient & 2 points are defined

        // derived from y0 = m(x0) + C; gradient = (y1 - y0) / (x1 - x0)
        // where x0 and y0 are values of x and y when x = 0
        // and x1 and y1 are values of x and y of any coordinate along the line
        Vector2 ptToUse; // for the calculation
        if (pt1.x == 0)
            ptToUse = pt2;
        else
            ptToUse = pt1;

        c_intersection = ptToUse.y - (gradient * ptToUse.x);
    }

    static bool CheckTwoFloatValuesAreEqual(float val1, float val2)
    {
        if (Mathf.Abs(val1 - val2) <= forgivenessValue)
            return true;

        return false;
    }

    static bool CheckTwoPointsAreEqual(Vector2 pt1, Vector2 pt2)
    {
        //if ((pt1.x - pt2.x <= forgivenessValue || pt2.x - pt1.x <= forgivenessValue) &&
        //    (pt1.y - pt2.y <= forgivenessValue || pt2.y - pt1.y <= forgivenessValue))
        if (CheckTwoFloatValuesAreEqual(pt1.x, pt2.x) && CheckTwoFloatValuesAreEqual(pt1.y, pt2.y))
            return true;

        return false;
    }

    // comparison - to check for deleting same edges
    public static bool operator ==(OutlineEdge edge1, OutlineEdge edge2)
    {
        if (CheckTwoPointsAreEqual(edge1.pt1, edge2.pt1) && CheckTwoPointsAreEqual(edge1.pt2, edge2.pt2)) //&&
            //(edge1.gradient == edge2.gradient) &&
            //(edge1.c_intersection == edge2.c_intersection))
            return true;

        return false;
    }
    public bool Equals(OutlineEdge other)
    {
        if (CheckTwoPointsAreEqual(this.pt1, other.pt1) && CheckTwoPointsAreEqual(this.pt2, other.pt2))
            return true;

        return false;
    }

    public static bool operator !=(OutlineEdge edge1, OutlineEdge edge2)
    {
        if (!CheckTwoPointsAreEqual(edge1.pt1, edge2.pt1) || !CheckTwoPointsAreEqual(edge1.pt2, edge2.pt2)) //||
            //(edge1.gradient != edge2.gradient) ||
            //(edge1.c_intersection != edge2.c_intersection))
            return false;

        return true;
    }

    public static bool IsSameGradient(OutlineEdge edge1, OutlineEdge edge2)
    {
        if (CheckTwoFloatValuesAreEqual(edge1.gradient, edge2.gradient))
            return true;

        return false;
    }

    // add together - when combining to form the outline shape
    public static bool CanAddTogether(OutlineEdge edge1, OutlineEdge edge2)
    {
        //if (edge1.gradient == edge2.gradient &&
        //    (edge1.pt1 == edge2.pt1 || edge1.pt1 == edge2.pt2 ||
        //    edge1.pt2 == edge2.pt1 || edge1.pt2 == edge2.pt2))
        //    return true;

        // if same c_intersection, they lie on the same line
        if (CheckTwoFloatValuesAreEqual(edge1.gradient, edge2.gradient) &&
            (CheckTwoPointsAreEqual(edge1.pt1, edge2.pt1) || CheckTwoPointsAreEqual(edge1.pt1, edge2.pt2) ||
            CheckTwoPointsAreEqual(edge1.pt2, edge2.pt1) || CheckTwoPointsAreEqual(edge1.pt2, edge2.pt2)))
            // dunnid check again cause the edges are created with the same direction
            return true;

        return false;
    }
    public void Add(OutlineEdge other)
    {
        // assume gradient is the same already and they can be added together.
        // conditions for adding together:
        // gradients are the same; they share 1 common point
        if (CheckTwoPointsAreEqual(this.pt1, other.pt1))
            this.pt1 = other.pt2;
        else if (CheckTwoPointsAreEqual(this.pt2, other.pt1))
            this.pt2 = other.pt2;
        else if (CheckTwoPointsAreEqual(this.pt1, other.pt2))
            this.pt1 = other.pt1;
        else //if (this.pt2 == other.pt2)
            this.pt2 = other.pt1;
    }

}