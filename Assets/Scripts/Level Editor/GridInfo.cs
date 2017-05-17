using UnityEngine;
using System.Collections;

public class GridInfo : MonoBehaviour {

    int tileID = 0;    // type of tile; 0 is default empty/blank

    public void ChangeTileID(int id) {
        tileID = id;
    }

}
