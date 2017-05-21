using UnityEngine;
using System.Collections;

public class NPCEventHandler : MonoBehaviour {

    // left click - maybe used to place props/NPC assets (1-time placement only)
    private void OnMouseDown()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // walk to NPC first
        //DialogueManager.dManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        DialogueManager.dManager.InitDialogue(this.gameObject);
        //dManager.RunDialogue(clickTarget.GetComponent<NPCDialogue>().GetDialogue());
        //dManager.CloseDialogue();
#endif
    }
}
