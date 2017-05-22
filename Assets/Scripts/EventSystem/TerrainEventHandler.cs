using UnityEngine;
using System.Collections;

public class TerrainEventHandler : MonoBehaviour {

    // left click - maybe used to place props/NPC assets (1-time placement only)
    private void OnMouseDown()
    {
#if LEVELEDITOR

#else
        if (DialogueManager.inDialogue)
            return;

        // Player Movement stuff
        RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

        PlayerAction.instance.SetDestination(new Vector3(RaycastInfo.hit2D.point.x, RaycastInfo.hit2D.point.y, PlayerAction.instance.transform.position.z));
        PlayerAction.instance.SetVelocity((PlayerAction.instance.GetDestination() - PlayerAction.instance.transform.position).normalized);
#endif
    }

    // do click functions here - to have click & hold functionalities
    private void OnMouseOver()
    {
#if LEVELEDITOR
        // highlight tile
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        // set ghost object
        if (LevelEditorManager.leManager.GetGhostID() == 0) {
            goto rightClick;
        }
        LevelEditorManager.leManager.SetGhostPosition(this.transform.position);


        // left click - to add a tile/asset
        // switch case - when want place NPC/Prop on a terrain tile
        if (Input.GetMouseButton(0))
        {
            switch (LevelEditorManager.leManager.GetGhostObjectType())
            {
                case GO_TYPE.GO_TERRAIN:
                    // swap sprite
                    //RaycastInfo.clickTarget.GetComponent<SpriteRenderer>().sprite = LevelEditorManager.leManager.GetGhostObject().GetComponent<SpriteRenderer>().sprite;
                    this.GetComponent<SpriteRenderer>().sprite = (LevelEditorManager.leManager.GetGhostObject()).GetComponent<SpriteRenderer>().sprite;

                    // transfer tile ID
                    this.GetComponent<AssetInfo>().SetID(LevelEditorManager.leManager.GetGhostID());
                    break;
                case GO_TYPE.GO_NPC:
                    break;
                case GO_TYPE.GO_PROP:
                    break;
                case GO_TYPE.GO_NIL:
                default:
                    break;
            }
        }


    // right click - to erase a tile there
    rightClick:
        if (Input.GetMouseButton(1))
        {
            if (this.GetComponent<AssetInfo>().GetID() != 0) {  // if it's not empty
                this.GetComponent<SpriteRenderer>().sprite = LevelEditorManager.leManager.grid;
                // transfer tile ID
                this.GetComponent<AssetInfo>().SetID(0);
            }
        }
#else

#endif
    }

    private void OnMouseExit()
    {
#if LEVELEDITOR
        // un-highlight tile
        this.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);

        // set ghost object to inactive
        LevelEditorManager.leManager.SetGhostInactive();
#endif
    }

}
