using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {

    // level data
    public TextAsset csvFile;
    int[,] levelGrid;

    // tiles for generating world
    public float width;    // width in pixels divided by 2, then divide by 100
    public float height;   // height in pixels divided by 2, then divide by 100
    public Transform genericTile;
    public Sprite[] tileSprites;

    // Use this for initialization
    void Start()
    {
        GenerateWorld();
    }

    // Update is called once per frame
    void Update()
    {
        // test only
        if (Input.GetKeyDown(KeyCode.Tab))  // change level
        {
            // remove/reset scene here
            DeleteWorld();
            // set new level data
            this.csvFile = Resources.Load("LevelData/level2") as TextAsset;
            GenerateWorld();
        }
    }

    void DeleteWorld()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void GenerateWorld()
    {
        // get level grid data
        levelGrid = CSVReader.GetCSVGridInt(csvFile.text);

        int rows = levelGrid.GetUpperBound(0);
        int columns = levelGrid.GetUpperBound(1);

        // create terrain
        for (int y = 0; y <= rows; ++y)
        {
            for (int x = 0; x <= columns; ++x)
            {
                if (levelGrid[y, x] == 0)
                    continue;

                float posX = (width * y) + (width * x);
                float posY = (-height * y) + (height * x);

                Transform newTile = (Transform)Instantiate(genericTile, new Vector3(0, 0, 0), Quaternion.identity);
                newTile.SetParent(this.transform);
                newTile.position = new Vector3(posX, posY, 1f);
                switch (levelGrid[y, x])
                {
                    case 1:
                        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[0];
                        break;
                    case 2:
                        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[1];
                        break;
                    case 3:
                        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[2];
                        break;
                    case 4:
                        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[3];
                        break;
                }
            }
        }
    }

}
