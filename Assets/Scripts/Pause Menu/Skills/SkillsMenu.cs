using UnityEngine;
using System.Collections;

public class SkillsMenu : MonoBehaviour {

    // references to each sub-category
    public ElementsMenu elementMenu;
    public SkillListMenu skillListMenu;
    public GemsInventory gemsInventory;

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
        gemsInventory.Init();
    }

}
