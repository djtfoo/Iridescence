using UnityEngine;
using System.Collections;

public enum RaycastTargetType
{
    Raycast_Terrain,
    Raycast_Enemy,
    Raycast_NIL
}

public static class RaycastInfo {

    //public static RaycastHit hit;
    public static RaycastHit2D hit2D;
    public static RaycastTargetType raycastType;
    //public static GameObject clickTarget;   // for knowing what object type the target is, e.g. can check if target enemy is within attack range

    public static GameObject GetRaycastTarget2D()
    {
        hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit2D.collider != null)
        {
            if (hit2D.transform.gameObject.tag == "Terrain")
            {
                raycastType = RaycastTargetType.Raycast_Terrain;
                return null;
            }
            else if (hit2D.transform.gameObject.tag == "Enemy")
            {
                raycastType = RaycastTargetType.Raycast_Enemy;
                return hit2D.transform.gameObject;
            }
        }

        raycastType = RaycastTargetType.Raycast_NIL;
        //clickTarget = null;   // remove this bc previous click is stil valid
        return null;
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
