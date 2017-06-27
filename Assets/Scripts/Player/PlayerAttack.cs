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
    [XmlEnum("AoE")]
    SKILL_AOE,    // AoE around player
    [XmlEnum("Self")]
    SKILL_SELF,   // affects self or does a summon
    //SKILL_LASER
}

public class PlayerAttack : MonoBehaviour {

    // attack variables
    private SKILL_TYPE attackType = SKILL_TYPE.SKILL_MELEE;    // current attack type
    private  float currRangeSquared = 0f;     // current range value where player shld stop moving

    // TEMP VARIABLES!!!!
    public static float meleeRangeSquared = 0.5f;  // temp variable to represent weapon
    public static float meleeDmg = 10f;    // temp damage variable
    public static float rangedRangeSquared = 1.5f;  // temp variable to represent ranged attack range
    public GameObject fireProjectile;   // temp projectile to launch


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

    // Use this for initialization
    void Start () {
        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();
    }
	
	// Update is called once per frame
	void Update () {

        return;

        Element elementOne = playerData.GetElementOne();
        Element elementTwo = playerData.GetElementTwo();
        CombinedElement combinedElement = playerData.GetCombinedElement();

        // update element skills' cooldowns & appearances
        if (elementOne != null) {
            foreach (Skill skill in elementOne.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                }
            }
        }
        if (elementTwo != null) {
            foreach (Skill skill in elementTwo.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                }
            }
        }
        if (combinedElement != null) {
            foreach (Skill skill in combinedElement.skills)
            {
                if (skill.IsOnCooldown())
                {
                    skill.UpdateCooldown();
                }
            }
        }

        // CANNOT attack if in dialogue
        if (DialogueManager.inDialogue)
            return;

        // check for whether skill continues - GetKey()

        // check for whether skill stops - GetKeyUp()
        /// only for some skills!

        // check for start of skill - GetKeyDown()
        if (Input.anyKeyDown)
        {
            /// ELEMENT ONE
            if (elementOne != null) {
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
            if (elementTwo != null) {
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
            if (combinedElement != null) {
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
        }
        else {  /// not key was pressed
            // if this part is reached, it means player did not use any skill; thus don't move to it
            return;
        }

        // goto: setting where player moves to
SetMovement:
        {
            // store the type into RaycastInfo via getting tag/name
            RaycastInfo.clickTarget = RaycastInfo.GetRaycastTarget2D();
            
            // IF IS ENEMY, walk to enemy
            Vector2 enemyPos = RaycastInfo.clickTarget.transform.parent.position;
            //this.attackType = ATK_TYPE.ATK_FIREPROJECTILE;  // temp
            //this.currRangeSquared = rangedRangeSquared; // temp
            PlayerAction.instance.SetMoveTo(new Vector3(enemyPos.x, enemyPos.y, PlayerAction.instance.transform.position.z));
        }


    }

    public void SpawnProjectile(Vector3 targetPos)
    {
        GameObject projectile = (GameObject)Instantiate(fireProjectile, this.transform.GetChild(0).position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetVelocity((targetPos - this.transform.position).normalized);
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
            // USE MELEE ATTACK INSTEAD
            return false;

        // check enough MP or not
        if (skill.MPCost > playerData.GetMP())
            // USE MELEE ATTACK INSTEAD
            return false;

        // else, set to use skill
        /// set attack variables
        attackType = skill.atkType;
        currRangeSquared = skill.rangeValue;

        return true;
    }

    /// <summary>
    ///  Use skill when user reaches 
    /// </summary>
    /// <param name="skill"> Skill to use </param>
    public void UseSkill(Skill skill)
    {
        /// use MP
        playerData.UseMP(skill.MPCost);

        /// set cooldown (if any)
        skill.SetStartCooldown();

    }

}
