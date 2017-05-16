using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GenerateLevel : MonoBehaviour {

    public TextAsset csvFile;
    int[,] levelGrid;

    public Transform genericTile;
    public Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        // get level grid data
        levelGrid = CSVReader.GetCSVGridInt(csvFile.text);

        int rows = levelGrid.GetUpperBound(0);
        int columns = levelGrid.GetUpperBound(1);

        // create terrain
        // increase X by 0.62, increase Y by 0.36
        Vector3 startingPos = Vector3.zero;
        for (int y = 0; y <= rows; ++y)
        {
            for (int x = 0; x <= columns; ++x)
            {
                if (levelGrid[y, x] == 0)
                    continue;

                float posX = (0.62f * y) + (0.62f * x);
                float posY = (-0.36f * y) + (0.36f * x);

                Transform newTile = (Transform)Instantiate(genericTile, new Vector3(0, 0, 0), Quaternion.identity);
                newTile.SetParent(this.transform);
                newTile.position = new Vector3(posX, posY, 1f);
                switch (levelGrid[y,x])
                {
                    case 1:
                        newTile.GetComponent<SpriteRenderer>().sprite = sprites[0];
                        break;
                    case 2:
                        newTile.GetComponent<SpriteRenderer>().sprite = sprites[1];
                        break;
                    case 3:
                        newTile.GetComponent<SpriteRenderer>().sprite = sprites[2];
                        break;
                    case 4:
                        newTile.GetComponent<SpriteRenderer>().sprite = sprites[3];
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // test only
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //this.csvFile = Resources.Load("Data/Level/level2.csv") as TextAsset;
            //SceneManager.LoadScene("2ndTest2D");
        }
    }
}
