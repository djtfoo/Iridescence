using UnityEngine;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour {

    public Text questName;
    public Text descriptionText;
    public Text requirementText;
    public Text statusText;

	// Use this for initialization
	void Start () {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
	}

}
