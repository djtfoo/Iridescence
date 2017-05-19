using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// class to handle GameObjects placed in the editor
public class LevelEditorManager : MonoBehaviour {

    public Sprite grid;

    // ghost tile/GameObject
    public Transform objectGhostPrefab;
    Transform ghostObject;

    // ghost tile/GameObject info
    GO_TYPE ghostObjectType;
    int ghostObjectID;

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;

        ghostObjectType = GO_TYPE.GO_NIL;
        ghostObjectID = 0;

        ghostObject = (Transform)Instantiate(objectGhostPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        SpriteRenderer sr = ghostObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0.5f);
    }

    // Get Transform of ghost object
    public Transform GetGhostObject()
    {
        return ghostObject;
    }
    // Set when selecting tile from dropdown list
    public void SetGhostObject(Sprite sprite, GO_TYPE type, int id)
    {
        ghostObject.GetComponent<SpriteRenderer>().sprite = sprite;
        ghostObjectType = type;
        ghostObjectID = id;
    }

	// Update is called once per frame
	void Update () {

        // mouse matters
        RaycastInfo.MouseUpdate();

        // if no ghost object selected
        if (ghostObjectID == 0) {
            if (ghostObject.gameObject.activeSelf)
                ghostObject.gameObject.SetActive(false);
            goto finishLeftClick;
        }

        // move ghost tile
        if (RaycastInfo.raycastTarget)
        {
            if (!ghostObject.gameObject.activeSelf)
                ghostObject.gameObject.SetActive(true);
            ghostObject.localPosition = RaycastInfo.raycastTarget.transform.position;
        }
        else
        {
            if (ghostObject.gameObject.activeSelf)
                ghostObject.gameObject.SetActive(false);
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

            switch (ghostObjectType)
            {
                case GO_TYPE.GO_TERRAIN:
                    if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Terrain) {
                        RaycastInfo.clickTarget.GetComponent<SpriteRenderer>().sprite = ghostObject.GetComponent<SpriteRenderer>().sprite;
                        // transfer tile ID
                        RaycastInfo.clickTarget.GetComponent<AssetInfo>().SetID(0);
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

            switch (ghostObjectType)
            {
                case GO_TYPE.GO_TERRAIN:
                    if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Terrain)
                    {
                        RaycastInfo.clickTarget.GetComponent<SpriteRenderer>().sprite = grid;
                        // transfer tile ID
                        RaycastInfo.clickTarget.GetComponent<AssetInfo>().SetID(ghostObjectID);
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
