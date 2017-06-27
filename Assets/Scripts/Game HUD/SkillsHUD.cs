using UnityEngine;
using UnityEngine.UI;

public class SkillsHUD : MonoBehaviour {

    public Image elementOneIcon;
    public Text elementOneLabel;
    public Image elementTwoIcon;
    public Text elementTwoLabel;

    public Sprite unlockSprite;
    public Sprite lockedSprite;

    // element one
    public Image skillQIcon;
    public Image skillQOverlay;    // for lock/unlock, & cooldown if any

    public Image skillWIcon;
    public Image skillWOverlay;    // for lock/unlock, & cooldown if any

    public Image skillEIcon;
    public Image skillEOverlay;    // for lock/unlock, & cooldown if any

    // element two
    public Image skillAIcon;
    public Image skillAOverlay;    // for lock/unlock, & cooldown if any

    public Image skillSIcon;
    public Image skillSOverlay;    // for lock/unlock, & cooldown if any
    
    public Image skillDIcon;
    public Image skillDOverlay;    // for lock/unlock, & cooldown if any

    // combined element
    public Image skillRIcon;
    public Image skillROverlay;    // for lock/unlock, & cooldown if any

    public Image skillFIcon;
    public Image skillFOverlay;    // for lock/unlock, & cooldown if any

    public static SkillsHUD instance;

    // Use this for initialization
    private void Awake() {

        instance = GetComponent<SkillsHUD>();   // this
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
        }
        else {  // have alpha
            elementOneIcon.color = new Color(1f, 1f, 1f, 1f);

            elementOneIcon.sprite = element.icon;
            elementOneLabel.text = element.name;

            Skill skillQ = element.GetSkillOne();
            skillQIcon.sprite = element.GetSkillIcon(0);    // set skill icon
            skillQIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[0]);  // set Skills tooltip
            if (skillQ != null)
            {   // set normal overlay
                skillQOverlay.sprite = unlockSprite;
                skillQOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillQOverlay.sprite = lockedSprite;


            Skill skillW = element.GetSkillTwo();
            skillWIcon.sprite = element.GetSkillIcon(1);    // set skill icon
            skillWIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[1]);  // set Skills tooltip
            if (skillW != null)
            {   // set normal overlay
                skillWOverlay.sprite = unlockSprite;
                skillWOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillWOverlay.sprite = lockedSprite;

            Skill skillE = element.GetSkillThree();
            skillEIcon.sprite = element.GetSkillIcon(2);    // set skill icon
            skillEIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[2]);  // set Skills tooltip
            if (skillE != null)
            {   // set normal overlay
                skillEOverlay.sprite = unlockSprite;
                skillEOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillEOverlay.sprite = lockedSprite;
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
        }
        else {  // have alpha
            elementTwoIcon.color = new Color(1f, 1f, 1f, 1f);

            elementTwoIcon.sprite = element.icon;
            elementTwoLabel.text = element.name;

            Skill skillA = element.GetSkillOne();
            skillAIcon.sprite = element.GetSkillIcon(0);    // set skill icon
            skillAIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[0]);  // set Skills tooltip
            if (skillA != null)
            {   // set normal overlay
                skillAOverlay.sprite = unlockSprite;
                skillAOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillAOverlay.sprite = lockedSprite;


            Skill skillS = element.GetSkillTwo();
            skillSIcon.sprite = element.GetSkillIcon(1);    // set skill icon
            skillSIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[1]);  // set Skills tooltip
            if (skillS != null)
            {   // set normal overlay
                skillSOverlay.sprite = unlockSprite;
                skillSOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillSOverlay.sprite = lockedSprite;

            Skill skillD = element.GetSkillThree();
            skillDIcon.sprite = element.GetSkillIcon(2);    // set skill icon
            skillDIcon.transform.parent.GetComponent<SkillsTooltip>().SetSkill(element.skills[2]);  // set Skills tooltip
            if (skillD != null)
            {   // set normal overlay
                skillDOverlay.sprite = unlockSprite;
                skillDOverlay.fillAmount = 0f;
            }
            else  // set locked overlay
                skillDOverlay.sprite = lockedSprite;
        }
    }

}
