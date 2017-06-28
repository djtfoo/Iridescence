using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

// class that stores currently saved player information. for serializing & deserializing
[XmlRoot("PlayerData")]
public class PlayerData {

    // XML files - load from Resources
    private TextAsset[] elementXMLFiles;    // information of player's elements

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

    [XmlElement("elementOne")]
    public string currElement1;
    [XmlElement("elementTwo")]
    public string currElement2;

    [XmlArray("equippedPotions")]
    [XmlArrayItem("potion")]
    public string[] equippedPotions; // user's potion slots (5 slots total)

    // Player Items
    [XmlElement("blankShardsCount")]
    public int blankShardsCount;    // number of blank shards player has

    [XmlArray("crystalCountArray")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] crystalCountArray; // number of Iris Fragments player has (max. 3)

    [XmlArray("potionsCountArray")]
    [XmlArrayItem("ObjectArrayItem")]
    public ObjectArrayItem[] potionsCountArray; // number of potions player has (max. 99)

    // non-XML serialized variables
    private int playerEXPTotal; // total exp required for this level to level up

    private int HP;
    private int MP;

    // Player items
    private Dictionary<string, int> crystalCount;   // number of Iris Fragments player has (max. 3)
    private Dictionary<string, int> potionsQuantity;    // quantity of each potion that player has (max. 99)

    // element information
    private Dictionary<string, Element> elements;   // player's elements, read from XML
    private Dictionary<string, CombinedElement> combinedElements;   // player's combined elements, read from XML

    // currently equipped elements
    private Element currElementOne; // currently equipped 1st element
    private Element currElementTwo; // currently equipped 2nd element
    private CombinedElement currCombinedElement;    // current combined element

    public void Init()
    {
        playerEXPTotal = StatsAlgorithmManager.CalculateEXPRequirement(playerLevel);

        HP = maxHP;
        MP = maxMP;

        // transfer temp deserializer crystalCountArray to Dictionary
        crystalCount = new Dictionary<string, int>();
        for (int i = 0; i < crystalCountArray.Length; ++i)
        {
            crystalCount.Add(crystalCountArray[i].varType, int.Parse(crystalCountArray[i].variable));
        }

        // transfer temp deserializer potionsCountArray to Dictionary
        potionsQuantity = new Dictionary<string, int>();
        for (int i = 0; i < potionsCountArray.Length; ++i)
        {
            potionsQuantity.Add(potionsCountArray[i].varType, int.Parse(potionsCountArray[i].variable));
        }

        // init elements
        elementXMLFiles = Resources.LoadAll<TextAsset>("ElementXML");

        elements = new Dictionary<string, Element>();
        combinedElements = new Dictionary<string, CombinedElement>();

        for (int i = 0; i < elementXMLFiles.Length; ++i)
        {
            // deserialize XML
            Element tempElement = XMLSerializer<Element>.DeserializeXMLFile(elementXMLFiles[i]);

            tempElement.Init();

            // add to dictionary
            elements.Add(tempElement.name, tempElement);
        }

        // set whether skills are unlocked or not
        foreach (string key in elements.Keys)
        {
            elements[key].SetUnlockSkills(GetCrystalCount(key));
        }
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

    // Potions
    public int GetPotionQuantity(string potionName)
    {
        return potionsQuantity[potionName];
    }

    /// <summary>
    ///  Set Element to slot one, slot two, or combined
    /// </summary>
    public void SetPotionToSlot(string potionName, int slotIdx)
    {
        equippedPotions[slotIdx] = potionName;

        // set to PotionsHUD
        PotionsHUD.instance.SetPotion(potionName, slotIdx);
    }

    // Element
    public Element GetElementData(string key) // get element via key
    {
        return elements[key];
    }

    public Element GetElementOne()
    {
        return currElementOne;
    }
    public Element GetElementTwo()
    {
        return currElementTwo;
    }
    public CombinedElement GetCombinedElement()
    {
        return currCombinedElement;
    }

    /// <summary>
    ///  Set Element to slot one, slot two, or combined
    /// </summary>
    public void SetElementReference(string elementKey, string slot)
    {
        switch (slot)
        {
            case "One":
                if (elementKey == "")
                    currElementOne = null;
                else
                    currElementOne = elements[elementKey];
                SkillsHUD.instance.SetElementOne(currElementOne);
                // set skills icons

                break;
            case "Two":
                if (elementKey == "")
                    currElementTwo = null;
                else
                    currElementTwo = elements[elementKey];
                SkillsHUD.instance.SetElementTwo(currElementTwo);
                // set skills icons
                break;
            case "Combined":
                //currCombinedElement = elements[elementKey];
                //SkillsHUD.instance.SetCombinedElementIcon(currCombinedElement.icon);
                break;
            default:
                break;
        }
    }


}