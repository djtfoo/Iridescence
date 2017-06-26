﻿using UnityEngine;
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
    public int playerLevel;   // player's level
    [XmlElement("playerEXP")]
    public int playerEXP;   // player's exp for this level

    [XmlElement("statPoints")]
    public int statPoints;    // points gained from levelling up -- use to increase other stats

    [XmlElement("statATK")]
    public int statATK; // affects physical attack damage
    [XmlElement("statDEF")]
    public int statDEF; // affects damage reduction
    [XmlElement("statMAG")]
    public int statMAG; // affects projectile (any non-melee) damage
    [XmlElement("statSPD")]
    public int statSPD; // affects attack speed

    [XmlElement("maxHP")]
    public int maxHP;
    [XmlElement("maxMP")]
    public int maxMP;

    // Player Items
    private Potion[] potions;

    [XmlElement("blankShardsCount")]
    public int blankShardsCount;    // number of blank shards player has

    [XmlArray("crystalCountArray")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] crystalCountArray;

    // non-XML serialized variables
    private int playerEXPTotal; // total exp required for this level to level up

    private int HP;
    private int MP;

    private Dictionary<string, int> crystalCount;

    public void Init()
    {
        // transfer temp dewserializer crystalCountArray to Dictionary
        crystalCount = new Dictionary<string, int>();
        for (int i = 0; i < crystalCountArray.Length; ++i)
        {
            crystalCount.Add(crystalCountArray[i].varType, int.Parse(crystalCountArray[i].variable));
        }

        playerEXPTotal = StatsAlgorithmManager.CalculateEXPRequirement(playerLevel);

        HP = maxHP;
        MP = maxMP;
    }

    // Crystals
    public int GetCrystalCount(string elementName)
    {
        return crystalCount[elementName];
    }

    // EXP
    public int GetCurrentEXP() {    // current for this level
        return playerEXP;
    }
    public int GetCurrentEXPTotal() {   // current for this level
        return playerEXPTotal;
    }

    // HP
    public int GetHP() {   // curr HP
        return HP;
    }
    public void TakeDamage(int dmg)
    {
        SetHP(HP - dmg);
    }
    public void RestoreHP() {
        SetHP(maxHP);
    }
    private void SetHP(int newHP)
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
    public int GetMP() {   // curr MP
        return MP;
    }
    public void UseMP(int cost)
    {
        SetMP(MP - cost);
    }
    public void RestoreMP() {
        SetMP(maxMP);
    }
    private void SetMP(int newMP) {
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