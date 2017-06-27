using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour {

    public GameObject destinationMarkerPrefab;
    GameObject destinationMarker;

    Vector3 velocity;
    //Vector3 destination;
    List<Vector3> pathWaypoints = new List<Vector3>();  // storing the waypoints of a path
    //float getMouse0InputTimer = 0f;
    //const float mouse0InputTimer = 0.05f;

    private PlayerAttack playerAttack;
    private PlayerData playerData;

    bool doAttack = false;

    public static PlayerAction instance;
    //RaycastHit hit;
    //RaycastTargetType raycastType;
    //GameObject raycastTarget;   // target of mouseover before clicking
    //GameObject clickTarget; // for knowing what object type the target is, e.g. can check if target enemy is within attack range

    // set the "current attack" when player presses a key; else it's default attack

    private void Awake()
    {
        instance = GetComponent<PlayerAction>();

        // get save data XML file
        TextAsset savedataXML = Resources.Load<TextAsset>("SaveData/playerdata");   // temp

        // deserialize XML file
        playerData = XMLSerializer<PlayerData>.DeserializeXMLFile(savedataXML);
    }

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;

        playerAttack = GetComponent<PlayerAttack>();

        destinationMarker = (GameObject)Instantiate(destinationMarkerPrefab, destinationMarkerPrefab.transform.position, Quaternion.identity);
        destinationMarker.SetActive(false);

        // init player data
        playerData.Init();
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
            
            // temp
            switch (playerAttack.GetCurrentAttackType())
            {
                case SKILL_TYPE.SKILL_MELEE:
                    //RaycastInfo.clickTarget.GetComponent<EnemyData>().TakeDamage(attack.meleeDmg);
                    RaycastInfo.clickTarget.SendMessage("TakeDamage", PlayerAttack.meleeDmg);
                    break;

                case SKILL_TYPE.SKILL_FIREPROJECTILE:
                    playerAttack.SpawnProjectile(RaycastInfo.clickTarget.transform.parent.position);
                    //RaycastInfo.clickTarget.GetComponent<EnemyData>().TakeDamage(PlayerData.attackDmg);
                    break;
            }
            doAttack = false;
        }

        // Movement
        if (!velocity.Equals(Vector3.zero))
        {
            // STORING OF CLICK TARGET - MEANING NPC/ENEMY, WHICH IS THE "END-GOAL" OF THE MOVEMENT
            if (RaycastInfo.clickTarget)    // != null
            {
                float distSquared = (pathWaypoints[0] - this.transform.position).sqrMagnitude;
                // attack
                if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Enemy)
                {
                    if (distSquared < playerAttack.GetCurrentRangeSquared())
                    {
                        doAttack = true;
                        //velocity = Vector3.zero;
                        SetPathComplete();
                        // UseSkill() here
                        //clickTarget = null;   // don't null so you can attack that target?
                        goto endOfVelocityMovement;
                    }
                }
                // enter dialogue
                else if (RaycastInfo.raycastType == RaycastTargetType.Raycast_NPC && distSquared < PlayerData.converseRangeSquared)
                {
                    SetStartDialogue(RaycastInfo.clickTarget);
                    //velocity = Vector3.zero;
                    SetPathComplete();
                    goto endOfVelocityMovement;
                }
                else if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Waypoint && distSquared < PlayerData.converseRangeSquared)
                {
                    // waypoint heal
                    RaycastInfo.clickTarget.GetComponent<Waypoint>().HealPlayer();

                    // open dialogue to traverse waypoints

                    SetPathComplete();
                    goto endOfVelocityMovement;
                }
                else if (RaycastInfo.raycastType == RaycastTargetType.Raycast_TransitionPortal && distSquared < 0.1f)
                {
                    // transit to next area
                    InstantiateLevel.instance.LoadLevel(RaycastInfo.clickTarget.GetComponent<Portal>().connectedPrefabName);

                    SetPathComplete();
                    goto endOfVelocityMovement;
                }
            }

            // check for reach waypoint destination
            Vector3 dirCheck = pathWaypoints[0] - this.transform.position;
            // check by knowing whether "overshot" the path
            float cosAngle = dirCheck.x * velocity.x + dirCheck.z * velocity.z;
            if (cosAngle < 0f) {
                ReachedWaypoint();
            }

            // move because still got somewhere to move to
            if (pathWaypoints.Count != 0)
            {
                this.transform.position += velocity * Time.deltaTime;
            }
        }

        endOfVelocityMovement:;

    }   // end of Update()

    private void ReachedWaypoint()
    {
        pathWaypoints.RemoveAt(0);
        if (pathWaypoints.Count == 0)
            SetPathComplete();
        else
            SetVelocity((pathWaypoints[0] - transform.position).normalized);
    }
    private void SetPathComplete()
    {
        velocity = Vector3.zero;
        pathWaypoints.Clear();

        this.transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation("Idle");

        destinationMarker.SetActive(false);
    }

    private void CalculateDirection()
    {
        // get direction
        Vector2 playerVel = new Vector2(velocity.x, velocity.y);
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(playerVel, Vector2.down) / velocity.magnitude);  // Vector3.down is Vector3(0, -1, 0)
        if (velocity.x > 0f)
            angle = 360f - angle;
        angle += 22.5f;
        if (angle >= 360f)
            angle -= 360f;
        this.transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeDirection((int)(angle) / 45);
    }

    private void SetStartDialogue(GameObject NPC)
    {
        DialogueManager.dManager.InitDialogue(NPC);
        NPC.GetComponent<NPCEventHandler>().speechBubble.GetComponent<SpriteAnimator>().SetFreezeAnimation(true);
    }

    public void SetMoveTo(Vector3 destination)
    {
        // empty path
        pathWaypoints.Clear();
        // calculate path
        this.GetComponent<NewPathfinder>().CalculatePath(destination, ref pathWaypoints);
        //SetDestination(destination);

        // change to walk animation
        if (velocity.Equals(Vector3.zero))
            this.transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation("Walk");

        SetVelocity((pathWaypoints[0] - transform.position).normalized);

        if (RaycastInfo.clickTarget == null)
        {
            destinationMarker.SetActive(true);
            destinationMarker.transform.position = new Vector3(destination.x, destination.y, 1f);
        }

        //SetDestination(destination);
        //SetVelocity((destination - transform.position).normalized);
    }

    // public void SetMoveBy(Vector3 velocity, float duration)

    // Getters
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    public PlayerAttack GetAttackScript()
    {
        return playerAttack;
    }
    public PlayerData GetPlayerData()
    {
        return playerData;
    }
    //public Vector3 GetDestination()
    //{
    //    return destination;
    //}

    // Setters
    public void SetVelocity(Vector3 newVel)
    {
        velocity = newVel;
        CalculateDirection();
    }
    public void SetVelocityZero()
    {
        SetVelocity(Vector3.zero);
    }
    //public void SetDestination(Vector3 newDest)
    //{
    //    destination = newDest;
    //}

}   // end of class