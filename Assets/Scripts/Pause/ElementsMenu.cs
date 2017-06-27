using UnityEngine;
using UnityEngine.UI;

public class ElementsMenu : MonoBehaviour {

    // Element slot 1
    public Image element1Icon;
    public Text element1Label;

    // Element slot 2
    public Image element2Icon;
    public Text element2Label;

    // Combined Element
    public Image combinedElementIcon;
    public Text combinedElementLabel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    ///  Set element information
    /// </summary>
    public void InitElements()
    {
        Element element1 = PlayerAction.instance.GetPlayerData().GetElementOne();
        Element element2 = PlayerAction.instance.GetPlayerData().GetElementTwo();

        // Element 1
        if (element1 == null) {  // no alpha
            element1Icon.color = new Color(1f, 1f, 1f, 0f);
            element1Label.text = "";
        }
        else {  // have alpha
            element1Icon.color = new Color(1f, 1f, 1f, 1f);

            element1Icon.sprite = PlayerAction.instance.GetPlayerData().GetElementOne().icon;
            element1Label.text = PlayerAction.instance.GetPlayerData().GetElementOne().name;
        }

        // Element 2
        if (element2 == null)
        {  // no alpha
            element2Icon.color = new Color(1f, 1f, 1f, 0f);
            element2Label.text = "";
        }
        else
        {  // have alpha
            element2Icon.color = new Color(1f, 1f, 1f, 1f);

            element2Icon.sprite = PlayerAction.instance.GetPlayerData().GetElementTwo().icon;
            element2Label.text = PlayerAction.instance.GetPlayerData().GetElementTwo().name;
        }

        // Combined Element
        if (element1 == null || element2 == null) {  // no alpha
            combinedElementIcon.color = new Color(1f, 1f, 1f, 0f);
            combinedElementLabel.text = "";
        }
        else {  // have alpha
            combinedElementIcon.color = new Color(1f, 1f, 1f, 1f);
        }

    }   // end of InitElements()

}
