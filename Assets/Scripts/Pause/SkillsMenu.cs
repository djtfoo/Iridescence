using UnityEngine;
using System.Collections;

public class SkillsMenu : MonoBehaviour {

    public ElementsMenu elementMenu;
    public SkillListMenu skillListMenu;

	// Use this for initialization
	void Start () {
	
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
