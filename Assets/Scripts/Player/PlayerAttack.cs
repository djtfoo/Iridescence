using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

// temp
[Serializable]
public enum SKILL_TYPE
{
    [XmlEnum("Melee")]
    SKILL_MELEE,
    [XmlEnum("Projectile")]
    SKILL_FIREPROJECTILE,
    //[XmlEnum("AoE")]
    //SKILL_AOE,    // AoE around player
    [XmlEnum("Self")]
    SKILL_SELF,   // affects self or does a summon
    //SKILL_LASER
}

public class PlayerAttack : MonoBehaviour
{

    // attack variables
    private SKILL_TYPE attackType = SKILL_TYPE.SKILL_MELEE;    // current attack type
    private float currRangeSquared = 0f;    // current range value where player shld stop moving
    private string currUserAnimation = "";  // current skill's animation
    private Skill currSkill;    // a "pointer" to the current skill - the one that started the attack
    public void SetCurrSkillNull() { currSkill = null; }    // set currSkill to be null - when player left-clicks

    public bool castRangedSkill = false;    // able to cast even if not mouse over an enemy
    private Vector3 projectileDir;  // set direction projectile should move in

    private float gotoTimer;    // poll less often

    private KeyCode currKey;    // current key pressed

    // TEMP VARIABLES!!!!
    public static float meleeRangeSquared = 0.1f;  // variable to represent default melee attack
    public static float meleeDmg = 10f;    // temp damage variable

    public Transform dmgTextPrefab;

    // reference to player data
    private PlayerData playerData;

    // Getters
    public SKILL_TYPE GetCurrentAttackType()
    {
        return attackType;
    }
    public float GetCurrentRangeSquared()
    {
        return currRangeSquared;
    }
    public string GetCurrentUserAnimation()
    {
        return currUserAnimation;
    }

    // Use this for initialization
    void Start()
    {
        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();

        gotoTimer = 0f;
        currKey = KeyCode.None;
    }

