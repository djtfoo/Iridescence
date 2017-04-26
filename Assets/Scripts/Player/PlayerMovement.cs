using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Vector3 velocity;
    Vector3 destination;
    float getMouse0InputTimer = 0f;
    const float mouse0InputTimer = 0.05f;

    //RaycastHit hit;
    //RaycastTargetType raycastType;
    GameObject raycastTarget;   // target of mouseover before clicking
    GameObject clickTarget; // for knowing what object type the target is, e.g. can check if target enemy is within attack range

    // each weapon and/or attack skill will have its attack range.
    // Weapon objects shld be in PlayerInfo class
    // shld have a "pointer" to the current skill - the one that was clicked to do the attack
    float attackRange;  // temp variable to represent weapon

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        raycastTarget = null;
        clickTarget = null;
        attackRange = 2f;
    }
	
	// Update is called once per frame
	void Update () {

        // put all this mouse stuff into a different script
        if (getMouse0InputTimer < mouse0InputTimer)
            getMouse0InputTimer += Time.deltaTime;
        else
        {
            GameObject tempTarget = RaycastInfo.GetRaycastTarget2D();
            // if mouse is on a different target
            if (raycastTarget != tempTarget)
            {
                // remove previous highlight
                if (raycastTarget)  // != null
                {
                    if (raycastTarget.tag == "Enemy" || raycastTarget.tag == "NPC")
                    {
                        SpriteRenderer sr = raycastTarget.GetComponent<SpriteRenderer>();
                        sr.color = new Color(1, 1, 1);
                    }
                }

                // highlight new target IF necessary
                switch (RaycastInfo.raycastType)
                {
                    case RaycastTargetType.Raycast_Enemy:
                    case RaycastTargetType.Raycast_NPC:
                        {
                            SpriteRenderer sr = tempTarget.GetComponent<SpriteRenderer>();
                            sr.color = new Color(1, 0.5f, 0.5f);
                        }
                        break;
                }

                raycastTarget = tempTarget;
            }
            getMouse0InputTimer = 0f;
        }

        // Left mouse click
        if (Input.GetMouseButton(0))
        {
            clickTarget = RaycastInfo.GetRaycastTarget2D();
            
            switch (RaycastInfo.raycastType)
            {
                case RaycastTargetType.Raycast_Terrain:
                    destination = new Vector3(RaycastInfo.hit2D.point.x, RaycastInfo.hit2D.point.y, this.transform.position.z);
                    velocity = (destination - this.transform.position).normalized;
                    break;
                case RaycastTargetType.Raycast_Enemy:
                    {
                        Vector2 enemyPos = RaycastInfo.hit2D.transform.position;
                        destination = new Vector3(enemyPos.x, enemyPos.y, this.transform.position.z);
                        velocity = (destination - this.transform.position).normalized;
                    }
                    break;
                case RaycastTargetType.Raycast_NPC:
                    {
                        // walk to NPC first
                    }
                    break;
                case RaycastTargetType.Raycast_NIL:
                    break;
            }
        }

        //if (getMouse0InputTimer < mouse0InputTimer)
        //    getMouse0InputTimer += Time.deltaTime;
        //else
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        destination = RaycastInfo.GetHitpoint();
        //        velocity = (destination - this.transform.position).normalized;
        //        //this.transform.position = RaycastInfo.GetHitpoint();
        //        getMouse0InputTimer = 0f;
        //    }
        //}

        // Movement
        if (!velocity.Equals(Vector3.zero))
        {
            if (clickTarget)    // != null
            {
                float distSquared = (destination - this.transform.position).sqrMagnitude;
                if (distSquared < attackRange * attackRange)
                {
                    velocity = Vector3.zero;
                    //clickTarget = null;   // don't null so you can attack that target?
                }
                else
                {
                    this.transform.position += velocity * Time.deltaTime;

                }
            }
            else
            {
                this.transform.position += velocity * Time.deltaTime;

                Vector3 dirCheck = destination - this.transform.position;
                float cosAngle = dirCheck.x * velocity.x + dirCheck.z * velocity.z;
                if (cosAngle < 0f)
                {
                    velocity = Vector3.zero;
                }
            }


        }   // end of movement codes

    }   // end of Update()

}   // end of class