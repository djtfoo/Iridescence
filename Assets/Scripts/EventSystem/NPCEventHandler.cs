using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NPCEventHandler : MonoBehaviour {

    public GameObject speechBubble;

    private void Start()
    {
        speechBubble.SetActive(false);
    }

    // left click - maybe used to place props/NPC assets (1-time placement only)
    private void OnMouseDown()
    {
#if LEVELEDITOR

#else
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (DialogueManager.inDialogue)
            return;

        if (PlayerAction.instance.IsAttacking())    // in midst of attack animation
            return;

        RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        // walk to NPC
        Vector2 NPCPos = this.transform.parent.position;
        PlayerAction.instance.SetMoveTo(new Vector3(NPCPos.x, NPCPos.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetDestination(new Vector3(NPCPos.x, NPCPos.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetVelocity((PlayerAction.instance.GetDestination() - PlayerAction.instance.transform.position).normalized);

        // to save the click target
        //RaycastInfo.clickTarget = this;   // to store it
        // also store the type into RaycastInfo via getting tag/name
        //RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        // walk to NPC first

        // activate Dialogue Manager
        //DialogueManager.dManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        //dManager.RunDialogue(clickTarget.GetComponent<NPCDialogue>().GetDialogue());
        //dManager.CloseDialogue();

        //DialogueManager.dManager.InitDialogue(this.gameObject);
        //
        //PlayerAction.instance.SetVelocityZero();
#endif
    }

    // signify able to click for dialogue - dialogue box appears above NPC
    private void OnMouseEnter()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // for now, just change colour - we want a speech bubble with "..." animation
        this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);

        // set GameHUD highlight information
        GameHUD.instance.highlightInfo.SetHighlightInfo(this.name, this.tag);

        // activate speech bubble animation
        speechBubble.SetActive(true);
        speechBubble.GetComponent<SpriteAnimator>().ResetAnimation();   // set back to frame 0
#endif
    }

    private void OnMouseExit()
    {
#if LEVELEDITOR
        
#else
        if (DialogueManager.inDialogue)
            return;

        // reset highlight colour
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        // remove GameHUD highlight information
        GameHUD.instance.highlightInfo.DeactivateHighlightInfo();

        // remove speech bubble animation
        speechBubble.SetActive(false);
#endif
    }

    public void EndDialogue()
    {
        // reset highlight colour
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        speechBubble.GetComponent<SpriteAnimator>().SetFreezeAnimation(false);

        // remove GameHUD highlight information
        GameHUD.instance.highlightInfo.DeactivateHighlightInfo();

        // remove speech bubble animation
        speechBubble.SetActive(false);
    }

}
