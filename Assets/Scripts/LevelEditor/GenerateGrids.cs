using UnityEngine;
using System.Collections;

public class GenerateGrids : MonoBehaviour {

    public Transform gridPrefab;
    public int rows;
    public int columns;

    public float gridWidth;
    public float gridHeight;

    // Use this for initialization
    void Start()
    {
        GenerateEmptyGrid();
    }

    void Reset()
    {
        DeleteGrid();
        GenerateEmptyGrid();
    }

    void GenerateEmptyGrid()
    {
        float posX, posY;
        for (int y = 0; y <= rows; ++y)
        {
            posX = gridWidth * y;
            posY = -gridHeight * y;
            for (int x = 0; x <= columns; ++x, posX += gridWidth, posY += gridHeight)
            {
                //float posX = (gridWidth * y) + (gridWidth * x);
                //float posY = (-gridHeight * y) + (gridHeight * x);

                Transform newTile = (Transform)Instantiate(gridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newTile.SetParent(this.transform);
                newTile.position = new Vector3(posX, posY, 1f);

                SpriteRenderer sr = newTile.GetComponent<SpriteRenderer>();
                sr.color = new Color(0.8f, 0.8f, 0.8f);
            }
        }
    }

    void DeleteGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
