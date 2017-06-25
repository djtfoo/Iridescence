using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// NOT IN USE

public enum RaycastTargetType
{
    Raycast_Terrain,
    Raycast_Enemy,
    Raycast_NPC,
    Raycast_Waypoint,
    Raycast_TransitionPortal,
    Raycast_NIL
}

// use in both game & level editor
public static class RaycastInfo {

    //public static RaycastHit hit;
    public static RaycastHit2D hit2D;
    public static RaycastTargetType raycastType;
    //public static GameObject clickTarget;   // for knowing what object type the target is, e.g. can check if target enemy is within attack range

    static float getMouse0InputTimer = 0f;
    const float mouse0InputTimer = 0.05f;

    public static GameObject raycastTarget;   // target of mouseover before clicking
    public static GameObject clickTarget; // for knowing what object type the target is, e.g. can check if target enemy is within attack range


    public static GameObject GetRaycastTarget2D()
    {
        hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit2D.collider != null)
        {
            if (hit2D.transform.gameObject.tag == "Terrain")
            {
                raycastType = RaycastTargetType.Raycast_Terrain;
                if (SceneManager.GetActiveScene().name == "GameScene")
                    return null;
                else if (SceneManager.GetActiveScene().name == "LevelEditor")
                    return hit2D.transform.gameObject;
            }
            else if (hit2D.transform.gameObject.tag == "Enemy")
            {
                raycastType = RaycastTargetType.Raycast_Enemy;
                return hit2D.transform.gameObject;
            }
            else if (hit2D.transform.gameObject.tag == "NPC")
            {
                raycastType = RaycastTargetType.Raycast_NPC;
                return hit2D.transform.gameObject;
            }
            else if (hit2D.transform.gameObject.tag == "Waypoint")
            {
                raycastType = RaycastTargetType.Raycast_Waypoint;
                return hit2D.transform.gameObject;
            }
            else if (hit2D.transform.gameObject.tag == "Portal")
            {
                raycastType = RaycastTargetType.Raycast_TransitionPortal;
                return hit2D.transform.gameObject;
            }
        }

        raycastType = RaycastTargetType.Raycast_NIL;
        //clickTarget = null;   // remove this bc previous click is stil valid
        return null;
    }

    // CHANGE THIS TO EVENT ONMOUSEOVER()
    public static void MouseUpdate()
    {
        // put all this mouse stuff into a different script
        if (getMouse0InputTimer < mouse0InputTimer)
            getMouse0InputTimer += Time.deltaTime;
        else
        {
            GameObject tempTarget = GetRaycastTarget2D();

            // if mouse is on a different target
            if (raycastTarget != tempTarget)
            {
                // remove previous highlight
                if (raycastTarget)  // != null
                {
                    if (SceneManager.GetActiveScene().name == "GameScene")
                    {
                        if (raycastTarget.tag == "Enemy" || raycastTarget.tag == "NPC")
                        {
                            //SpriteRenderer sr = raycastTarget.GetComponent<SpriteRenderer>();
                            //sr.color = new Color(1, 1, 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "LevelEditor")
                    {
                        if (raycastTarget.tag == "Terrain")
                        {
                            //SpriteRenderer sr = raycastTarget.GetComponent<SpriteRenderer>();
                            //sr.color = new Color(0.8f, 0.8f, 0.8f);
                        }
                    }
                }

                // highlight new target IF necessary
                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    switch (raycastType)
                    {
                        case RaycastTargetType.Raycast_Enemy:
                        case RaycastTargetType.Raycast_NPC:
                            {
                                //SpriteRenderer sr = tempTarget.GetComponent<SpriteRenderer>();
                                //sr.color = new Color(1, 0.5f, 0.5f);
                            }
                            break;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "LevelEditor")
                {
                    switch (raycastType)
                    {
                        case RaycastTargetType.Raycast_Terrain:
                            {
                                //SpriteRenderer sr = tempTarget.GetComponent<SpriteRenderer>();
                                //sr.color = new Color(1, 1, 1);
                            }
                            break;

                        default:
                            break;
                    }
                }

                raycastTarget = tempTarget;
            }
            getMouse0InputTimer = 0f;
        }
    }

    // FOR 3D, NOT IN USE
    /*public static GameObject GetRaycastTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastInfo.hit))
        {
            if (hit.transform.gameObject.name == "Terrain")
            {
                raycastType = RaycastTargetType.Raycast_Terrain;
                return null;
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                raycastType = RaycastTargetType.Raycast_Enemy;
                return hit.transform.gameObject;
            }
        }

        raycastType = RaycastTargetType.Raycast_NIL;
        //clickTarget = null;   // remove this bc previous click is stil valid
        return null;
    }*/

    // NOT IN USE
    /*
    public static bool GetHitpoint(ref Vector3 destination)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Terrain")
            {
                destination = hit.point;
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                destination = hit.transform.gameObject.transform.position;
            }
        
            return true;
        }

        return false;
    }*/

    // NOT IN USE
    /*
    public static Vector3 GetHitpoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Terrain")
                return hit.point;
            else if (hit.transform.gameObject.tag == "Enemy")
                return new Vector3(0, 0, 0);
        }

        return new Vector3(0, 0, 0);
    }*/

}
