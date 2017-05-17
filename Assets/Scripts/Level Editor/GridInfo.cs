using UnityEngine;
using System.Collections;

public class GridInfo : MonoBehaviour {

    int tileNum = 0;    // type of tile; 0 is default empty/blank

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeTile(int num) {
        tileNum = num;
    }

}
