using UnityEngine;
using System.Collections;

// requirements to complete a quest
public class QuestObjectives {

    Requirement[] ongoingObjectives;    // conditions that have yet to be met
    Requirement[] completedObjectives;  // conditions that have been met

    void AllObjectivesCompleted()
    {
        // event trigger to complete quest
        //QuestManager.instance.SendMessage("CompleteQuest", <pointer to this quest>);
    }

}
