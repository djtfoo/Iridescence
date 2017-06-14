using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

// singleton class
public class QuestManager : MonoBehaviour {

    //List<MainQuest> mainquests = new List<MainQuest>();
    //List<SideQuest> sidequests = new List<SideQuest>();

    MainQuest[] mainquests;
    SideQuest[] sidequests;

    MainQuest activeMainQuest;
    List<SideQuest> activeSideQuests = new List<SideQuest>();

    public static QuestManager instance;

    // Use this for initialization
    void Start () {

        instance = GameObject.Find("GameDataManager").GetComponent<QuestManager>();

        // read TextAsset files to retrieve quest data

        // create listeners for quests that are locked
        /// main quest flow - listen out for a main quest getting completed
        /// side quests

        // create listeners for completing objectives of ongoing quests
        /// active main quest
        /// active side quests

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
