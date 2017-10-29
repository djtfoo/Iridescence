using UnityEngine;
using System.Collections;

public class ShowQuestInfo : MonoBehaviour {

    public QuestInfo questInfo;

    public string questName;
    public string questDescription;
    public string questRequirement;
    public string questStatus;

	public void Show()
    {
        if (questInfo.gameObject.activeSelf && questInfo.questName.text == questName)  // quest box is already showing this quest, toggle to inactive
            questInfo.gameObject.SetActive(false);

        else
        {
            questInfo.gameObject.SetActive(true);

            questInfo.questName.text = questName;
            questInfo.descriptionText.text = questDescription;
            questInfo.requirementText.text = questRequirement;
            questInfo.statusText.text = "Status: " + questStatus;
        }
    }

}
