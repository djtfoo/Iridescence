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

    [Tooltip("HUD that shows information of highlighted GameObject")]
    public HUDElementBar elementBar;

    [Tooltip("3D Tooltip for GameObjects in scene")]
    public GameObject tooltip3D;

    public static GameHUD instance;

    // Use this for initialization
    private void Awake() {

        instance = GetComponent<GameHUD>(); // this
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void HPChanged(int newHP, int maxHP)
    {
        HPBar.localScale = new Vector3((float)newHP / maxHP, 1f, 1f);
    }

    public void MPChanged(int newMP, int maxMP)
    {
        MPBar.localScale = new Vector3((float)newMP / maxMP, 1f, 1f);
    }

}
