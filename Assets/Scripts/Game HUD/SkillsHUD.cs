using UnityEngine;
using UnityEngine.UI;

public class SkillsHUD : MonoBehaviour {

    public Image elementOneIcon;
    public Image elementTwoIcon;

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

    public void SetElementOneIcon(Sprite icon)
    {
        elementOneIcon.sprite = icon;
    }

    public void SetElementTwoIcon(Sprite icon)
    {
        elementTwoIcon.sprite = icon;
    }

}
