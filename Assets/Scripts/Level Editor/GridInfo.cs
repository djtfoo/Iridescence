using UnityEngine;
using System.Collections;

public class GridInfo : MonoBehaviour {

    [SerializeField]
    int tileID = 0;    // type of tile; 0 is default empty/blank

    public void SetTileID(int id) {
        tileID = id;
    }

    public int GetTileID() {
        return tileID;
    }

}
