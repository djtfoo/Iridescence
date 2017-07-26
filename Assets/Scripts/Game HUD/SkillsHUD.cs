using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public struct SkillOnHUD    // assets of 1 skill icon on HUD
{
    public string inputKey;
    public Image skillIcon;
    public Image skillOverlay;  // for lock/unlock, & cooldown if any
}

public class SkillsHUD : MonoBehaviour {

    public Image elementOneIcon;
    public Text elementOneLabel;
    public Image elementTwoIcon;
    public Text elementTwoLabel;

    public Sprite unlockSprite;
    public Sprite lockedSprite;

    public SkillOnHUD[] HUDSkillArray;
    private Dictionary<string, SkillOnHUD> HUDSkills;   // key is the input's key
    private Dictionary<string, int> KeyInputNum;    // the skill's number in its element

    public static SkillsHUD instance;

    //// element one
    //public Image skillQIcon;
    //public Image skillQOverlay;    // for lock/unlock, & cooldown if any
    //
    //public Image skillWIcon;
    //public Image skillWOverlay;    // for lock/unlock, & cooldown if any
    //
    //public Image skillEIcon;
    //public Image skillEOverlay;    // for lock/unlock, & cooldown if any
    //
    //// element two
    //public Image skillAIcon;
    //public Image skillAOverlay;    // for lock/unlock, & cooldown if any
    //
    //public Image skillSIcon;
    //public Image skillSOverlay;    // for lock/unlock, & cooldown if any
    //
    //public Image skillDIcon;
    //public Image skillDOverlay;    // for lock/unlock, & cooldown if any
    //
    //// combined element
    //public Image skillRIcon;
    //public Image skillROverlay;    // for lock/unlock, & cooldown if any
    //
    //public Image skillFIcon;
    //public Image skillFOverlay;    // for lock/unlock, & cooldown if any

    // Use this for initialization
    private void Awake() {

        instance = GetComponent<SkillsHUD>();   // this
        HUDSkills = new Dictionary<string, SkillOnHUD>();

        foreach (SkillOnHUD temp in HUDSkillArray)
        {
            HUDSkills.Add(temp.inputKey, temp);
        }

        KeyInputNum = new Dictionary<string, int>();
        KeyInputNum.Add("Q", 0);
        KeyInputNum.Add("W", 1);
        KeyInputNum.Add("E", 2);
        KeyInputNum.Add("A", 0);
        KeyInputNum.Add("S", 1);
        KeyInputNum.Add("D", 2);
        KeyInputNum.Add("R", 0);
        KeyInputNum.Add("F", 1);
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetCooldown(int count, float currTime, float maxTime)
    {
        // set cooldown for that element
        string inputKey = HUDSkillArray[count].inputKey;
        float percentage = Mathf.Min(1f, currTime / maxTime);
        HUDSkills[inputKey].skillOverlay.fillAmount = 1f - percentage;
    }

    /// <summary>
    ///  Function to set skill slot to empty
    ///   When no element is equipped
    /// </summary>
    private void SetSkillNull(string key)
    {
        HUDSkills[key].skillIcon.sprite = null;
        HUDSkills[key].skillOverlay.sprite = lockedSprite;
        HUDSkills[key].skillOverlay.fillAmount = 1f;
        HUDSkills[key].skillIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(null);  // remove Skills tooltip
    }
    /// <summary>
    ///  Function to set skill to slot
    ///   This function checks whether skill is locked or not
    /// </summary>
    private void SetSkillSlot(Element element, string key)
    {
        Skill skill = element.GetSkillByIdx(KeyInputNum[key]);
        HUDSkills[key].skillIcon.sprite = element.GetSkillIcon(KeyInputNum[key]);    // set skill icon
        HUDSkills[key].skillIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[KeyInputNum[key]]);  // set Skills tooltip
        if (skill != null)
        {   // set normal overlay
            HUDSkills[key].skillOverlay.sprite = unlockSprite;
            HUDSkills[key].skillOverlay.fillAmount = 0f;
        }
        else
        {  // set locked overlay
            HUDSkills[key].skillOverlay.sprite = lockedSprite;
            HUDSkills[key].skillOverlay.fillAmount = 1f;
        }
    }

    /// <summary>
    ///  Set label & icons for element one & its skills
    /// </summary>
    /// <param name="icon"></param>
    public void SetElementOne(Element element)
    {
        if (element == null)
        {  // no alpha
            elementOneIcon.color = new Color(1f, 1f, 1f, 0f);
            elementOneLabel.text = "";

            SetSkillNull("Q");
            SetSkillNull("W");
            SetSkillNull("E");
        }
        else {  // have alpha
            elementOneIcon.color = new Color(1f, 1f, 1f, 1f);

            elementOneIcon.sprite = element.icon;
            elementOneLabel.text = element.name;

            SetSkillSlot(element, "Q");
            SetSkillSlot(element, "W");
            SetSkillSlot(element, "E");
        }
    }

    /// <summary>
    ///  Set icons for element two & its skills
    /// </summary>
    /// <param name="icon"></param>
    public void SetElementTwo(Element element)
    {
        if (element == null)
        {  // no alpha
            elementTwoIcon.color = new Color(1f, 1f, 1f, 0f);
            elementTwoLabel.text = "";

            SetSkillNull("A");
            SetSkillNull("S");
            SetSkillNull("D");
        }
        else {  // have alpha
            elementTwoIcon.color = new Color(1f, 1f, 1f, 1f);

            elementTwoIcon.sprite = element.icon;
            elementTwoLabel.text = element.name;

            SetSkillSlot(element, "A");
            SetSkillSlot(element, "S");
            SetSkillSlot(element, "D");
        }
    }

    public void SetCombinedElement(CombinedElement element)
    {
        if (element == null)
        {
            SetSkillNull("R");
            SetSkillNull("F");
        }
        else
        {
            SetSkillSlot(element, "R");
            SetSkillSlot(element, "F");
        }
    }

}
