using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TerrainEventHandler : MonoBehaviour {

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

    // do click functions here - to have click & hold functionalities
    private void OnMouseOver()
    {
#if LEVELEDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            goto rightClick;
        }

        // highlight tile
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        // set ghost object
        if (LevelEditorManager.leManager.GetGhostID() == 0) {
            goto rightClick;
        }
        LevelEditorManager.leManager.SetGhostPosition(transform.position);


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

    // placement of objects in world
    private void OnMouseUp()
    {
#if LEVELEDITOR
        switch (LevelEditorManager.leManager.GetGhostObjectType())
        {
            case GO_TYPE.GO_TERRAIN:
                break;
            case GO_TYPE.GO_NPC:
                break;
            case GO_TYPE.GO_PROP:
                break;
            case GO_TYPE.GO_WAYPOINT:
                GameObject searchWaypoint = GameObject.FindGameObjectWithTag("Waypoint");
                if (searchWaypoint == null) // only create a waypoint if none exists
                {
                    Transform waypointPrefab = Resources.Load<Transform>("LevelAssets/WaypointObject");
                    Transform waypoint = (Transform)Instantiate(waypointPrefab, LevelEditorManager.leManager.GetGhostPosition(), Quaternion.identity);
                    waypoint.parent = LevelEditorManager.leManager.terrain;
                }
                break;
            case GO_TYPE.GO_NIL:
            default:
                break;
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
