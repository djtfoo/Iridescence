using UnityEngine;
using System.Collections;

public class SkillsMenu : MonoBehaviour {

    public ElementsMenu elementMenu;
    public SkillListMenu skillListMenu;

	// Use this for initialization
	public void InitSelf()
    {
        skillListMenu.InitSelf();
    }

    // Update is called once per frame
    void Update () {
	
	}
    
    public void InitSkillsMenu()
    {
        elementMenu.InitElements();
        skillListMenu.InitSkills();
    }

}
