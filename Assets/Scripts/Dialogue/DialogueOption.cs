using UnityEngine;
using System.Collections;

public class DialogueOption : MonoBehaviour {

    int index = -2; // index of the response of this option; -2 default for "Bye"

    public void SetDialogue()
    {
        DialogueManager.dManager.SetNextMessage(index);
    }

    public void CloseDialogue()
    {
        DialogueManager.dManager.SetCloseDialogue();
    }

    public void SetIndex(int idx)
    {
        index = idx;
    }
}
