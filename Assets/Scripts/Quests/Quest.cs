using UnityEngine;
using System.Collections;

public enum QUEST_STATE
{
    QUEST_LOCKED,
    QUEST_AVAILABLE,
    QUEST_INPROGRESS,
    QUEST_COMPLETED
}

public abstract class Quest {

    string questName;
    QUEST_STATE questState;

    public string eventTrigger;

    public string GetQuestName() { return questName; }
    public QUEST_STATE GetQuestState() { return questState; }

    public abstract void InitQuestData();

}

public class MainQuest : Quest {

    //int questIdx;
    QuestObjectives objectives; // quest objectives

    public override void InitQuestData()
    {
        // read XML file
    }

}

public class SideQuest : Quest {

    // pre-requisite
    // NPC that provides the quest
    QuestObjectives objectives; // quest objectives
    QuestConditions conditions; // quest unlock conditions

    // add reward

    public override void InitQuestData()
    {
        // read XML file
    }

}
