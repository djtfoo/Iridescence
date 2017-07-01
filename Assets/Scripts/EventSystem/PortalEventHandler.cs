using UnityEngine;
using UnityEngine.EventSystems;

public class PortalEventHandler : MonoBehaviour {

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

        // Player Movement stuff
        RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        PlayerAction.instance.SetMoveTo(new Vector3(RaycastInfo.hit2D.point.x, RaycastInfo.hit2D.point.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetDestination(new Vector3(RaycastInfo.hit2D.point.x, RaycastInfo.hit2D.point.y, PlayerAction.instance.transform.position.z));
        //PlayerAction.instance.SetVelocity((PlayerAction.instance.GetDestination() - PlayerAction.instance.transform.position).normalized);
#endif
    }
    
}
