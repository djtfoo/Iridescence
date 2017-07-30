using UnityEngine;
using System.Collections;

public class DepthSortManager : MonoBehaviour {

    public float minY = 500f;    // should be nearest
    public float maxY = -500f;     // should be furthest
    public float minZ = 0f;     // nearest
    public float maxZ = -10f;   // furthest away

    private static float zUnitPerY;    // how much change in z-axis per 1 unit of y-axis
    public static float GetZUnitPerY() { return zUnitPerY; }

    public static DepthSortManager instance;

	// Use this for initialization
	void Start () {
        instance = GameObject.FindGameObjectWithTag("OverallManager").GetComponent<DepthSortManager>();

        zUnitPerY = (maxZ - minZ) / (maxY - minY);
	}
	
    /// <summary>
    /// @ desc  Function that calculates the depth the object should be at, depending on its y-value
    /// </summary>
    /// <param name="position"></param>
    /// <returns> float  result z-coordinate value </returns>
	public float CalculateZCoord(float yCoord)
    {
        return zUnitPerY * (yCoord - minY) + minZ;
    }

}
