using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour {

    // reference to different stat menus
    public MainStatsMenu mainStatsMenu;
    public ElementStatsMenu elementStatsMenu;

    // buttons for each stat menu
    public Button buttonOne;
    public Button buttonTwo;

    // SINGLETON
    public static StatsMenu instance;

    // Use this for initialization
    private void Awake()
    {
        instance = GetComponent<StatsMenu>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitStatsMenu()
    {
        SetMainStatsActive();
    }

    // on click
    public void SetMainStatsActive()
    {
        mainStatsMenu.gameObject.SetActive(true);
        elementStatsMenu.gameObject.SetActive(false);

        mainStatsMenu.InitSelf();

        // set colors of buttons
        /// One
        ColorBlock cb = buttonOne.colors;
        cb.normalColor = Color.white;
        buttonOne.colors = cb;

        /// Two
        ColorBlock cb2 = buttonTwo.colors;
        cb.normalColor = new Color(0.85f, 0.85f, 0.85f);
        buttonTwo.colors = cb;

    }
    public void SetElementalStatsActive()
    {
        mainStatsMenu.gameObject.SetActive(false);
        elementStatsMenu.gameObject.SetActive(true);

        elementStatsMenu.InitSelf();

        // set colors of buttons
        /// One
        ColorBlock cb = buttonOne.colors;
        cb.normalColor = new Color(0.85f, 0.85f, 0.85f);
        buttonOne.colors = cb;

        /// Two
        ColorBlock cb2 = buttonTwo.colors;
        cb.normalColor = Color.white;
        buttonTwo.colors = cb;
    }


}
