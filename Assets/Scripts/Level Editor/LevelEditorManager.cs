using UnityEngine;
using System.Collections;

// class to handle GameObjects placed in the editor
public class LevelEditorManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;
    }
	
	// Update is called once per frame
	void Update () {

        RaycastInfo.MouseUpdate();

        // Left mouse click
        if (Input.GetMouseButton(0))
        {
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

            switch (RaycastInfo.raycastType)
            {
                case RaycastTargetType.Raycast_Terrain:
                    break;
                case RaycastTargetType.Raycast_Enemy:
                    break;
                case RaycastTargetType.Raycast_NPC:
                    break;
                case RaycastTargetType.Raycast_NIL:
                default:
                    break;
            }
        }
    }
}
