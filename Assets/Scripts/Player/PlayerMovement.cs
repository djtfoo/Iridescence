using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 destination;
    //float getMouse0InputTimer = 0f;
    //const float mouse0InputTimer = 0.05f;

    bool doAttack = false;

    public static PlayerMovement instance;
    //RaycastHit hit;
    //RaycastTargetType raycastType;
    //GameObject raycastTarget;   // target of mouseover before clicking
    //GameObject clickTarget; // for knowing what object type the target is, e.g. can check if target enemy is within attack range

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;

        instance = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        // in conversation; cannot move
        if (DialogueManager.inDialogue)
        {
            //DialogueManager.dManager.RunDialogue();
            //DialogueManager.dManager.RunDialogue(RaycastInfo.clickTarget.GetComponent<NPCDialogue>().GetDialogue());
            return;
        }
        if (doAttack)
        {
            // cast skill when "ready"/have buffer time/is animation
            // check for collision
            RaycastInfo.clickTarget.GetComponent<EnemyData>().TakeDamage(PlayerData.attackDmg);
            doAttack = false;
        }

        // Movement
        if (!velocity.Equals(Vector3.zero))
        {
            // STORING OF CLICK TARGET - MEANING NPC/ENEMY, WHICH IS THE "END-GOAL" OF THE MOVEMENT
            if (RaycastInfo.clickTarget)    // != null
            {
                float distSquared = (destination - this.transform.position).sqrMagnitude;
                if (distSquared < PlayerData.attackRangeSquared)
                {
                    velocity = Vector3.zero;
                    doAttack = true;
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
        }
    }   // end of Update()

}   // end of class