﻿using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {

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
                }
                ++elementCount;
            }
        }

        // CANNOT attack while in midst of another attack's animation
        if (PlayerAction.instance.IsAttacking())
            return;

        // check for whether skill continues - GetKey()

        // check for whether skill stops - GetKeyUp()
        /// only for some skills!

        // check for start of skill - GetKeyDown()
        if (Input.anyKeyDown)
        {
            /// ELEMENT ONE
            if (elementOne != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (CheckSkill(elementOne.GetSkillOne()))
                        goto SetMovement;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (CheckSkill(elementOne.GetSkillTwo()))
                        goto SetMovement;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (CheckSkill(elementOne.GetSkillThree()))
                        goto SetMovement;
                }
            }

            /// ELEMENT TWO
            if (elementTwo != null)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (CheckSkill(elementTwo.GetSkillOne()))
                        goto SetMovement;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (CheckSkill(elementTwo.GetSkillTwo()))
                        goto SetMovement;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (CheckSkill(elementTwo.GetSkillThree()))
                        goto SetMovement;
                }
            }

            /// COMBINED ELEMENT
            if (combinedElement != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (CheckSkill(combinedElement.GetSkillOne()))
                        goto SetMovement;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (CheckSkill(combinedElement.GetSkillTwo()))
                        goto SetMovement;
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
            // store the type into RaycastInfo via getting tag/name
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();

            // IF IS ENEMY, walk to enemy
            if (RaycastInfo.raycastType == RaycastTargetType.Raycast_Enemy)
            {
                Vector2 enemyPos = RaycastInfo.clickTarget.transform.parent.position;
                SetSkillVariables();
                PlayerAction.instance.SetMoveTo(new Vector3(enemyPos.x, enemyPos.y, PlayerAction.instance.transform.position.z));
            }
            else
            {
                // walk to a spot and attack there -- same as how one would attack with shift
                /// set destination as player's own spot
                currSkill = null;
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
        if (skill.IsOnCooldown())
            return false;

        // check enough MP or not
        if (skill.MPCost > playerData.GetMP())
            return false;

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
            playerData.UseMP(currSkill.MPCost); // shld be done at start of skill

            /// set cooldown (if any)
            currSkill.SetStartCooldown();   // shld be done at end of skill
        }

        /// skill effect
        switch (attackType)
        {
            case SKILL_TYPE.SKILL_MELEE:
                {
                    /// deal damage
                    float skillDmg;
                    if (currSkill == null)  // regular physical melee
                        skillDmg = meleeDmg;
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

                    /// attach modifier components if any
                    if (currSkill != null)
                    {
                        if (currSkill.HasKey("ComponentSelf"))
                        {
                            AttachModifier.SetModifierEffect(PlayerAction.instance.gameObject,
                                currSkill.GetValue("ComponentSelf"),
                                float.Parse(currSkill.GetValue("Duration")), float.Parse(currSkill.GetValue("EffectValue")));
                        }
                    }

                    /// Create melee skill animation
                    CreateMeleeSkillAnimation();

                    CreateDmgText((int)damageDealt);
                }
                break;

            case SKILL_TYPE.SKILL_FIREPROJECTILE:
                /// create projectile
                SpawnProjectile(RaycastInfo.clickTarget.transform.parent.position);
                break;

            case SKILL_TYPE.SKILL_SELF:
                /// add buff to self
                break;
        }

        currSkill = null;   // remove pointer to skill - shld be done at end of skill
    }

    private void CreateDmgText(int dmg)
    {
        Transform dmgText = (Transform)Instantiate(dmgTextPrefab, Vector3.zero, Quaternion.identity);
        dmgText.localScale = new Vector3(1f, 1f, 1f);
        dmgText.localPosition = 0.5f * (PlayerAction.instance.transform.position + RaycastInfo.clickTarget.transform.position);   // mid-point
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
            instantiated.transform.position = 0.5f * (PlayerAction.instance.transform.position + RaycastInfo.clickTarget.transform.position);   // mid-point
        }
    }

    private void SpawnProjectile(Vector3 targetPos)
    {
        GameObject projectile = Resources.Load<GameObject>("ParticleAnimations/" + currSkill.name);
        GameObject instantiated = Instantiate(projectile);
        instantiated.transform.position = PlayerAction.instance.transform.position;   // slightly forward
        instantiated.GetComponent<Projectile>().SetVelocity((targetPos - transform.position).normalized);
    }

}
