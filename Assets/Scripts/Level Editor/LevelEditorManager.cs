using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

enum GO_TYPE
{
    GO_TERRAIN,
    GO_NPC,
    GO_PROP,
    GO_NIL
}

// class to handle GameObjects placed in the editor
public class LevelEditorManager : MonoBehaviour {

    public Sprite grid;

    // ghost tile/GameObject
    public Transform objectGhostPrefab;
    Transform selectedObject;

    // ghost tile/GameObject info
    GO_TYPE objectGhostType;
    int objectGhostID;

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;

        //objectGhostType = GO_TYPE.GO_NIL;
        objectGhostType = GO_TYPE.GO_TERRAIN;
        //objectGhostID = 0;
        objectGhostID = 1;

        selectedObject = (Transform)Instantiate(objectGhostPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        SpriteRenderer sr = selectedObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        // mouse matters
        RaycastInfo.MouseUpdate();

        // move ghost tile
        if (RaycastInfo.raycastTarget)
        {
            if (!selectedObject.gameObject.activeSelf)
                selectedObject.gameObject.SetActive(true);
            selectedObject.localPosition = RaycastInfo.raycastTarget.transform.position;
        }
        else
        {
            if (selectedObject.gameObject.activeSelf)
                selectedObject.gameObject.SetActive(false);
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //selectedObject.localPosition = new Vector3(mousePos.x, mousePos.y, -1f);
        }

        // Left mouse click
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                goto finishLeftClick;

            // see whether you're clicking on an object, or terrain
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

            switch (objectGhostType)
            {
                case GO_TYPE.GO_TERRAIN:
                    if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Terrain) {
                        RaycastInfo.clickTarget.GetComponent<SpriteRenderer>().sprite = objectGhostPrefab.GetComponent<SpriteRenderer>().sprite;
                        // transfer tile ID
                        RaycastInfo.clickTarget.GetComponent<GridInfo>().SetTileID(0);
                    }

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

        finishLeftClick:
        if (Input.GetMouseButton(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                goto finishRightClick;

            // see whether you're clicking on an object, or terrain
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

            switch (objectGhostType)
            {
                case GO_TYPE.GO_TERRAIN:
                    if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Terrain)
                    {
                        RaycastInfo.clickTarget.GetComponent<SpriteRenderer>().sprite = grid;
                        // transfer tile ID
                        RaycastInfo.clickTarget.GetComponent<GridInfo>().SetTileID(objectGhostID);
                    }

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

        finishRightClick: ;

    }
}
