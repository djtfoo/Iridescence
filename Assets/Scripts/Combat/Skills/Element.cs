using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("Element")]
public class Element {

    [XmlElement("name")]
    public string name;     // this element's name

    [XmlElement("iconFilename")]
    public string iconFilename; // filename of this element's icon sprite

    [XmlElement("colorR")]
    public float colorR;
    [XmlElement("colorG")]
    public float colorG;
    [XmlElement("colorB")]
    public float colorB;

    // each element has 3 skills
    /// skill 1: Q/A - melee
    /// skill 2: W/S - ranged
    /// skill 3: E/D - relating to personal or AoE
    [XmlArray("Skills")]
    [XmlArrayItem("Skill")]
    public Skill[] skills;  // information of this element's skills

    // non-XML variables
    public Sprite icon;     // this element's icon
    public Color color;    // this element's Color
    private bool[] unlockedSkills;  // whether user has unlocked the corresponding skills or not

    // Skill Getters
    public Sprite GetSkillIcon(int skillIdx)
    {
        return skills[skillIdx].GetSkillIcon();
    }

    public Skill GetSkillByIdx(int idx)
    {
        if (unlockedSkills[idx])
            return skills[idx];
        else
            return null;
    }
    public Skill GetSkillOne()  // Q or A skill - melee
    {
        if (unlockedSkills[0])
            return skills[0];
        else
            return null;
    }
    public Skill GetSkillTwo()  // W or S skill - ranged
    {
        if (unlockedSkills[1])
            return skills[1];
        else
            return null;
    }
    public Skill GetSkillThree()    // E or D skill - related to personal or AoE
    {
        if (unlockedSkills[2])
            return skills[2];
        else
            return null;
    }

    public void Init()  // XML already deserialized
    {
        // initialise bool array of whether skill is locked or not
        unlockedSkills = new bool[skills.Length];

        // default
        for (int i = 0; i < unlockedSkills.Length; ++i)
            unlockedSkills[i] = false;

        // create element icon sprite
        icon = Resources.Load<Sprite>("ElementIcons/" + iconFilename);

        // set color
        color = new Color(colorR / 255f, colorG / 255f, colorB / 255f);

        ///=============
        /// SKILL STUFF
        ///=============
        for (int i = 0; i < skills.Length; ++i)
        {
            // create element skills' icons
            skills[i].SetSkillIcon(Resources.Load<Sprite>("Skill Icons/" + skills[i].iconFilename));

            // skills effectVariables
            skills[i].InitDictionary(XMLSerializer<Element>.ObjectArrayItemToDictionary(skills[i].effectVariables));
        }
        
        // process generic object array for skill
    }

    /// <summary>
    ///  Set whether skills are locked or unlocked - reading from PlayerData
    /// </summary>
    public void SetUnlockSkills(int numCrystals)
    {
        for (int i = 0; i < numCrystals; ++i)
        {
            unlockedSkills[i] = true;
            if (i >= 3)
                break;
        }
    }

}

[XmlRoot("CombinedElement")]
public class CombinedElement : Element {

    // each combined element has 2 skills
    /// skill 1: R
    /// skill 2: F

}
