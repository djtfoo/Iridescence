using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public struct ElementalGem
{
    public string elementName;
    public Sprite gemSprite;
}

public class ElementsMenu : MonoBehaviour {

    // Gems
    public ElementalGem[] elementalGems;   // for initialising
    private Dictionary<string, Sprite> elementalGemsList;

    // Element slot 1
    public Image element1Icon;
    public Image gem1Icon;
    public Image element1Color;
    public Text element1Label;

    // Element slot 2
    public Image element2Icon;
    public Image gem2Icon;
    public Image element2Color;
    public Text element2Label;

    // Combined Element
    public Image combinedElementIcon;
    public Text combinedElementLabel;

    public SkillsTooltip tooltipSkillR;
    public Image skillRIcon;
    public Image skillROverlay;    // for lock/unlock, & cooldown if any

    public SkillsTooltip tooltipSkillF;
    public Image skillFIcon;
    public Image skillFOverlay;    // for lock/unlock, & cooldown if any

    // unlock/lock sprites
    public Sprite unlockSprite;
    public Sprite lockedSprite;

    // Set Element to slot
    public void SetElementOne(string elementName)
    {
        SetElementToSlot(elementName, "One");
    }
    public void SetElementTwo(string elementName)
    {
        SetElementToSlot(elementName, "Two");
    }
    public void SwapElements()
    {
        PlayerData playerData = PlayerAction.instance.GetPlayerData();

        Element element1 = playerData.GetElementOne();
        Element element2 = playerData.GetElementTwo();

        playerData.SwapElementalChargeValues();

        string element1Name, element2Name;
        if (element1 == null)
            element1Name = "";
        else
            element1Name = element1.name;

        if (element2 == null)
            element2Name = "";
        else
            element2Name = element2.name;

        SetElementToSlot(element2Name, "One");
        SetElementToSlot(element1Name, "Two");
    }

    // Remove element from slot
    public void RemoveElementOne()
    {
        if (PlayerAction.instance.GetPlayerData().GetElementOne() != null)
            SetElementToSlot("", "One");
    }
    public void RemoveElementTwo()
    {
        if (PlayerAction.instance.GetPlayerData().GetElementTwo() != null)
            SetElementToSlot("", "Two");
    }

    /// <summary>
    ///  Function that CHANGES the PlayerData's elements
    ///  Different from function that sets the icons.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="slot"></param>
    private void SetElementToSlot(string element, string slot)
    {
        PlayerAction.instance.GetPlayerData().SetElementReference(element, slot);

        // set element icons after changing
        Element element1 = PlayerAction.instance.GetPlayerData().GetElementOne();
        Element element2 = PlayerAction.instance.GetPlayerData().GetElementTwo();
        switch (slot)
        {
            case "One":
                SetElement1Icons(element1);
                SetCombinedElementIcons(element1, element2);
                break;

            case "Two":
                SetElement2Icons(element2);
                SetCombinedElementIcons(element1, element2);
                break;
        }
    }

    /// <summary>
    ///  Set element information when menu first opens
    /// </summary>
    public void InitElements()
    {
        elementalGemsList = new Dictionary<string, Sprite>();
        foreach (ElementalGem gem in elementalGems)
        {
            elementalGemsList.Add(gem.elementName, gem.gemSprite);
        }

        Element element1 = PlayerAction.instance.GetPlayerData().GetElementOne();
        Element element2 = PlayerAction.instance.GetPlayerData().GetElementTwo();

        SetElement1Icons(element1);
        SetElement2Icons(element2);
        SetCombinedElementIcons(element1, element2);
    }

    private void SetElement1Icons(Element element1)
    {
        // Element icon
        if (element1 == null)
        {  // no alpha
            element1Icon.color = new Color(1f, 1f, 1f, 0f);
            element1Color.color = new Color(1f, 1f, 1f, 0f);
            element1Label.text = "";

            gem1Icon.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {  // have alpha
            element1Icon.color = new Color(1f, 1f, 1f, 1f);

            element1Icon.sprite = element1.icon;
            element1Color.color = element1.color;
            element1Label.text = element1.name;

            // Set gem icon
            gem1Icon.color = new Color(1f, 1f, 1f, 1f);
            gem1Icon.sprite = elementalGemsList[element1.name];
        }
    }

    private void SetElement2Icons(Element element2)
    {
        // Element icon
        if (element2 == null)
        {  // no alpha
            element2Icon.color = new Color(1f, 1f, 1f, 0f);
            element2Color.color = new Color(1f, 1f, 1f, 0f);
            element2Label.text = "";

            gem2Icon.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {  // have alpha
            element2Icon.color = new Color(1f, 1f, 1f, 1f);

            element2Icon.sprite = element2.icon;
            element2Color.color = element2.color;
            element2Label.text = element2.name;

            // Set gem icon
            gem2Icon.color = new Color(1f, 1f, 1f, 1f);
            gem2Icon.sprite = elementalGemsList[element2.name];
        }
    }

    private void SetCombinedElementIcons(Element element1, Element element2)
    {
        if (element1 == null || element2 == null)
        {
            combinedElementLabel.text = "";
            combinedElementIcon.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            CombinedElement combinedEle = PlayerAction.instance.GetPlayerData().GetCombinedElement();
            combinedElementLabel.text = combinedEle.name;

            combinedElementIcon.color = new Color(1f, 1f, 1f, 1f);

            combinedElementIcon.sprite = combinedEle.icon;

            // set skills' icons
            Skill skillR = combinedEle.GetSkillOne();
            skillRIcon.sprite = combinedEle.GetSkillIcon(0);    // set skill icon
  
            tooltipSkillR.SetSkill(combinedEle.skills[0]);  // set Skills tooltip
            if (skillR != null)
            {   // set normal overlay
                skillROverlay.sprite = unlockSprite;
                skillROverlay.fillAmount = 0f;
            }
            else {  // set locked overlay
                skillROverlay.sprite = lockedSprite;
                skillROverlay.fillAmount = 1f;
            }


            Skill skillF = combinedEle.GetSkillTwo();
            skillFIcon.sprite = combinedEle.GetSkillIcon(1);    // set skill icon

            tooltipSkillF.SetSkill(combinedEle.skills[1]);  // set Skills tooltip
            if (skillF != null)
            {   // set normal overlay
                skillFOverlay.sprite = unlockSprite;
                skillFOverlay.fillAmount = 0f;
            }
            else {  // set locked overlay
                skillFOverlay.sprite = lockedSprite;
                skillFOverlay.fillAmount = 1f;
            }
        }
    }

}
