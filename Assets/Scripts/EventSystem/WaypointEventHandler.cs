using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WaypointEventHandler : MonoBehaviour
{

    // left click - maybe used to place props/NPC assets (1-time placement only)
    private void OnMouseDown()
    {
#if LEVELEDITOR
        // create menu with options:
        // key in location name
        // key in location prefab name
        // remove this waypoint
        // only 1 waypoint per scene
        //
        // EditWaypoints (WaypointManager) will save to csv, the location name & its corresponding prefab
        // in Level Editor, user must edit waypoint manager

#else
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (DialogueManager.inDialogue)
            return;

        // store the type into RaycastInfo via getting tag/name
        RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        // walk to waypoint
        Vector2 waypointPos = this.transform.parent.position;
        PlayerAction.instance.SetMoveTo(new Vector3(waypointPos.x, waypointPos.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetDestination(new Vector3(enemyPos.x, enemyPos.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetVelocity((PlayerAction.instance.GetDestination() - PlayerAction.instance.transform.position).normalized);
#endif
    }

    private void OnMouseEnter()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // highlight colour
        this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f);

        // set GameHUD highlight information
        GameHUD.instance.highlightInfo.SetHighlightInfo(this.name, this.tag);
#endif
    }
    
    /*private void OnMouseOver()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // do things like right click
        //if (Input.GetMouseButtonDown(1))    // right click
        //{
        //    // store the type into RaycastInfo via getting tag/name
        //    RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();
        //
        //    // walk to enemy
        //    Vector2 waypointPos = this.transform.parent.position;
        //    PlayerAction.instance.SetMoveTo(new Vector3(waypointPos.x, waypointPos.y, PlayerAction.instance.transform.position.z));
        //}

#endif
    }*/

    private void OnMouseExit()
    {
#if LEVELEDITOR
        
#else
        if (DialogueManager.inDialogue)
            return;

        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        // remove GameHUD highlight information
        GameHUD.instance.highlightInfo.DeactivateHighlightInfo();
#endif
    }

}