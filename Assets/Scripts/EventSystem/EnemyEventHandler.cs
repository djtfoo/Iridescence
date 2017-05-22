using UnityEngine;
using System.Collections;

public class EnemyEventHandler : MonoBehaviour {

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

        // walk to enemy
        Vector2 enemyPos = this.transform.parent.position;
        PlayerMovement.instance.destination = new Vector3(enemyPos.x, enemyPos.y, PlayerMovement.instance.transform.position.z);
        PlayerMovement.instance.velocity = (PlayerMovement.instance.destination - PlayerMovement.instance.transform.position).normalized;

        // attack enemy
#endif
    }

    // signify able to attack enemy
    void OnMouseOver()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // for now, just change colour
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
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