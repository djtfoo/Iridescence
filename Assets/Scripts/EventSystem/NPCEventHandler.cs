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

        // to save the click target
        //RaycastInfo.clickTarget = this;   // to store it
        // also store the type into RaycastInfo via getting tag/name
        //RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        // walk to NPC first

        // activate Dialogue Manager
        //DialogueManager.dManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        DialogueManager.dManager.InitDialogue(this.gameObject);

        PlayerMovement.instance.velocity = Vector3.zero;
        //dManager.RunDialogue(clickTarget.GetComponent<NPCDialogue>().GetDialogue());
        //dManager.CloseDialogue();
#endif
    }

    // signify able to click for dialogue - dialogue box appears above NPC
    void OnMouseOver()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // for now, just change colour
        this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
#endif
    }

    void OnMouseExit()
    {
#if LEVELEDITOR
        
#else
        if (DialogueManager.inDialogue)
            return;

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
#endif
    }

}
