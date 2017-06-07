using UnityEngine;
using System.Collections;

public class ToggleQuestLog : MonoBehaviour {

    public GameObject questLogData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // button on click
    public void ToggleShowQuestData()
    {
        questLogData.SetActive(!questLogData.activeSelf);
    }

}
