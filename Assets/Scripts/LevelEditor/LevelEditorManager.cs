using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// class to handle GameObjects placed in the editor
public class LevelEditorManager : MonoBehaviour {

    public Sprite grid;

    // ghost tile/GameObject
    public Transform objectGhostPrefab;
    Transform ghostObject;

    public static LevelEditorManager leManager;

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

        leManager = GameObject.Find("LevelEditorManager").GetComponent<LevelEditorManager>();
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
    public GO_TYPE GetGhostObjectType()
    {
        return ghostObjectType;
    }
    public int GetGhostID()
    {
        return ghostObjectID;
    }
    public void SetGhostInactive()
    {
        ghostObject.gameObject.SetActive(false);
    }
    public void SetGhostPosition(Vector3 pos)
    {
        if (!ghostObject.gameObject.activeSelf)
            ghostObject.gameObject.SetActive(true);
        ghostObject.localPosition = pos;
    }

    // Update is called once per frame
    void Update () {

    }

}
