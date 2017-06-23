using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

public class Skill {

    [XmlAttribute("name")]
    public string name;     // this skill's name

    [XmlElement("description")]
    public string description;  // this skill's description (shown over tooltip)

    [XmlElement("iconFilename")]
    public string iconFilename; // filename of this element's icon sprite

    [XmlElement("MPCost")]
    public float MPCost;  // this skill's cost to use

    [XmlElement("attackType")]
    public SKILL_TYPE atkType;  // this skill's attack type - melee, projectile, etc

    [XmlElement("rangeValue")]
    public float rangeValue;    // this skill's attack range - where player shld stop moving to do attack

    [XmlElement("cooldownTime")]
    public float cooldownTime;  // how long this skill's cooldown is, if any (if no cooldown, is 0f)

    //public object[] effectVariables;    // this skill's variables for its effects

    // non-XML variables
    public Sprite icon;     // this skill's icon

    private bool isOnCooldown = false;  // whether this skill is currently on cooldown or not
    private float cooldownTimer = 0f;   // countdown for this skill's timer

    public bool IsOnCooldown()
    {
        return isOnCooldown;
    }
    public void SetStartCooldown()
    {
        if (cooldownTime > 0f) {
            isOnCooldown = true;
            cooldownTimer = 0f;
        }
    }
    public void UpdateCooldown()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldownTime) {
            isOnCooldown = false;
            cooldownTimer = 0f;
        }
    }

}