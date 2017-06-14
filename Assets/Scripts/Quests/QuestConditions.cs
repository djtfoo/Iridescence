using UnityEngine;
using System.Collections;

// requirements to unlock a quest
public class QuestConditions {

    Requirement[] ongoingConditions;    // conditions that have yet to be met
    Requirement[] completedConditions;  // conditions that have been met

    void AllObjectivesCompleted()
    {
        // event trigger to unlock quest
        //QuestManager.instance.SendMessage("UnlockQuest", <pointer to this quest>);
    }

}
