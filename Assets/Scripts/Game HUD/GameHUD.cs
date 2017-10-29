using UnityEngine;
using UnityEngine.UI;

// singleton information of HUD
public class GameHUD : MonoBehaviour {

    [Tooltip("Player's HP bar")]
    public Transform HPBar;

    [Tooltip("Player's MP bar")]
    public Transform MPBar;

    [Tooltip("Text for Player's Level")]
    public Text playerLevel;

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

    public void ResetHUD()
    {
        highlightInfo.DeactivateHighlightInfo();
        tooltip3D.SetActive(false);
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
