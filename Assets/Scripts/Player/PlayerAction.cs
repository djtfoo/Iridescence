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

    // related to sequence in doing an attack
    private bool doAttack = false;  // if true, player's animation will start
    private bool isAttacking = false;   // if true, player is in midst of attack animation -- NO ATTACK CANCELLATION ALLOWED!
    public bool IsAttacking() { return isAttacking; }
    public void SetStopAttacking() { isAttacking = false; }

    private bool movedThisFrame = false;
    public bool IsMovingThisFrame() { return movedThisFrame; }
    public void SetEndMovingThisFrame() { movedThisFrame = false; }

    // Potion usage
    private float countdownTimer = 0f;  // for using potion
    private float bufferTime = 0.5f;    // how long the player can't use potion after using 1 potion

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
        
        // init player data
        playerData.Init();

        // set potion buffer time
        bufferTime = 0.5f;
        countdownTimer = bufferTime;
    }

    // Use this for initialization
    void Start () {
        RaycastInfo.raycastType = RaycastTargetType.Raycast_NIL;
        RaycastInfo.raycastTarget = null;
        RaycastInfo.clickTarget = null;

        playerAttack = GetComponent<PlayerAttack>();

        destinationMarker = (GameObject)Instantiate(destinationMarkerPrefab, destinationMarkerPrefab.transform.position, Quaternion.identity);
        destinationMarker.SetActive(false);

        // SETTING OF CURR ELEMENTS
        playerData.SetElementReference(playerData.currElement1, "One");
        playerData.SetElementReference(playerData.currElement2, "Two");
    }

    public bool UsePotion(int slotIdx)
    {
        string potionName = playerData.equippedPotions[slotIdx];

        if (ItemInfoManager.instance.GetPotion(potionName).IsOnCooldown())
            return false;

        if (playerData.GetPotionQuantity(potionName) == 0)  // not enough potions
            return false;

        // apply potion effect
        ItemInfoManager.instance.GetPotion(potionName).UsePotion();

        // change quantity
        playerData.ChangePotionQuantity(potionName, -1);

        // set quantity text
        for (int i = 0; i < 5; ++i)
        {
            if (potionName == playerData.equippedPotions[i])
                PotionsHUD.instance.SetPotionQuantity(playerData.GetPotionQuantity(potionName), i);
        }

        // set cooldown image fill
        PotionsHUD.instance.potionSprites[slotIdx].fillAmount = 0f;
        countdownTimer = 0f;    // set countdown timer
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        // in conversation; cannot move or update
        if (DialogueManager.inDialogue)
        {
            //DialogueManager.dManager.RunDialogue();
            //DialogueManager.dManager.RunDialogue(RaycastInfo.clickTarget.GetComponent<NPCDialogue>().GetDialogue());
            return;
        }

        //========
        // POTION
        //========
        /// update potion cooldown
        for (int i = 0; i < 5; ++i)
        {
            if (playerData.equippedPotions[i] != "")
                ItemInfoManager.instance.GetPotion(playerData.equippedPotions[i]).ResetUpdateThisFrame();
        }

        /// potion usage
        bool potionUsed = false;    // a potion was used this frame - cannot use >1 potion in 1 frame
        for (KeyCode i = KeyCode.Alpha1; i <= KeyCode.Alpha5; ++i)
        {
            int slotIdx = i - KeyCode.Alpha1;
            string potName = playerData.equippedPotions[slotIdx];
            if (potName != "")
            {
                Potion potion = ItemInfoManager.instance.GetPotion(potName);
                if (potion.IsOnCooldown())
                {
                    potion.UpdateCooldown();
                }
                else if (Input.GetKeyDown(i) && !potionUsed)
                {
                    if (UsePotion(slotIdx))
                        potionUsed = true;
                }
            }
        }

        if (doAttack)
        {
            // cast skill when "ready"/have buffer time/is animation
            // check for collision
            
            // temp
            switch (playerAttack.GetCurrentAttackType())
            {
                case SKILL_TYPE.SKILL_MELEE:
                    //RaycastInfo.clickTarget.SendMessage("TakeDamage", PlayerAttack.meleeDmg);
                    transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation(playerAttack.GetCurrentUserAnimation(), false);
                    break;

                case SKILL_TYPE.SKILL_FIREPROJECTILE:
                    transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation(playerAttack.GetCurrentUserAnimation(), false);
                    break;
            }
            doAttack = false;
            isAttacking = true;
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
                    // set checkpoint & heal
                    RaycastInfo.clickTarget.GetComponent<Waypoint>().InteractWithWaypoint();

                    // open dialogue to traverse waypoints

                    SetPathComplete();
                    goto endOfVelocityMovement;
                }
                else if (RaycastInfo.raycastType == RaycastTargetType.Raycast_TransitionPortal && distSquared < 0.1f)
                {
                    // transit to next area
                    movedThisFrame = false; // so movement boost won't fuck this up
                    RaycastInfo.clickTarget.GetComponent<Portal>().GoToNextLevel();

                    SetPathComplete();
                    goto endOfVelocityMovement;
                }
            }

            // check for reach waypoint destination
            Vector3 dirCheck = pathWaypoints[0] - this.transform.position;
            // check by knowing whether "overshot" the path
            float cosAngle = dirCheck.x * velocity.x + dirCheck.y * velocity.y;
            if (cosAngle < 0f) {
                ReachedWaypoint();
            }

            // move because still got somewhere to move to
            if (pathWaypoints.Count != 0)
            {
                this.transform.position += velocity * Time.deltaTime;
                movedThisFrame = true;
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

        this.transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation("Idle", true);

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
            this.transform.GetChild(0).GetComponent<SpriteAnimator>().ChangeAnimation("Walk", true);

        SetVelocity((pathWaypoints[0] - transform.position).normalized);

        if (RaycastInfo.clickTarget == null || RaycastInfo.raycastType == RaycastTargetType.Raycast_TransitionPortal)
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

    public void InstantiateLevelUpParticles()
    {
        GameObject levelupParticles = Resources.Load<GameObject>("ParticleAnimations/Level Up");
        GameObject instantiated = Instantiate(levelupParticles);
        instantiated.transform.position = transform.position;
    }

}   // end of class