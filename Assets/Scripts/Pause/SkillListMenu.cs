using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ElementSkillList {

    public string name;

    public SkillsTooltip tooltipSkillQ;
    public Image skillQIcon;
    public Image skillQOverlay;    // for lock/unlock, & cooldown if any

    public SkillsTooltip tooltipSkillW;
    public Image skillWIcon;
    public Image skillWOverlay;    // for lock/unlock, & cooldown if any


    public SkillsTooltip tooltipSkillE;
    public Image skillEIcon;
    public Image skillEOverlay;    // for lock/unlock, & cooldown if any
}

public class SkillListMenu : MonoBehaviour {

    public Sprite unlockSprite;
    public Sprite lockedSprite;

    // elements
    public ElementSkillList[] elementsSkillList;
    
    // combined element
    public Image skillRIcon;
    public Image skillROverlay;    // for lock/unlock, & cooldown if any

    public Image skillFIcon;
    public Image skillFOverlay;    // for lock/unlock, & cooldown if any

    // reference to PlayerData
    private PlayerData playerData;

    // Use this for initialization
    public void InitSelf() {
        // set reference to player data
        playerData = PlayerAction.instance.GetPlayerData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitSkills()
    {
        foreach (ElementSkillList elementIcons in elementsSkillList)
        {
            Element element = playerData.GetElementData(elementIcons.name);

            Skill skillQ = element.GetSkillOne();
            elementIcons.skillQIcon.sprite = element.GetSkillIcon(0);    // set skill icon
            elementIcons.tooltipSkillQ.SetSkill(element.skills[0]);  // set Skills tooltip
            if (skillQ != null)
            {   // set normal overlay
                elementIcons.skillQOverlay.sprite = unlockSprite;
                elementIcons.skillQOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                elementIcons.skillQOverlay.sprite = lockedSprite;


            Skill skillW = element.GetSkillTwo();
            elementIcons.skillWIcon.sprite = element.GetSkillIcon(1);    // set skill icon
            elementIcons.tooltipSkillW.SetSkill(element.skills[1]);  // set Skills tooltip
            if (skillW != null)
            {   // set normal overlay
                elementIcons.skillWOverlay.sprite = unlockSprite;
                elementIcons.skillWOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                elementIcons.skillWOverlay.sprite = lockedSprite;

            Skill skillE = element.GetSkillThree();
            elementIcons.skillEIcon.sprite = element.GetSkillIcon(2);    // set skill icon
            elementIcons.tooltipSkillE.SetSkill(element.skills[2]);  // set Skills tooltip
            if (skillE != null)
            {   // set normal overlay
                elementIcons.skillEOverlay.sprite = unlockSprite;
                elementIcons.skillEOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                elementIcons.skillEOverlay.sprite = lockedSprite;
        }
    }
}
