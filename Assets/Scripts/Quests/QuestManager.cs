using UnityEngine;
using System.Collections.Generic;

public enum QUEST_STATE
{
    QUEST_LOCKED,
    QUEST_AVAILABLE,
    QUEST_INPROGRESS,
    QUEST_COMPLETED
}

public class QuestManager : MonoBehaviour {

    List<MainQuest> mainquests = new List<MainQuest>();
    List<SideQuest> sidequests = new List<SideQuest>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
