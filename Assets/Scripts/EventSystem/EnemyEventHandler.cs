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

        //RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        // walk to enemy
        Vector2 enemyPos = this.transform.position;
        PlayerMovement.instance.destination = new Vector3(enemyPos.x, enemyPos.y, PlayerMovement.instance.transform.position.z);
        PlayerMovement.instance.velocity = (PlayerMovement.instance.destination - PlayerMovement.instance.transform.position).normalized;

        // attack enemy
#endif
    }
}
