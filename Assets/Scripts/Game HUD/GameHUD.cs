using UnityEngine;
using System.Collections;

// singleton information of HUD
public class GameHUD : MonoBehaviour {

    [Tooltip("Player's HP bar")]
    public Transform HPBar;

    [Tooltip("Player's MP bar")]
    public Transform MPBar;

    [Tooltip("HUD that shows information of highlighted GameObject")]
    public HUDHighlightInfo highlightInfo;

    public static GameHUD instance;

    // Use this for initialization
    void Start () {

        instance = GameObject.Find("GameHUD").GetComponent<GameHUD>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void HPChanged(float newHP, float maxHP)
    {
        HPBar.localScale = new Vector3(newHP / maxHP, 1f, 1f);
    }

    public void MPChanged(float newMP, float maxMP)
    {
        MPBar.localScale = new Vector3(newMP / maxMP, 1f, 1f);
    }

}