    // Update is called once per frame
    void Update()
    {
        // update timer
        gotoTimer += Time.deltaTime;

        // game "pauses" if in dialogue
        if (DialogueManager.inDialogue)
            return;

        Element elementOne = playerData.GetElementOne();
        Element elementTwo = playerData.GetElementTwo();
        CombinedElement combinedElement = playerData.GetCombinedElement();

        // update element skills' cooldowns & appearances
        if (elementOne != null)
        {
            int elementCount = 0;
            foreach (Skill skill in elementOne.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                    SkillsHUD.instance.SetCooldown(elementCount, skill.GetCurrCooldownTimer(), skill.cooldownTime);
                }
                ++elementCount;
            }
        }
        if (elementTwo != null)
        {
            int elementCount = 3;
            foreach (Skill skill in elementTwo.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                    SkillsHUD.instance.SetCooldown(elementCount, skill.GetCurrCooldownTimer(), skill.cooldownTime);
                }
                ++elementCount;
            }
        }
        if (combinedElement != null)
        {
            int elementCount = 6;
            foreach (Skill skill in combinedElement.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                    //SkillsHUD.instance.SetCooldown(elementCount, skill.GetCurrCooldownTimer(), skill.cooldownTime);
                }
                ++elementCount;
            }
        }

        // check for whether skill continues - GetKey()

        // check for whether skill stops - GetKeyUp()
        /// only for some skills!

        if (currKey != KeyCode.None)
        {
            if (Input.GetKey(currKey))
            {   /// held down, continue attacking
                if (/*!PlayerAction.instance.isMovingToAttack &&*/ !PlayerAction.instance.IsAttacking())
                {  // end of attack
                    if (currSkill.atkType == SKILL_TYPE.SKILL_SELF)
                        return;

                    if (CheckSkill(currSkill))
                        goto SetMovement;
                }
                return;
            }
            else if (Input.GetKeyUp(currKey))
            {   /// player let go of key
                currKey = KeyCode.None;
                return;
            }
        }

        // CANNOT start a new attack while in midst of another attack's animation
        if (PlayerAction.instance.IsAttacking())
            return;

        // check for start of skill - GetKeyDown()
        if (Input.anyKeyDown)
        {
            /// ELEMENT ONE
            if (elementOne != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (CheckSkill(elementOne.GetSkillOne()))
                    {
                        currKey = KeyCode.Q;
                        goto SetMovement;
                    }
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (CheckSkill(elementOne.GetSkillTwo()))
                    {
                        currKey = KeyCode.W;
                        goto SetMovement;
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (CheckSkill(elementOne.GetSkillThree()))
                    {
                        currKey = KeyCode.E;
                        goto SetMovement;
                    }
                }
            }

            /// ELEMENT TWO
            if (elementTwo != null)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (CheckSkill(elementTwo.GetSkillOne()))
                    {
                        currKey = KeyCode.A;
                        goto SetMovement;
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (CheckSkill(elementTwo.GetSkillTwo()))
                    {
                        currKey = KeyCode.S;
                        goto SetMovement;
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (CheckSkill(elementTwo.GetSkillThree()))
                    {
                        currKey = KeyCode.D;
                        goto SetMovement;
                    }
                }
            }

            /// COMBINED ELEMENT
            if (combinedElement != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (CheckSkill(combinedElement.GetSkillOne()))
                    {
                        currKey = KeyCode.R;
                        goto SetMovement;
                    }
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (CheckSkill(combinedElement.GetSkillTwo()))
                    {
                        currKey = KeyCode.F;
                        goto SetMovement;
                    }
                }
            }

            // if this part is reached, it means player did not use any skill successfully; thus don't move to it
            return;
        }
        else
        {  /// no key was pressed
            return;
        }

        // goto: setting where player moves to
        SetMovement:
        {
            if (currSkill.atkType == SKILL_TYPE.SKILL_SELF)
            {
                PlayerAction.instance.SetUseSelfBuff();
                SetSkillVariables();
                PlayerAction.instance.SetMovingToAttack(true);

                return;
            }

            // store the type into RaycastInfo via getting tag/name
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

            // IF IS ENEMY, walk to enemy
            if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Enemy)
            {
                Vector2 enemyPos = RaycastInfo.clickTarget.transform.parent.position;
                SetSkillVariables();
                PlayerAction.instance.SetMoveTo(new Vector3(enemyPos.x, enemyPos.y, transform.position.z));
                projectileDir = (PlayerAction.instance.GetDestination() - transform.position).normalized;

                PlayerAction.instance.SetMovingToAttack(true);

                if (currSkill.atkType == SKILL_TYPE.SKILL_FIREPROJECTILE)
                {
                    castRangedSkill = true; // stop shifting position
                }
                else
                {
                    castRangedSkill = false;
                }

                gotoTimer = 0f;
            }
            else if (gotoTimer >= 0.5f)
            {
                //currSkill = null;

                // walk to a spot and attack there -- same as how one would attack with shift

                PlayerAction.instance.SetMovingToAttack(true);

                if (!castRangedSkill)   // either not casting ranged, or first frame of attempting to cast a ranged
                {
                    if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Terrain)
                    {
                        PlayerAction.instance.SetMoveTo(new Vector3(RaycastInfo.hit2D.point.x, RaycastInfo.hit2D.point.y, PlayerAction.instance.transform.position.z));
                        gotoTimer = 0f;
                    }
                }

                if (currSkill.atkType == SKILL_TYPE.SKILL_FIREPROJECTILE)
                {
                    castRangedSkill = true; // stop shifting position & set to be able to cast once movement ends
                    projectileDir = (PlayerAction.instance.GetDestination() - PlayerAction.instance.transform.position).normalized;
                    SetSkillVariables();
                }
                else
                {
                    castRangedSkill = false;
                }
            }
        }

    }

    /// <summary>
    ///  Checks whether skill can be used or not. If yes, set it to be used
    /// </summary>
    /// <param name="skill"> Skill to use </param>
    /// <returns> true if skill used; false if not used </returns>
    public bool CheckSkill(Skill skill)
    {
        // check whether skill is locked
        if (skill == null)
            return false;

        // check cooldown
        if (skill.IsOnCooldown()) {
            return false;
        }

        // check cost
        switch (skill.costType)
        {
            case COST_TYPE.COST_MP:
                // check enough MP or not
                if (skill.MPCost > playerData.GetMP()) {
                    return false;
                }
                break;

            case COST_TYPE.COST_COMBINED1:
                if (!playerData.CheckUseCombinedSkill1())   // check against Elemental Charge Bar
                    return false;
                break;

            case COST_TYPE.COST_COMBINED2:
                if (!playerData.CheckUseCombinedSkill2())   // check against Elemental Charge Bar
                    return false;
                break;
        }

        // set the skill
        currSkill = skill;

        return true;
    }

    private void SetSkillVariables()
    {
        // set attack variables
        attackType = currSkill.atkType;
        currRangeSquared = currSkill.rangeValue;
        currUserAnimation = currSkill.userAnimation;
    }

    /// <summary>
    ///  Use skill when user reaches 
    /// </summary>
    public void UseSkill()
    {
        if (currSkill != null)
        {
            /// use MP
            switch (currSkill.costType)
            {
                case COST_TYPE.COST_MP:
                    playerData.UseMP(currSkill.MPCost); // shld be done at start of skill
                    break;

                case COST_TYPE.COST_COMBINED1:
                    playerData.UseCombinedSkill1();
                    break;

                case COST_TYPE.COST_COMBINED2:
                    playerData.UseCombinedSkill2();
                    break;
            }

            /// set cooldown (if any)
            currSkill.SetStartCooldown();   // shld be done at end of skill
        }

        /// skill effect
        switch (attackType)
        {
            case SKILL_TYPE.SKILL_MELEE:
                {
                    // DAMAGE
                    float skillDmg;
                    if (currSkill == null)  // regular physical melee
                        skillDmg = meleeDmg;
                    else if (!currSkill.HasKey("Damage"))
                        goto endOfDamage;
                    else
                        skillDmg = float.Parse(currSkill.GetValue("Damage")) * meleeDmg;  // temp; shld be elemental stat

                    /// calculate critical hit chance
                    int rand = UnityEngine.Random.Range(0, 100);
                    float criticalHitChance = playerData.criticalHitChance;

                    /// increase critical hit chance if skill has increased chance
                    if (currSkill != null)
                    {
                        if (currSkill.HasKey("CriticalChanceIncrease"))
                            criticalHitChance += int.Parse(currSkill.GetValue("CriticalChanceIncrease"));
                    }

                    if (rand < criticalHitChance)    // critical hit success
                        skillDmg *= playerData.criticalHitMultiplier;

                    float damageDealt = AlgorithmManager.DamageCalculation(skillDmg, RaycastInfo.clickTarget.GetComponent<EnemyData>().defense);

                    RaycastInfo.clickTarget.SendMessage("TakeDamage", damageDealt);

                    //CreateDmgText((int)damageDealt, 0.5f * (transform.position + RaycastInfo.clickTarget.transform.position));  // position is mid-point between enemy and player
                    CreateDmgText((int)damageDealt, RaycastInfo.clickTarget.transform.position);    // on top of enemy

            endOfDamage:
                    // attach Modifier components if any
                    if (currSkill != null)
                    {
                        // Self
                        if (currSkill.HasKey("ComponentSelf"))
                        {
                            AttachModifier.SetModifierEffect(PlayerAction.instance.gameObject,
                                currSkill.GetValue("ComponentSelf"),
                                float.Parse(currSkill.GetValue("Duration")), float.Parse(currSkill.GetValue("EffectValue")));
                        }

                        // Enemy
                        if (currSkill.HasKey("ComponentEnemy"))
                        {
                            AttachModifier.SetModifierEffect(RaycastInfo.clickTarget,
                                currSkill.GetValue("ComponentEnemy"),
                                float.Parse(currSkill.GetValue("Duration")), float.Parse(currSkill.GetValue("EffectValue")));
                        }
                    }

                    // Play Skill SFX
                    if (currSkill != null) {
                        AudioManager.instance.PlaySFX(currSkill.name);
                    }

                    // Create melee skill animation
                    CreateMeleeSkillAnimation();

                    // Increase elemental charge bar
                    if (currSkill != null)
                    {
                        if (playerData.currElement1 == currSkill.GetElementType())
                            playerData.IncreaseElementalChargeBar1(10);
                        else if (playerData.currElement2 == currSkill.GetElementType())
                            playerData.IncreaseElementalChargeBar2(10);
                    }
                }
                break;

            case SKILL_TYPE.SKILL_FIREPROJECTILE:
                /// create projectile
                //SpawnProjectile(RaycastInfo.clickTarget.transform.parent.position);
                SpawnProjectile(projectileDir);
                break;

            case SKILL_TYPE.SKILL_SELF:
                /// add buff to self
                if (currSkill.HasKey("ComponentSelf"))
                {
                    AttachModifier.SetModifierEffect(PlayerAction.instance.gameObject,
                        currSkill.GetValue("ComponentSelf"),
                        float.Parse(currSkill.GetValue("Duration")), float.Parse(currSkill.GetValue("EffectValue")));
                }

                // Play Skill SFX
                AudioManager.instance.PlaySFX(currSkill.name);

                CreateSelfSkillAnimation();
                break;
        }

        castRangedSkill = false;
        //currSkill = null;   // remove pointer to skill - shld be done at end of skill
    }

    public void CreateDmgText(int dmg, Vector3 position)
    {
        Transform dmgText = (Transform)Instantiate(dmgTextPrefab, Vector3.zero, Quaternion.identity);
        dmgText.localScale = new Vector3(1f, 1f, 1f);
        dmgText.localPosition = position;
        dmgText.GetComponent<DamageText>().SetDamageText(dmg);
    }

    /// <summary>
    ///  Function to set player using a regular non-skill melee attack
    /// </summary>
    public void SetRegularMeleeAttack()
    {
        attackType = SKILL_TYPE.SKILL_MELEE;
        currRangeSquared = meleeRangeSquared;
        currUserAnimation = "Melee";
    }

    /// <summary>
    ///  Create melee skill animation when attack hits
    /// </summary>
    private void CreateMeleeSkillAnimation()
    {
        if (currSkill != null)
        {
            GameObject skillAnimation = Resources.Load<GameObject>("ParticleAnimations/" + currSkill.name);
            GameObject instantiated = Instantiate(skillAnimation);
            //instantiated.transform.position = 0.5f * (transform.position + RaycastInfo.clickTarget.transform.position);   // mid-point

            instantiated.transform.position = new Vector3(RaycastInfo.clickTarget.transform.position.x, RaycastInfo.clickTarget.transform.position.y - 0.2f,
                RaycastInfo.clickTarget.transform.position.z);  // slightly lower than enemy's position
        }
    }

    /// <summary>
    ///  Create self skill animation when being cast
    /// </summary>
    private void CreateSelfSkillAnimation()
    {
        if (currSkill != null)
        {
            GameObject skillAnimation = Resources.Load<GameObject>("ParticleAnimations/" + currSkill.name);
            GameObject instantiated = Instantiate(skillAnimation);
            instantiated.transform.position = transform.position;
            instantiated.GetComponent<ParticleAnimationMovement>().SetParent(this.transform);
        }
    }

    //private void SpawnProjectile(Vector3 targetPos)
    //{
    //    GameObject projectile = Resources.Load<GameObject>("ParticleAnimations/" + currSkill.name);
    //    GameObject instantiated = Instantiate(projectile);
    //    instantiated.transform.position = PlayerAction.instance.transform.position;   // slightly forward
    //    instantiated.GetComponent<Projectile>().SetVelocity((targetPos - transform.position).normalized);
    //}

    private void SpawnProjectile(Vector3 dir)
    {
        GameObject projectile = Resources.Load<GameObject>("ParticleAnimations/Projectiles/" + currSkill.name);
        GameObject instantiated = Instantiate(projectile);
        instantiated.transform.position = transform.position;   // slightly forward
        instantiated.GetComponent<Projectile>().SetVelocity(dir);
    }

}