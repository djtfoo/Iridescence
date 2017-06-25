using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

// class that stores currently saved player information. for serializing & deserializing
[XmlRoot("PlayerData")]
public class PlayerData {

    // shld have a "pointer" to the current skill - the one that was clicked to do the attack


    // CONST VALUE
    public const float converseRangeSquared = 0.5f;  // distance between player & NPC/Waypoint to start dialogue

    // Player Stats
    [XmlElement("playerLevel")]
    public float playerLevel;   // player's level

    [XmlElement("statPoints")]
    public float statPoints;    // points gained from levelling up -- use to increase other stats

    [XmlElement("statATK")]
    public int statATK; // affects physical attack damage
    [XmlElement("statDEF")]
    public int statDEF; // affects damage reduction
    [XmlElement("statMAG")]
    public int statMAG; // affects projectile (any non-melee) damage
    [XmlElement("statSPD")]
    public int statSPD; // affects attack speed

    [XmlElement("maxHP")]
    public float maxHP;
    [XmlElement("maxMP")]
    public float maxMP;

    // Player Items
    private Potion[] potions;

    [XmlElement("blankShardsCount")]
    public int blankShardsCount;    // number of blank shards player has

    [XmlArray("crystalCountArray")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] crystalCountArray;

    // non-XML serialized variables
    private float HP;
    private float MP;

    private Dictionary<string, int> crystalCount;

    public void Init()
    {
        // transfer temp dewserializer crystalCountArray to Dictionary
        crystalCount = new Dictionary<string, int>();
        for (int i = 0; i < crystalCountArray.Length; ++i)
        {
            crystalCount.Add(crystalCountArray[i].varType, int.Parse(crystalCountArray[i].variable));
        }

        HP = maxHP;
        MP = maxMP;
    }

    // HP
    public float GetHP() {   // curr HP
        return HP;
    }
    public float GetMaxHP() {
        return maxHP;
    }
    public void TakeDamage(float dmg)
    {
        SetHP(HP - dmg);
    }
    public void RestoreHP() {
        SetHP(maxHP);
    }
    private void SetHP(float newHP)
    {
        HP = newHP;
        //if (newHP <= 0)
            // lose game

        // edit HP bar
        GameHUD.instance.HPChanged(HP, maxHP);
    }
    public bool IsAtMaxHP() {
        return HP == maxHP;
    }
    
    // MP
    public float GetMP() {   // curr MP
        return MP;
    }
    public float GetMaxMP() {
        return maxMP;
    }
    public void UseMP(float cost)
    {
        SetMP(MP - cost);
    }
    public void RestoreMP() {
        SetMP(maxMP);
    }
    private void SetMP(float newMP) {
        MP = newMP;

        if (newMP <= 0)
            MP = 0;

        // edit MP bar
        GameHUD.instance.MPChanged(MP, maxMP);
    }
    public bool IsAtMaxMP() {
        return MP == maxMP;
    }


}